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
    public Text uiItemText;
    public Image uiItemImage;
    public Button yesButton;
    public Button noButton;

    public string jsonFilePath = "JsonFiles/Inventory/InventoryData";

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
        if (uiImageList == null || uiTextList == null || uiItemText == null || uiItemImage == null)
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
                uiTextList[i].text = "Quantity: " + item.quantity;
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

        // 추가: usingItem의 인덱스에 해당하는 itemText 값을 uiItemText 요소에 할당
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
    }

    private void OnNoButtonClicked()
    {
        // ... (other parts of the code)

        // 추가: usingItem 초기화
        inventoryData.usingItem = string.Empty;
        // UI 업데이트
        UpdateUI();
    }

    // 추가: UIText 요소 초기화
    private void InitializeUIText()
    {
        uiItemText.text = string.Empty;
    }

    // 추가: UIText 요소 업데이트
    private void UpdateUIItemText()
    {
        if (!string.IsNullOrEmpty(inventoryData.usingItem))
        {
            InvenData item = inventoryData.itemList.Find(x => x.itemName == inventoryData.usingItem);
            if (item != null)
            {
                uiItemText.text = item.itemText;
            }
            else
            {
                uiItemText.text = string.Empty;
            }
        }
        else
        {
            uiItemText.text = string.Empty;
        }
    }

    private void SaveDataToJsonFile()
    {
        string jsonData = JsonUtility.ToJson(inventoryData, true);
        string filePath = Path.Combine(Application.dataPath, "Resources", jsonFilePath + ".json");
        File.WriteAllText(filePath, jsonData);
    }
}
