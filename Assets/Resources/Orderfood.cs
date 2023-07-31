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
    public List<Image> H_images;
    public List<Image> M_images;
    public List<Image> L_images;
    private void Start()
    {
        ParseLevelData();
        selectionButton.onClick.AddListener(StartSelection);
        inventoryData = new InventoryData();
        LoadInventoryData();
        DebugInventoryContents();
    }
    private void ActivateImage()
    {
        if (selectedLevelItem.imageName == "a rainbow cake filled with vitality")
        {
            H_images[0].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Rainbow rice cake with sociality")
        {
            H_images[1].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Rainbow Pancake with Boldness")
        {
            H_images[2].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Curious rainbow lolipop")
        {
            H_images[3].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Rainbow slush with various emotions")
        {
            H_images[4].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "a calm shipped cream cake")
        {
            H_images[5].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Baekseolgi with independence")
        {
            H_images[6].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Plain pancakes with prudence")
        {
            H_images[7].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "a speculative white candy")
        {
            H_images[8].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "a cold white slush")
        {
            H_images[9].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Black-bean-sauce noodles")
        {
            M_images[0].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Jjambbong")
        {
            M_images[1].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Sweet and sour pork")
        {
            M_images[2].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Fried dumplings")
        {
            M_images[3].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "katsudon")
        {
            M_images[4].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "bibimbap")
        {
            M_images[5].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "fried rice")
        {
            M_images[6].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Curry Rice")
        {
            M_images[7].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "omelet rice")
        {
            M_images[8].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Ramen")
        {
            M_images[9].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Carbonara")
        {
            M_images[10].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "stir-fried Rice Cake")
        {
            M_images[11].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Fish cake skewers")
        {
            M_images[12].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Takoyaki")
        {
            M_images[13].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Chicken kebabs")
        {
            M_images[14].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Takoyaki")
        {
            M_images[15].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "hamburger")
        {
            M_images[16].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Korean style Pancake")
        {
            M_images[17].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Pork belly")
        {
            M_images[18].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Korean style raw beef")
        {
            M_images[19].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Takoyaki")
        {
            M_images[20].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Fried Chicken")
        {
            M_images[21].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "seasoned spicy chicken")
        {
            M_images[22].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "fruit salad")
        {
            M_images[23].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Macaron")
        {
            M_images[24].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "a dongo")
        {
            M_images[25].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Pudding")
        {
            M_images[26].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Strawberry cake")
        {
            M_images[27].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Chocolate Cake")
        {
            M_images[28].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Carrot Cake")
        {
            M_images[29].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Blueberry cake")
        {
            M_images[30].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Banana cake")
        {
            M_images[31].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Shrimp sushi")
        {
            M_images[32].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Egg sushi")
        {
            M_images[33].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Tuna sushi")
        {
            M_images[34].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Pork Cutlet")
        {
            M_images[35].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Sweet Potatoes and Cheese Stuffed Pork Cutlet")
        {
            M_images[36].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Ham and Cheese Sandwich")
        {
            M_images[37].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Ham and egg Sandwich")
        {
            M_images[38].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "a peanut butter sandwich")
        {
            M_images[39].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "ang butter sandwich")
        {
            M_images[40].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Mint chocolate icecream")
        {
            M_images[41].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Shooting Star icecream")
        {
            M_images[42].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Strawberry Ice Cream")
        {
            M_images[43].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Chocolate ice cream")
        {
            M_images[44].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Yogurt ice cream")
        {
            M_images[45].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Soda ice cream")
        {
            M_images[46].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Combination Pizza")
        {
            M_images[47].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Sweet Potato Pizza")
        {
            M_images[48].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Potato pizza")
        {
            M_images[49].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "pepperoni pizza")
        {
            M_images[50].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Cheeze pizza")
        {
            M_images[51].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Milk-flavored stick dog gum")
        {
            L_images[0].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Cheese-flavored stick dog gum")
        {
            L_images[1].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Sweet potato-flavored stick dog gum")
        {
            L_images[2].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Chicken-flavored stick dog gum")
        {
            L_images[3].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Dried pollack-flavored stick dog gum")
        {
            L_images[4].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Beef-flavored stick dog gum")
        {
            L_images[5].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Salmon-flavored stick dog gum")
        {
            L_images[6].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Milk-flavored twisted dog gum")
        {
            L_images[7].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Cheese-flavored twisted dog gum")
        {
            L_images[8].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Sweet potato-flavored twisted dog gum")
        {
            L_images[9].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Chicken-flavored twisted dog gum")
        {
            L_images[10].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Dried pollack-flavored twisted dog gum")
        {
            L_images[11].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Beef-flavored twisted dog gum")
        {
            L_images[12].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Salmon-flavored twisted dog gum")
        {
            L_images[13].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Chicken-flavored feed")
        {
            L_images[14].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "salmon-flavored feed")
        {
            L_images[15].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "beef-flavored feed")
        {
            L_images[16].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "dried pollack feed")
        {
            L_images[17].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Watermelon")
        {
            L_images[18].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Banana")
        {
            L_images[19].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "a heavenly peach")
        {
            L_images[20].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "cherry")
        {
            L_images[21].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "oriental melon")
        {
            L_images[22].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Mango")
        {
            L_images[23].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "plum")
        {
            L_images[24].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "a beef-flavored can")
        {
            L_images[25].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "a salmon-flavored can")
        {
            L_images[26].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "a chicken-flavored can")
        {
            L_images[27].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "a lamb-flavored can")
        {
            L_images[28].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "a duck-flavored can")
        {
            L_images[29].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "canned tuna")
        {
            L_images[30].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Puppy candy")
        {
            L_images[31].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Meong Puccino")
        {
            L_images[32].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Cat Churu")
        {
            L_images[33].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "catnip")
        {
            L_images[34].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "bone")
        {
            L_images[35].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "pet milk")
        {
            L_images[36].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "the neck of a duck")
        {
            L_images[37].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Shrimp crackers")
        {
            L_images[38].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "dried sweet potato")
        {
            L_images[39].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Pumpkin sweet potato")
        {
            L_images[40].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Chicken breast")
        {
            L_images[41].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "Salad")
        {
            L_images[42].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "bean curd")
        {
            L_images[43].gameObject.SetActive(false);
        }
        else if (selectedLevelItem.imageName == "dried pollack salad")
        {
            L_images[44].gameObject.SetActive(false);
        }
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
