using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine.Networking;

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

public class Orderfood : MonoBehaviour
{
    public TextAsset levelData;
    public LevelData[] levels;
    public Button selectionButton;
    public Image selectedImage;
    public Image foodImage;
    public Text SelectedName;
    public Text SelectedInfo;
    public Image OpenImage;

    private InventoryData inventoryData;
    private ItemData selectedLevelItem; // 전역 변수로 선택된 아이템을 저장하기 위한 변수 추가

    private void Start()
    {
        ParseLevelData();
        selectionButton.onClick.AddListener(StartSelection);
        inventoryData = new InventoryData();
        LoadInventoryData();
        DebugInventoryContents();
    }

    private void StartSelection()
    {
        string selectedLevel = SelectLevel();

        LevelData selectedLevelData = GetLevelData(selectedLevel);

        if (selectedLevelData != null)
        {
            string jsonFileName = selectedLevelData.level + ".json";
            string jsonFilePath = Path.Combine(Application.streamingAssetsPath, jsonFileName);
            StartCoroutine(LoadJson(jsonFilePath, selectedLevelData));
        }
        else
        {
            Debug.LogError("Invalid level selected.");
        }
    }

    private IEnumerator LoadJson(string jsonFilePath, LevelData selectedLevelData)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(jsonFilePath))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                // For Android, use UnityWebRequest to load the JSON file
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    string jsonData = www.downloadHandler.text;
                    ItemDataList itemList = JsonUtility.FromJson<ItemDataList>(jsonData);
                    LoadFixedItem(itemList);
                    SaveInventoryToJson();
                    UpdateUI();
                }
                else
                {
                    Debug.LogError("Error loading JSON file: " + www.error);
                }
            }
            else
            {
                // For other platforms, use the existing file loading code
                if (File.Exists(jsonFilePath))
                {
                    string jsonData = File.ReadAllText(jsonFilePath);
                    ItemDataList itemList = JsonUtility.FromJson<ItemDataList>(jsonData);
                    LoadFixedItem(itemList);
                    SaveInventoryToJson();
                    UpdateUI();
                }
                else
                {
                    Debug.LogError("JSON file not found at path: " + jsonFilePath);
                }
            }
        }
    }

    private void LoadFixedItem(ItemDataList itemList)
    {
        if (itemList != null && itemList.items.Length > 0)
        {
            selectedLevelItem = itemList.items[Random.Range(0, itemList.items.Length)]; // Choose a random item from the list.
            Debug.Log("Selected Item: " + selectedLevelItem.name);

            ItemInfo newItemInfo = new ItemInfo
            {
                myClass = selectedLevelItem.myClass,
                itemName = selectedLevelItem.name,
                quantity = 1,
                conv = selectedLevelItem.conv,
                firstStatType = selectedLevelItem.firstStatType,
                secondStatType = selectedLevelItem.secondStatType,
                firstStatValue = selectedLevelItem.firstStatValue,
                secondStatValue = selectedLevelItem.secondStatValue,
                itemText = selectedLevelItem.itemText,
                imageName = selectedLevelItem.imageName
            };

            Debug.Log(selectedLevelItem.firstStatType + "이 "+selectedLevelItem.firstStatValue + "만큼 늘어날 수 있습니다.");
            Debug.Log(selectedLevelItem.secondStatType + "이 " + selectedLevelItem.secondStatValue + "만큼 늘어날 수 있습니다.");

            // Update the inventory data
            UpdateInventory(newItemInfo);
        }
        else
        {
            Debug.LogError("No items found in the JSON file.");
        }
    }

    private void UpdateUI()
    {
        // Use the selectedLevelItem to update the UI elements
        if (selectedLevelItem != null)
        {
            // Update UI elements using selectedLevelItem
            string imagePath = "Image/Food/" + selectedLevelItem.myClass + "/" + selectedLevelItem.imageName;
            Sprite selectedSprite = Resources.Load<Sprite>(imagePath);

            if (selectedSprite != null)
            {
                selectedImage.sprite = selectedSprite;
                foodImage.sprite = selectedSprite;
                OpenImage.sprite = selectedSprite;

                SelectedName.text = selectedLevelItem.name;
                SelectedInfo.text = selectedLevelItem.itemText;

                // Update the quantity
                // The quantity is not modified in this example, modify it based on your logic if needed.
            }
            else
            {
                Debug.LogError("Image file not found: " + imagePath);
            }
        }
        else
        {
            Debug.LogWarning("No item selected!");
        }
    }

    private void SaveInventoryToJson()
    {
        string jsonFilePath = Path.Combine(Application.persistentDataPath, "inventory.json");
        string jsonData = JsonUtility.ToJson(inventoryData);

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

    private void UpdateInventory(ItemInfo newItemInfo)
    {
        inventoryData.itemList.Add(newItemInfo);
    }

    private void LoadInventoryData()
    {
        string jsonFilePath = Path.Combine(Application.persistentDataPath, "inventory.json");

        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            inventoryData = JsonUtility.FromJson<InventoryData>(jsonData);
        }
        else
        {
            Debug.Log("No existing inventory data found. Creating new inventory data.");
            inventoryData = new InventoryData();
        }
    }

    private void DebugInventoryContents()
    {
        Debug.Log("Inventory Contents:");

        foreach (var item in inventoryData.itemList)
        {
            string itemName = item.itemName;
            int quantity = item.quantity;
            Debug.Log(itemName + " x " + quantity);
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

    private void ParseLevelData()
    {
        if (levelData != null)
        {
            string jsonText = levelData.text;
            ProbabilityData data = JsonUtility.FromJson<ProbabilityData>(jsonText);
            if (data != null)
            {
                levels = data.levels;
            }
            else
            {
                Debug.LogError("Failed to parse level data from JSON.");
            }
        }
        else
        {
            Debug.LogError("Level data not assigned.");
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
