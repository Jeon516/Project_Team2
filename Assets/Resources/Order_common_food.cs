using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class LevelData
{
    public string level;
    public float probability;
}

[System.Serializable]
public class InGameData
{
    public List<ItemData> itemList;
    public string usingItem;
}

[System.Serializable]
public class ItemDataList
{
    public ItemData[] items;
}

[System.Serializable]
public class ItemData
{
    public string myClass;
    public string sort;
    public string name;
    public string itemText;
    public string conv;
    public int firstStatType;
    public int firstStatValue;
    public int secondStatType;
    public int secondStatValue;
    public string imageName;
    public int quantity;
}

public class Order_common_food : MonoBehaviour
{
    public TextAsset levelData;
    public LevelData[] levels;
    public Button selectionButton;
    public Image selectedImage;
    public Image foodImage;
    public Text SelectedName;
    public Text SelectedInfo;
    public Image OpenImage;

    private Dictionary<string, int> inventory;
    private InGameData inGameData;

    private void Start()
    {
        ParseLevelData();

        selectionButton.onClick.AddListener(StartSelection);

        // Initialize inventory dictionary
        inventory = new Dictionary<string, int>();

        // Load in-game data from JSON file (if it exists)
        LoadInGameData();
    }

    private void ParseLevelData()
    {
        if (levelData != null)
        {
            string jsonText = levelData.text;
            ItemDataList itemList = JsonUtility.FromJson<ItemDataList>(jsonText);

            if (itemList != null && itemList.items.Length > 0)
            {
                levels = new LevelData[itemList.items.Length];
                for (int i = 0; i < itemList.items.Length; i++)
                {
                    levels[i] = new LevelData
                    {
                        level = itemList.items[i].name,
                        probability = Random.Range(0f, 100f) // Generate random probability for each item
                    };
                }
            }
            else
            {
                Debug.LogError("No items found in the JSON file.");
            }
        }
        else
        {
            Debug.LogError("Level data not assigned.");
        }
    }

    private void StartSelection()
    {
        string selectedLevel = SelectLevel();

        LevelData selectedLevelData = GetLevelData(selectedLevel);

        if (selectedLevelData != null)
        {
            string jsonFilePath = "JsonFiles/" + selectedLevelData.level;
            TextAsset selectedJsonFile = Resources.Load<TextAsset>(jsonFilePath);

            if (selectedJsonFile != null)
            {
                LoadJson(selectedJsonFile, selectedLevelData);
                SaveInventoryToJson();
            }
            else
            {
                Debug.LogError("JSON file not found for selected level: " + selectedLevelData.level);
            }
        }
        else
        {
            Debug.LogError("Invalid level selected.");
        }
    }

    private LevelData GetLevelData(string level)
    {
        foreach (LevelData levelData in levels)
        {
            if (levelData.level == level)
            {
                return levelData;
            }
        }

        return null;
    }

    private void LoadJson(TextAsset jsonFile, LevelData selectedLevelData)
    {
        if (jsonFile != null)
        {
            string jsonText = jsonFile.text;
            ItemDataList itemList = JsonUtility.FromJson<ItemDataList>(jsonText);

            if (itemList != null && itemList.items.Length > 0)
            {
                ItemData selectedItem = itemList.items[Random.Range(0, itemList.items.Length)];
                Debug.Log("Selected Item: " + selectedItem.name);

                // Load the Food Image based on the imageName and selectedLevel
                string imagePath = "Image/Food/" + selectedLevelData.level + "/" + selectedItem.imageName;
                Sprite selectedSprite = Resources.Load<Sprite>(imagePath);

                if (selectedSprite != null)
                {
                    selectedImage.sprite = selectedSprite;
                    foodImage.sprite = selectedSprite; // Assign the Food Image
                    OpenImage.sprite = selectedSprite; // Assign the Open Image

                    // Assign the Selected Name and Selected Info Text
                    SelectedName.text = selectedItem.name;
                    SelectedInfo.text = selectedItem.itemText;

                    // Add the obtained item to the inventory or increase its quantity if it already exists
                    AddToInventory(selectedItem);
                }
                else
                {
                    Debug.LogError("Image file not found: " + imagePath);
                }
            }
            else
            {
                Debug.LogError("No items found in the JSON file.");
            }
        }
        else
        {
            Debug.LogError("JSON file not assigned.");
        }
    }

    private string SelectLevel()
    {
        float randomValue = Random.Range(0f, 100f);
        float sum = 0f;

        foreach (LevelData level in levels)
        {
            sum += level.probability;
            if (randomValue <= sum)
            {
                return level.level;
            }
        }

        return levels[0].level;
    }

    private void AddToInventory(ItemData selectedItem)
    {
        if (selectedItem == null)
            return;

        // Check if the item already exists in the inventory
        if (inventory.ContainsKey(selectedItem.name))
        {
            // If the item exists, increase its quantity by 1
            inventory[selectedItem.name]++;
        }
        else
        {
            // If the item does not exist, add it to the inventory with quantity 1
            inventory[selectedItem.name] = 1;
        }
    }

    private void LoadInGameData()
    {
        string jsonFilePath = "JsonFiles/Inventory/InventoryData";
        TextAsset inventoryData = Resources.Load<TextAsset>(jsonFilePath);

        if (inventoryData != null)
        {
            inGameData = JsonUtility.FromJson<InGameData>(inventoryData.text);

            // Initialize inventory dictionary from in-game data
            foreach (ItemData item in inGameData.itemList)
            {
                inventory[item.name] = item.quantity;
            }
        }
        else
        {
            Debug.LogWarning("Inventory JSON file not found.");
            inGameData = new InGameData
            {
                itemList = new List<ItemData>(),
                usingItem = ""
            };
        }
    }

    private void SaveInventoryToJson()
    {
        // Save the updated inventory data to inGameData
        inGameData.itemList.Clear();
        foreach (var item in inventory)
        {
            inGameData.itemList.Add(new ItemData
            {
                name = item.Key,
                quantity = item.Value
            });
        }

        string jsonFilePath = "JsonFiles/Inventory/InventoryData";
        string jsonData = JsonUtility.ToJson(inGameData);
        File.WriteAllText(Application.dataPath + "/Resources/" + jsonFilePath + ".json", jsonData);
    }
}
