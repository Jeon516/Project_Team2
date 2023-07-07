using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LevelData
{
    public string level;
    public float probability;
    public string jsonFilePath;
}

public class FirstProbabilitySetting : MonoBehaviour
{
    public TextAsset levelData;
    public LevelData[] levels;
    public Button selectionButton;

    private void Start()
    {
        ParseLevelData();

        selectionButton.onClick.AddListener(StartSelection);
    }

    private void ParseLevelData()
    {
        if (levelData != null)
        {
            ProbabilityData data = JsonUtility.FromJson<ProbabilityData>(levelData.text);
            levels = data.levels;

            for (int i = 0; i < levels.Length; i++)
            {
                string jsonFilePath = "JsonFiles/" + levels[i].level;
                levels[i].jsonFilePath = jsonFilePath;
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
            string jsonFilePath = selectedLevelData.jsonFilePath;
            TextAsset selectedJsonFile = Resources.Load<TextAsset>(jsonFilePath);

            if (selectedJsonFile != null)
            {
                LoadJson(selectedJsonFile);
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

    private void LoadJson(TextAsset jsonFile)
    {
        if (jsonFile != null)
        {
            string jsonText = jsonFile.text;
            ItemDataList itemList = JsonUtility.FromJson<ItemDataList>(jsonText);

            if (itemList != null && itemList.items.Length > 0)
            {
                ItemData selectedItem = itemList.items[Random.Range(0, itemList.items.Length)];
                Debug.Log("Selected Item: " + selectedItem.name);
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
}
