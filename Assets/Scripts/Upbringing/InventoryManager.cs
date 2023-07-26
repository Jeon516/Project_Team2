using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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
    public Text uiItemTextComponent; // Renamed variable to avoid naming conflicts
    public Image uiItemImage;
    public Button yesButton;
    public Button noButton;

    public string jsonFilePath = "JsonFiles/Inventory/InventoryData";

    private int firstStatType;
    private int secondStatType;
    private int firstStatValue;
    private int secondStatValue;

#if UNITY_EDITOR
    private FileSystemWatcher fileWatcher;
#endif

    private Inventory inventoryData;

    void OnEnable()
    {
        // JSON 파일을 읽어서 데이터를 파싱하고 UI 업데이트
        StartCoroutine(LoadDataAndUpdateUI());

#if UNITY_EDITOR
        // 활성화될 때마다 JSON 파일의 변경을 감지하여 UI 업데이트
        StartFileWatcher();
#endif
    }

    void OnDisable()
    {
#if UNITY_EDITOR
        // 비활성화될 때 파일 감시 종료
        StopFileWatcher();
#endif
    }

    void Start()
    {
        // 버튼 리스트에 이벤트 리스너 추가
        for (int i = 0; i < uiButtonList.Count; i++)
        {
            int index = i; // 이벤트 클로저에 사용할 인덱스 변수
            uiButtonList[i].onClick.AddListener(() => OnButtonClicked(index));
        }

        // YesButton과 NoButton에 이벤트 리스너 추가
        yesButton.onClick.AddListener(OnYesButtonClicked);
        noButton.onClick.AddListener(OnNoButtonClicked);
    }

#if UNITY_EDITOR
    private void StartFileWatcher()
    {
        string filePath = Path.Combine(Application.dataPath, "Resources", jsonFilePath + ".json");
        fileWatcher = new FileSystemWatcher(Path.GetDirectoryName(filePath));
        fileWatcher.Filter = Path.GetFileName(filePath);
        fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
        fileWatcher.Changed += OnFileChanged;
        fileWatcher.EnableRaisingEvents = true;
    }

    private void StopFileWatcher()
    {
        if (fileWatcher != null)
        {
            // 파일 감시 정리
            fileWatcher.Changed -= OnFileChanged;
            fileWatcher.Dispose();
            fileWatcher = null;
        }
    }

    private void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        // 파일이 변경되면 데이터를 다시 파싱하고 UI를 업데이트
        StartCoroutine(LoadDataAndUpdateUI());
    }
#endif

    private IEnumerator LoadDataAndUpdateUI()
    {
        // 비동기적으로 JSON 파일을 읽어서 데이터를 파싱
        yield return LoadDataFromJsonFile();

        // UI 업데이트
        UpdateUI();
    }

    private async Task LoadDataFromJsonFile()
    {
        string filePath = Path.Combine(Application.dataPath, "Resources", jsonFilePath + ".json");

        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string jsonData = await reader.ReadToEndAsync();
                inventoryData = JsonUtility.FromJson<Inventory>(jsonData);
            }
        }
        else
        {
            Debug.LogError("JSON file not found at path: " + jsonFilePath);
        }
    }

    private void UpdateUI()
    {
        // Handle null check for UI elements before updating UI
        if (uiImageList == null || uiTextList == null || uiItemTextComponent == null || uiItemImage == null)
        {
            Debug.LogError("UI elements not assigned in the Inspector!");
            return;
        }

        // 파싱된 데이터를 기반으로 UI 업데이트
        for (int i = 0; i < uiImageList.Count; i++)
        {
            if (i < inventoryData.itemList.Count)
            {
                InvenData item = inventoryData.itemList[i];

                // 2. UI이미지 리스트에는 Assets/Resources/Image/Food/[item.MyClass]/[item.imageName].PNG가 할당되어야 함
                string imagePath = "Image/Food/" + item.myClass + "/" + item.imageName;
                //stat
                firstStatType = item.firstStatType;
                firstStatValue = item.firstStatValue;
                secondStatType = item.secondStatType;
                secondStatValue = item.secondStatValue; // Get data
                Sprite sprite = Resources.Load<Sprite>(imagePath);
                if (sprite != null)
                {
                    // 3. 이미지 리스트 인덱스가 n이면 이미지 리스트 n번째 자리에 할당
                    uiImageList[i].sprite = sprite;
                }
                else
                {
                    Debug.LogError("Image not found at path: " + imagePath);
                }

                // 4. 텍스트 리스트에 [item.quantity]를 할당
                uiTextList[i].text = "" + item.quantity;
            }
            else
            {
                // 리스트 인덱스가 아이템 개수보다 많으면 비우기
                uiImageList[i].sprite = null;
                uiTextList[i].text = string.Empty;
            }
        }

        // 추가: UIText 요소 업데이트
        UpdateUIItemText();
    }

    private void OnButtonClicked(int index)
    {
        // ... (other parts of the code)

        // 아이템 사용 중인지 여부를 확인하고 usingItem 값을 설정
        inventoryData.usingItem = (index >= 0 && index < inventoryData.itemList.Count) ? inventoryData.itemList[index].itemName : string.Empty;

        // 추가: usingItem의 인덱스에 해당하는 itemText 값을 uiItemTextComponent 요소에 할당
        UpdateUIItemText();

        // 추가: usingItem의 인덱스에 해당하는 이미지를 uiItemImage 요소에 할당
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
        // ... (other parts of the code)

        // 추가: usingItem 초기화
        inventoryData.usingItem = string.Empty;
        // UI 업데이트
        UpdateUI();
        // StatChange
        StatChange();
        Debug.Log(firstStatType + "에서 " + firstStatValue+"만큼 증가하였습니다");
        Debug.Log(secondStatType + "에서 " + secondStatValue + "만큼 증가하였습니다");
    }

    private void OnNoButtonClicked()
    {
        // ... (other parts of the code)

        // 추가: usingItem 초기화
        inventoryData.usingItem = string.Empty;
        // UI 업데이트
        UpdateUI();
    }

    // 추가: UIText 요소 업데이트
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
                        PlayerPrefs.SetFloat("EnergyX", PlayerPrefs.GetInt("EnergyX") - 111* StatValue[i]);
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
