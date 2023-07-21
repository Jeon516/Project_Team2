using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class LevelData
{
    public string level;
    public float probability;
    public string jsonFileName; // Name of the JSON file (without extension)
}

public class Order_common_food : MonoBehaviour
{
    public TextAsset levelData;
    public LevelData[] levels;
    public Button selectionButton;
    public Image selectedImage; // Reference to the UI Image component
    public Image foodImage; // Reference to the UI Image component for Food Image
    public Text SelectedName; // Reference to the UI Text component for Selected Name
    public Text SelectedInfo; // Reference to the UI Text component for Selected Info
    public Image OpenImage; // Reference to the UI Image component for Open Image

    private Dictionary<string, int> inventory; // Dictionary to store inventory data

    private void Start()
    {
        ParseLevelData();

        selectionButton.onClick.AddListener(StartSelection);

        // Initialize inventory dictionary
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
                    if (inventory.ContainsKey(selectedItem.name))
                    {
                        inventory[selectedItem.name]++;
                    }
                    else
                    {
                        inventory[selectedItem.name] = 1;
                    }
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

    private void SaveInventoryToJson()
    {
        string jsonFilePath = Application.persistentDataPath + "/inventory.json";
        string jsonData = JsonUtility.ToJson(inventory);

        // Write the inventory data to the JSON file
        File.WriteAllText(jsonFilePath, jsonData);
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
