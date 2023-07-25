using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class LevelData
{
    public string level;
    public float probability;
    public string jsonFileName;
}

[System.Serializable]
public class InventoryData
{
    public List<ItemInfo> itemList = new List<ItemInfo>();
    public string usingItem;
}

[System.Serializable]
public class ItemInfo
{
    public string myClass;
    public string itemName;
    public int quantity;
    public string conv;
    public int firstStatType; 
    public int secondStatType;   
    public int firstStatValue;
    public int secondStatValue; 
    public string itemText;
    public string imageName; 
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

    private void Start()
    {
        ParseLevelData();
        selectionButton.onClick.AddListener(StartSelection);
        inventory = new Dictionary<string, int>();
    }

    private ProbabilityData ParseLevelData(TextAsset levelData)
    {
        if (levelData != null)
        {
            string jsonText = levelData.text;
            ProbabilityData data = JsonUtility.FromJson<ProbabilityData>(jsonText);
            return data;
        }
        else
        {
            Debug.LogError("Level data not assigned.");
            return null;
        }
    }

    private void ParseLevelData()
    {
        ProbabilityData data = ParseLevelData(levelData);
        if (data != null)
        {
            levels = data.levels;

            for (int i = 0; i < levels.Length; i++)
            {
                string jsonFileName = "JsonFiles/" + levels[i].level;
                levels[i].jsonFileName = jsonFileName;
            }
        }
    }

    private void StartSelection()
    {
        string selectedLevel = SelectLevel();

        LevelData selectedLevelData = GetLevelData(selectedLevel);

        if (selectedLevelData != null)
        {
            string jsonFilePath = selectedLevelData.jsonFileName;
            TextAsset selectedJsonFile = Resources.Load<TextAsset>(jsonFilePath);

            if (selectedJsonFile != null)
            {
                LoadJson(selectedJsonFile, selectedLevelData);
                SaveInventoryToJson();
            }
            else
            {
                Debug.LogError("JSON file not found for selected level: " + selectedLevel);
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

                string imagePath = "Image/Food/" + selectedLevelData.level + "/" + selectedItem.imageName;
                Sprite selectedSprite = Resources.Load<Sprite>(imagePath);

                if (selectedSprite != null)
                {
                    selectedImage.sprite = selectedSprite;
                    foodImage.sprite = selectedSprite; 
                    OpenImage.sprite = selectedSprite; 

                    SelectedName.text = selectedItem.name;
                    SelectedInfo.text = selectedItem.itemText;

                    if (inventory.ContainsKey(selectedItem.name))
                    {
                        inventory[selectedItem.name]++;
                    }
                    else
                    {
                        inventory[selectedItem.name] = 1;
                    }

                    ItemInfo newItemInfo = new ItemInfo
                    {
                        myClass = selectedItem.myClass,
                        itemName = selectedItem.name,
                        quantity = 1,
                        conv = selectedItem.conv,
                        firstStatType= selectedItem.firstStatType, 
                        secondStatType = selectedItem.secondStatType, 
                        firstStatValue = selectedItem.firstStatValue,
                        secondStatValue = selectedItem.secondStatValue,
                        itemText = selectedItem.itemText,
                        imageName = selectedItem.imageName
                    };

                    // Update the inventory data JSON file
                    UpdateInventory(newItemInfo);
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

    private void UpdateInventory(ItemInfo newItemInfo)
    {
        // Load the existing inventory data
        string jsonFilePath = "Assets/Resources/JsonFiles/Inventory/InventoryData.json";
        TextAsset jsonFile = Resources.Load<TextAsset>("JsonFiles/Inventory/InventoryData");
        InventoryData inventoryData = null;

        if (jsonFile != null)
        {
            string jsonData = jsonFile.text;
            inventoryData = JsonUtility.FromJson<InventoryData>(jsonData);
        }
        else
        {
            Debug.LogError("Inventory JSON file not found: " + jsonFilePath);
            return;
        }

        ItemInfo foundItem = null;
        foreach (ItemInfo item in inventoryData.itemList)
        {
            if (item.itemName == newItemInfo.itemName)
            {
                foundItem = item;
                break;
            }
        }

        if (foundItem != null)
        {
            foundItem.quantity++;
        }
        else
        {
            inventoryData.itemList.Add(newItemInfo);
        }
        string updatedJsonData = JsonUtility.ToJson(inventoryData);
        File.WriteAllText(jsonFilePath, updatedJsonData);
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

    private void SaveInventoryToJson()
    {
        string jsonFilePath = Application.persistentDataPath + "/inventory.json";
        string jsonData = JsonUtility.ToJson(inventory);

        try
        {
            File.WriteAllText(jsonFilePath, jsonData);
            Debug.Log("Inventory JSON file saved successfully.");
        }
        catch (IOException e)
        {
            Debug.LogError("Error writing inventory JSON file: " + e.Message);
        }
    }
}

[System.Serializable]
public class ProbabilityData
{
    public LevelData[] levels;
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
}
