using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;

[System.Serializable]
public class InvenData
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

[System.Serializable]
public class Inventory
{
    public List<InvenData> itemList = new List<InvenData>();
    public string usingItemText;
}

public class InventoryManager : MonoBehaviour
{
    public List<Button> uiButtonList;
    public List<Image> uiImageList;
    public List<Text> uiTextList;
    public Text uiItemTextComponent;
    public Image uiItemImage;
    public Button yesButton;
    public Button noButton;
    public string jsonFilePath = "InventoryData"; // Removed the directory part of the path
    public GameObject[] Exist=new GameObject[20];
    public Text EatConv;

    private InvenData SelectedItem;
    private Inventory inventoryData;
    private int FirstStatType;
    private int SecondStatType;
    private int FirstStatValue;
    private int SecondStatValue;

    void Start()
    {
        inventoryData = new Inventory();
        Debug.Log("업데이트 되었습니다");
        for (int i = 0; i < uiButtonList.Count; i++)
        {
            int index = i;
            uiButtonList[i].onClick.AddListener(() => OnButtonClicked(index));
        }

        yesButton.onClick.AddListener(OnYesButtonClicked);
        noButton.onClick.AddListener(OnNoButtonClicked);

        StartCoroutine(LoadDataAndUpdateUI());
    }
    private IEnumerator LoadDataAndUpdateUI()
    {
        yield return LoadDataFromJsonFile();
        Debug.Log("Inventory json 파일에 접속되었습니다.");
        UpdateUI();
    }

    private string GetCorrectedFilePath(string originalPath)
    {
        return originalPath.Replace("\\", "/");
    }

    private IEnumerator LoadDataFromJsonFile()
    { // 인벤토리 초기화
        string jsonFileName = "inventory.json";
        jsonFilePath = Path.Combine(Application.persistentDataPath, jsonFileName);
        Debug.Log(jsonFilePath);

        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            inventoryData = JsonUtility.FromJson<Inventory>(jsonData);
            Debug.Log("JSON 파일 불러오기 성공!");
        }
        else
        {
            string jsonData = JsonUtility.ToJson(inventoryData, true);
            File.WriteAllText(jsonFilePath, jsonData);
            Debug.LogWarning("JSON 파일이 존재하지 않습니다.");
        }

