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
    public string usingItem;
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

    private int firstStatType;
    private int secondStatType;
    private int firstStatValue;
    private int secondStatValue;

    private Inventory inventoryData;

    void OnEnable()
    {
        StartCoroutine(LoadDataAndUpdateUI());
    }

    void Start()
    {
        Debug.Log("업데이트 되었습니다");
        for (int i = 0; i < uiButtonList.Count; i++)
        {
            int index = i;
            uiButtonList[i].onClick.AddListener(() => OnButtonClicked(index));
        }

        yesButton.onClick.AddListener(OnYesButtonClicked);
        noButton.onClick.AddListener(OnNoButtonClicked);
    }

    private IEnumerator LoadDataAndUpdateUI()
    {
        yield return LoadDataFromJsonFile();
        Debug.Log("json 파일에 접속되었습니다.");
        UpdateUI();
    }

    private string GetCorrectedFilePath(string originalPath)
    {
        return originalPath.Replace("\\", "/");
    }

    private IEnumerator LoadDataFromJsonFile()
    {
        string jsonFileName = "inventory.json";
        jsonFilePath = Path.Combine(Application.persistentDataPath, jsonFileName);

        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            inventoryData = JsonUtility.FromJson<Inventory>(jsonData);
            Debug.Log("JSON 파일 불러오기 성공!");
            Debug.Log(jsonData);
        }
        else
        {
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
                InvenData item = inventoryData.itemList[i];
                string imagePath = "Image/Food/" + item.myClass + "/" + item.imageName;
                firstStatType = item.firstStatType;
                firstStatValue = item.firstStatValue;
                secondStatType = item.secondStatType;
                secondStatValue = item.secondStatValue;
                Sprite sprite = Resources.Load<Sprite>(imagePath);
                if (sprite != null)
                {
                    uiImageList[i].sprite = sprite;
                }
                else
                {
                    Debug.LogError("Image not found at path: " + imagePath);
                }

                uiTextList[i].text = "" + item.quantity;
                Debug.Log(item.imageName + "이 있습니다");
            }
            else
            {
                uiImageList[i].sprite = null;
                uiTextList[i].text = string.Empty;
            }
        }

        UpdateUIItemText();

        Debug.Log("UI가 업데이트되었습니다");
    }

    private void OnButtonClicked(int index)
    {
        inventoryData.usingItem = (index >= 0 && index < inventoryData.itemList.Count) ? inventoryData.itemList[index].itemName : string.Empty;
        UpdateUIItemText();

        if (index >= 0 && index < uiImageList.Count && index < inventoryData.itemList.Count)
        {
            uiItemImage.sprite = uiImageList[index].sprite;
        }
        else
        {
            uiItemImage.sprite = null;
        }
    }

    private void OnYesButtonClicked()
    {
        inventoryData.usingItem = string.Empty;
        UpdateUI();
        StatChange();
        Debug.Log(firstStatType + "에서 " + firstStatValue + "만큼 증가하였습니다");
        Debug.Log(secondStatType + "에서 " + secondStatValue + "만큼 증가하였습니다");
    }

    private void OnNoButtonClicked()
    {
        inventoryData.usingItem = string.Empty;
        UpdateUI();
    }

    private void UpdateUIItemText()
    {
        if (!string.IsNullOrEmpty(inventoryData.usingItem))
        {
            InvenData item = inventoryData.itemList.Find(x => x.itemName == inventoryData.usingItem);
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
        int[] StatType = { firstStatType, secondStatType };
        int[] StatValue = { firstStatValue, secondStatValue };
        //data connect

        for (int i = 0; i < 2; i++)
        {
            Debug.Log(StatType[i]);
            if (StatType[i] == 101)
            {
                if (StatValue[i] > 0)
                {
                    if (PlayerPrefs.GetInt("Energy") < 4)
                    {
                        PlayerPrefs.SetInt("Energy", PlayerPrefs.GetInt("Energy") + StatValue[i]);
                        PlayerPrefs.SetFloat("EnergyX", PlayerPrefs.GetInt("EnergyX") - 111 * StatValue[i]);
                    }
                }
                else
                {
                    if (PlayerPrefs.GetInt("Energy") > -4)
                    {
                        PlayerPrefs.SetInt("Energy", PlayerPrefs.GetInt("Energy") + StatValue[i]);
                        PlayerPrefs.SetFloat("EnergyX", PlayerPrefs.GetInt("EnergyX") - 111 * StatValue[i]);
                    }
                }
            }
            else if (StatType[i] == 102)
            {
                if (StatValue[i] > 0)
                {
                    if (PlayerPrefs.GetInt("Sociality") < 4)
                    {
                        PlayerPrefs.SetInt("Sociality", PlayerPrefs.GetInt("Sociality") + StatValue[i]);
                        PlayerPrefs.SetFloat("SocialityX", PlayerPrefs.GetInt("SocialityX") - 111 * StatValue[i]);
                    }
                }
                else
                {
                    if (PlayerPrefs.GetInt("Sociality") > -4)
                    {
                        PlayerPrefs.SetInt("Sociality", PlayerPrefs.GetInt("Sociality") + StatValue[i]);
                        PlayerPrefs.SetFloat("SocialityX", PlayerPrefs.GetInt("SocialityX") - 111 * StatValue[i]);
                    }
                }
            }
            else if (StatType[i] == 103)
            {
                if (StatValue[i] > 0)
                {
                    if (PlayerPrefs.GetInt("Deliberation") < 4)
                    {
                        PlayerPrefs.SetInt("Deliberation", PlayerPrefs.GetInt("Deliberation") + StatValue[i]);
                        PlayerPrefs.SetFloat("DeliberationX", PlayerPrefs.GetInt("DeliberationX") - 111 * StatValue[i]);
                    }
                }
                else
                {
                    if (PlayerPrefs.GetInt("Deliberation") > -4)
                    {
                        PlayerPrefs.SetInt("Deliberation", PlayerPrefs.GetInt("Deliberation") + StatValue[i]);
                        PlayerPrefs.SetFloat("DeliberationX", PlayerPrefs.GetInt("DeliberationX") - 111 * StatValue[i]);
                    }
                }
            }
            else if (StatType[i] == 104)
            {
                if (StatValue[i] > 0)
                {
                    if (PlayerPrefs.GetInt("Curiosoty") < 4)
                    {
                        PlayerPrefs.SetInt("Curiosoty", PlayerPrefs.GetInt("Curiosoty") + StatValue[i]);
                        PlayerPrefs.SetFloat("CuriosotyX", PlayerPrefs.GetInt("CuriosotyX") - 111 * StatValue[i]);
                    }
                }
                else
                {
                    if (PlayerPrefs.GetInt("Curiosoty") > -4)
                    {
                        PlayerPrefs.SetInt("Curiosoty", PlayerPrefs.GetInt("Curiosoty") + StatValue[i]);
                        PlayerPrefs.SetFloat("CuriosotyX", PlayerPrefs.GetInt("CuriosotyX") - 111 * StatValue[i]);
                    }
                }
            }
            else if (StatType[i] == 105)
            {
                if (StatValue[i] > 0)
                {
                    if (PlayerPrefs.GetInt("Love") < 4)
                    {
                        PlayerPrefs.SetInt("Love", PlayerPrefs.GetInt("Love") + StatValue[i]);
                        PlayerPrefs.SetFloat("LoveX", PlayerPrefs.GetInt("LoveX") - 111 * StatValue[i]);
                    }
                }
                else
                {
                    if (PlayerPrefs.GetInt("Love") > -4)
                    {
                        PlayerPrefs.SetInt("Love", PlayerPrefs.GetInt("Love") + StatValue[i]);
                        PlayerPrefs.SetFloat("LoveX", PlayerPrefs.GetInt("LoveX") - 111 * StatValue[i]);
                    }
                }
            }
            else if (StatType[i] == 106)
            {
                PlayerPrefs.SetInt("ActionNum", PlayerPrefs.GetInt("ActionNum") + firstStatValue);
            }
        }
    }
}
