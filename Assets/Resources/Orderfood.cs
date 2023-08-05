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
    private Food foodList;
    public List<Image> H_images;
    public List<Image> M_images;
    public List<Image> L_images;

    public GameObject[] Selected;
    public Image[] SelectedImage;
    public Text ShowDogName;
    public Text ShowDogInformation;
    public int selectorder=0;
    // Show FoodCollection

    public static Orderfood Instance { get; private set; } = null;

    private void Start()
    {
        Instance = this;

        ParseLevelData();
        selectionButton.onClick.AddListener(StartSelection);
        inventoryData = new InventoryData();
        foodList = new Food();
        LoadInventoryData();
        LoadFoodCollect();
        ActivateImage();
    }

    private void ActivateImage()
    {
        for(int i=0;i< foodList.foods.Count;i++)
        {
            if (foodList.foods[i].Image == "a rainbow cake filled with vitality")
            {
                H_images[0].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Rainbow rice cake with sociality")
            {
                H_images[1].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Rainbow Pancake with Boldness")
            {
                H_images[2].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Curious rainbow lollipop")
            {
                H_images[3].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Rainbow slush with various emotions")
            {
                H_images[4].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "a calm whipped cream cake")
            {
                H_images[5].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Baekseolgi with independence")
            {
                H_images[6].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Plain pancakes with prudence")
            {
                H_images[7].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "a speculative white candy")
            {
                H_images[8].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "a cold white slush")
            {
                H_images[9].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Black-bean-sauce noodles")
            {
                M_images[0].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Jjambbong")
            {
                M_images[1].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Sweet and sour pork")
            {
                M_images[2].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Fried dumplings")
            {
                M_images[3].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "katsudon")
            {
                M_images[4].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "bibimbap")
            {
                M_images[5].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "fried rice")
            {
                M_images[6].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Curry Rice")
            {
                M_images[7].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "omelet rice")
            {
                M_images[8].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Ramen")
            {
                M_images[9].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Carbonara")
            {
                M_images[10].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "stir-fried Rice Cake")
            {
                M_images[11].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Fish cake skewers")
            {
                M_images[12].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Takoyaki")
            {
                M_images[13].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Chicken kebabs")
            {
                M_images[14].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "hamburger")
            {
                M_images[15].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "French fries")
            {
                M_images[16].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Korean style Pancake")
            {
                M_images[17].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Pork belly")
            {
                M_images[18].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Korean style raw beef")
            {
                M_images[19].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Fried Chicken")
            {
                M_images[20].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "seasoned spicy chicken")
            {
                M_images[21].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "fruit salad")
            {
                M_images[22].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Macaron")
            {
                M_images[23].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "a dango")
            {
                M_images[24].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Pudding")
            {
                M_images[25].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Strawberry cake")
            {
                M_images[26].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Chocolate Cake")
            {
                M_images[27].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Carrot Cake")
            {
                M_images[28].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Red Velvet cake")
            {
                M_images[29].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Banana cake")
            {
                M_images[30].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Shrimp sushi")
            {
                M_images[31].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "fried tofu sushi")
            {
                M_images[32].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Salmon sushi")
            {
                M_images[33].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Egg sushi")
            {
                M_images[34].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Tuna sushi")
            {
                M_images[35].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Pork Cutlet")
            {
                M_images[36].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Sweet Potatoes and Cheese Stuffed Pork Cutlet")
            {
                M_images[37].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Pork Cutlet with Curry")
            {
                M_images[38].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Ham and Cheese Sandwich")
            {
                M_images[39].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Ham and egg Sandwich")
            {
                M_images[40].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "a barbecued chicken sandwich")
            {
                M_images[41].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "a peanut butter sandwich")
            {
                M_images[42].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "ang butter sandwich")
            {
                M_images[43].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Mint chocolate ice cream")
            {
                M_images[44].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Shooting Star ice cream")
            {
                M_images[45].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Strawberry Ice Cream")
            {
                M_images[46].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Chocolate ice cream")
            {
                M_images[47].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Yogurt ice cream")
            {
                M_images[48].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Soda ice cream")
            {
                M_images[49].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Combination Pizza")
            {
                M_images[50].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Sweet Potato Pizza")
            {
                M_images[51].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Potato pizza")
            {
                M_images[52].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "pepperoni pizza")
            {
                M_images[53].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Cheese pizza")
            {
                M_images[54].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Milk-flavored stick dog gum")
            {
                L_images[0].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Cheese-flavored stick dog gum")
            {
                L_images[1].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Sweet potato-flavored stick dog gum")
            {
                L_images[2].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Chicken-flavored stick dog gum")
            {
                L_images[3].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Dried pollack flavored stick dog gum")
            {
                L_images[4].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Beef-flavored stick dog gum")
            {
                L_images[5].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Salmon-flavored stick dog gum")
            {
                L_images[6].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Milk-flavored twisted dog gum")
            {
                L_images[7].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Cheese-flavored twisted dog gum")
            {
                L_images[8].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Sweet potato-flavored twisted dog gum")
            {
                L_images[9].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Chicken-flavored twisted dog gum")
            {
                L_images[10].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Dried pollack-flavored twisted dog gum")
            {
                L_images[11].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Beef-flavored twisted dog gum")
            {
                L_images[12].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Salmon-flavored twisted dog gum")
            {
                L_images[13].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "chicken-flavored feed")
            {
                L_images[14].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "salmon-flavored feed")
            {
                L_images[15].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "beef-flavored feed")
            {
                L_images[16].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "dried pollack feed")
            {
                L_images[17].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Watermelon")
            {
                L_images[18].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Banana")
            {
                L_images[19].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "a heavenly peach")
            {
                L_images[20].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "cherry")
            {
                L_images[21].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "oriental melon")
            {
                L_images[22].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Mango")
            {
                L_images[23].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "plum")
            {
                L_images[24].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "a beef-flavored can")
            {
                L_images[25].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "a salmon-flavored can")
            {
                L_images[26].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "a chicken-flavored can")
            {
                L_images[27].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "a lamb-flavored can")
            {
                L_images[28].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "a duck-flavored can")
            {
                L_images[29].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "canned tuna")
            {
                L_images[30].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Puppy candy")
            {
                L_images[31].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Meong Puccino")
            {
                L_images[32].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Cat Churu")
            {
                L_images[33].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "catnip")
            {
                L_images[34].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "bone")
            {
                L_images[35].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "pet milk")
            {
                L_images[36].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "the neck of a duck")
            {
                L_images[37].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Shrimp crackers")
            {
                L_images[38].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "dried sweet potatoes")
            {
                L_images[39].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Pumpkin sweet potatoes")
            {
                L_images[40].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Chicken breast")
            {
                L_images[41].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "Salad")
            {
                L_images[42].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "bean curd")
            {
                L_images[43].gameObject.SetActive(false);
            }
            else if (foodList.foods[i].Image == "dried pollack salad")
            {
                L_images[44].gameObject.SetActive(false);
            }
        }
    }

    public void Onclick_CollectFood()
    {
        for (int i = 0; i < 110; i++)
        {
            if (Selected[i].activeSelf)
            {
                Debug.Log(i+"이 켜져있습니다");
                for (int j = 0; j < foodList.foods.Count; j++)
                {
                    if (SelectedImage[i].sprite.name == foodList.foods[j].Image)
                    {
                        selectorder = i;
                        ShowDogName.text = foodList.foods[j].Name;
                        ShowDogInformation.text = foodList.foods[j].Info;
                    }
                }
            }
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
                    SaveFoodData();
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

            Debug.Log(selectedLevelItem.imageName);
            // Update the inventory data
            UpdateInventory(newItemInfo);
            ActivateImage();
        }
        else
        {
            Debug.LogError("No items found in the JSON file.");
        }
    }

    /// <summary>
    /// ///////////////////////////////////////////////////////////////////////////////////////////
    /// <FoodCollection>
    private void LoadFoodCollect()
    {
        string jsonFilePath = Path.Combine(Application.persistentDataPath, "collectfood.json");
        Debug.Log("파일의 경로는" + jsonFilePath);

        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            foodList = JsonUtility.FromJson<Food>(jsonData);
            Debug.Log("Food JSON 파일 불러오기 성공!");
        }
        else
        {
            Debug.LogWarning("JSON 파일이 존재하지 않습니다.");
            SaveNewData();
        }
    } // Load foodcollection  
    private void SaveNewData()
    {
        string jsonFilePath = Path.Combine(Application.persistentDataPath, "collectfood.json");
        string jsonData = JsonUtility.ToJson(foodList);

        try
        {
            File.WriteAllText(jsonFilePath, jsonData);
            Debug.Log("CollectionFood JSON file saved successfully.");
        }
        catch (IOException e)
        {
            Debug.LogError("Error writing CollectionFood JSON file: " + e.Message);
        }
    } // Save foodcollection
    private void SaveFoodData()
    {
        FoodInformation CollectedItemInfo = new FoodInformation
        {
            Class = selectedLevelItem.myClass,
            Name = selectedLevelItem.name,
            Info = selectedLevelItem.itemText,
            Image = selectedLevelItem.imageName
        };

        Debug.Log(CollectedItemInfo);
        foodList.foods.Add(CollectedItemInfo);

        string jsonFilePath = Path.Combine(Application.persistentDataPath, "collectfood.json");
        string jsonData = JsonUtility.ToJson(foodList);

        try
        {
            File.WriteAllText(jsonFilePath, jsonData);
            ActivateImage();
            Debug.Log("Inventory JSON file saved successfully.");
        }
        catch (IOException e)
        {
            Debug.LogError("Error writing inventory JSON file: " + e.Message);
        }
    } // Save the food in foodcollection

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

[System.Serializable]
public class Food
{
    public List<FoodInformation> foods;
}

[System.Serializable]
public class FoodInformation
{
    public string Class;
    public string Name;
    public string Image;
    public string Info;
}