        yield return null;
    }

    private void UpdateUI()
    {
        if (uiImageList == null || uiTextList == null || uiItemTextComponent == null || uiItemImage == null)
        {
            Debug.LogError("UI elements not assigned in the Inspector!");
            return;
        }

        for (int i = 0; i < uiImageList.Count; i++)
        {
            if (i < inventoryData.itemList.Count)
            {
                Exist[i].SetActive(true);
                InvenData item = inventoryData.itemList[i];
                string imagePath = "Image/Food/" + item.myClass + "/" + item.imageName;
                Sprite sprite = Resources.Load<Sprite>(imagePath);
                if (sprite != null)
                {
                    uiImageList[i].sprite = sprite;
                }
                else
                {
                    Debug.LogError("Image not found at path: " + imagePath);
                    // If the sprite is not found, you may want to display a default image here.
                }

                uiTextList[i].text = "" + item.quantity;
                Debug.Log(item.imageName + "이 있습니다");
            }
            else
            {
                Debug.Log("아이템이 없습니다");
                Exist[i].SetActive(false);
                uiImageList[i].sprite = null;
                uiTextList[i].text = string.Empty;
            }
        }

        UpdateUIItemText();

        Debug.Log("UI가 업데이트되었습니다");
    }

    private void OnButtonClicked(int index)
    {
        if (index >= 0 && index < uiImageList.Count)
        {
            InvenData item = inventoryData.itemList[index];
            inventoryData.usingItemText = item.itemName; // Set the currently selected item's name

            string imagePath = "Image/Food/" + item.myClass + "/" + item.imageName;
            Sprite sprite = Resources.Load<Sprite>(imagePath);
            if (sprite != null)
            {
                uiItemImage.sprite = sprite;
            }
            else
            {
                Debug.LogError("Image not found at path: " + imagePath);
                // If the sprite is not found, you may want to display a default image here.
            }

            FirstStatType=item.firstStatType;
            FirstStatValue = item.firstStatValue;
            SecondStatType = item.secondStatType;
            SecondStatValue = item.secondStatValue;
            SelectedItem = item;

            // Update the UI item text based on the selected item
            UpdateUIItemText();
        }
        else
        {
            // If the clicked index is out of range, reset the selected item
            uiItemImage.sprite = null;
            inventoryData.usingItemText = string.Empty;
            UpdateUIItemText();
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

    private void OnYesButtonClicked()
    {
        EatConv.text = SelectedItem.conv;
        gameObject.SetActive(false);
        inventoryData.itemList.Remove(SelectedItem);
        UpdateUI();
        SaveInventoryToJson();
        StatChange();
    }

    private void OnNoButtonClicked()
    {
        inventoryData.itemList.Remove(SelectedItem);
        UpdateUI();
        SaveInventoryToJson();
    }

    private void UpdateUIItemText()
    {
        if (!string.IsNullOrEmpty(inventoryData.usingItemText))
        {
            InvenData item = inventoryData.itemList.Find(x => x.itemName == inventoryData.usingItemText);
            if (item != null)
            {
                uiItemTextComponent.text = item.itemText;
            }
            else
            {
                uiItemTextComponent.text = string.Empty;
            }
        }
        else
        {
            uiItemTextComponent.text = string.Empty;
        }
    }

    private void StatChange()
    {
        int[] StatType = { FirstStatType, SecondStatType };
        int[] StatValue = { FirstStatValue, SecondStatValue };

        int Energy = PlayerPrefs.GetInt("Energy");
        float EnergyX = PlayerPrefs.GetFloat("EnergyX");

        int Sociality = PlayerPrefs.GetInt("Sociality");
        float SocialityX = PlayerPrefs.GetFloat("SocialityX");

        int Deliberation = PlayerPrefs.GetInt("Deliberation");
        float DeliberationX = PlayerPrefs.GetFloat("DeliberationX");

        int Curiosoty = PlayerPrefs.GetInt("Curiosoty");
        float CuriosotyX = PlayerPrefs.GetFloat("CuriosotyX");

        int Love = PlayerPrefs.GetInt("Love");
        float LoveX = PlayerPrefs.GetFloat("LoveX");

        int ActionNum = PlayerPrefs.GetInt("ActionNum");

        Debug.Log(Energy+" : Energy입니다.");
        Debug.Log(Sociality+" : Sociality입니다.");
        Debug.Log(Deliberation+ " : Deliberation입니다");
        Debug.Log(Curiosoty + " : Curiosoty입니다");
        Debug.Log(Love + " : Love입니다");
        //data connect

        for (int i = 0; i < 2; i++)
        {
            Debug.Log(StatType[i]+"이 "+StatValue[i]+"만큼 늘어났습니다.");
            if (StatType[i] == 101)
            {
                if (StatValue[i] > 0)
                {
                    if (Energy < 4)
                    {
                        PlayerPrefs.SetInt("Energy", Energy + StatValue[i]);
                        PlayerPrefs.SetFloat("EnergyX", EnergyX - 111 * StatValue[i]);
                    }
                }
                else
                {
                    if (Energy > -4)
                    {
                        PlayerPrefs.SetInt("Energy", Energy + StatValue[i]);
                        PlayerPrefs.SetFloat("EnergyX", EnergyX - 111 * StatValue[i]);
                    }
                }
            }
            else if (StatType[i] == 102)
            {
                if (StatValue[i] > 0)
                {
                    if (Sociality < 4)
                    {
                        PlayerPrefs.SetInt("Sociality", Sociality + StatValue[i]);
                        PlayerPrefs.SetFloat("SocialityX", SocialityX - 111 * StatValue[i]);
                    }
                }
                else
                {
                    if (Sociality > -4)
                    {
                        PlayerPrefs.SetInt("Sociality", Sociality + StatValue[i]);
                        PlayerPrefs.SetFloat("SocialityX", SocialityX - 111 * StatValue[i]);
                    }
                }
            }
            else if (StatType[i] == 103)
            {
                if (StatValue[i] > 0)
                {
                    if (Deliberation < 4)
                    {
                        PlayerPrefs.SetInt("Deliberation", Deliberation + StatValue[i]);
                        PlayerPrefs.SetFloat("DeliberationX", DeliberationX - 111 * StatValue[i]);
                    }
                }
                else
                {
                    if (Deliberation > -4)
                    {
                        PlayerPrefs.SetInt("Deliberation", Deliberation + StatValue[i]);
                        PlayerPrefs.SetFloat("DeliberationX", DeliberationX - 111 * StatValue[i]);
                    }
                }
            }
            else if (StatType[i] == 104)
            {
                if (StatValue[i] > 0)
                {
                    if (Curiosoty < 4)
                    {
                        PlayerPrefs.SetInt("Curiosoty", Curiosoty + StatValue[i]);
                        PlayerPrefs.SetFloat("CuriosotyX", CuriosotyX - 111 * StatValue[i]);
                    }
                }
                else
                {
                    if (Curiosoty > -4)
                    {
                        PlayerPrefs.SetInt("Curiosoty", Curiosoty + StatValue[i]);
                        PlayerPrefs.SetFloat("CuriosotyX", CuriosotyX - 111 * StatValue[i]);
                    }
                }
            }
            else if (StatType[i] == 105)
            {
                if (StatValue[i] > 0)
                {
                    if (Love < 4)
                    {
                        PlayerPrefs.SetInt("Love", Love + StatValue[i]);
                        PlayerPrefs.SetFloat("LoveX", LoveX - 111 * StatValue[i]);
                    }
                }
                else
                {
                    if (Love > -4)
                    {
                        PlayerPrefs.SetInt("Love", Love + StatValue[i]);
                        PlayerPrefs.SetFloat("LoveX", LoveX - 111 * StatValue[i]);
                    }
                }
            }
            else if (StatType[i] == 106)
            {
                PlayerPrefs.SetInt("ActionNum", ActionNum + StatValue[i]);
            }
        }
    }
}
