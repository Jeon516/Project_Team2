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
    public List<Button> uiButtonList; // 버튼 리스트 추가
    public List<Image> uiImageList; // UI 이미지 리스트
    public List<Text> uiTextList;   // UI 텍스트 리스트
    public Text uiItemText;   // 추가: UIText 요소
    public Image uiItemImage; // 추가: UIImage 요소
    public Button yesButton; // Yes 버튼 추가
    public Button noButton;  // No 버튼 추가

    public string jsonFilePath = "JsonFiles/Inventory/InventoryData"; // 파일 경로

#if UNITY_EDITOR
    private FileSystemWatcher fileWatcher;
#endif

    private Inventory inventoryData; // 파싱된 데이터를 저장하는 변수

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
        // 버튼이 클릭되면 해당 아이템을 usingItem으로 설정
        if (index >= 0 && index < inventoryData.itemList.Count) // Fix potential index out of range
        {
            inventoryData.usingItem = inventoryData.itemList[index].itemName;
        }
        else
        {
            inventoryData.usingItem = string.Empty;
        }

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
        // 현재 usingItem에 해당하는 아이템 찾기
        InvenData item = inventoryData.itemList.Find(x => x.itemName == inventoryData.usingItem);

        if (item != null)
        {
            // 아이템 삭제
            inventoryData.itemList.Remove(item);
            inventoryData.usingItem = string.Empty; // 추가: usingItem 초기화
            // JSON 파일에 저장
            SaveDataToJsonFile();
            // UI 업데이트
            UpdateUI();
        }
    }

    private void OnNoButtonClicked()
    {
        // 현재 usingItem에 해당하는 아이템 찾기
        InvenData item = inventoryData.itemList.Find(x => x.itemName == inventoryData.usingItem);

        if (item != null)
        {
            // 아이템 개수가 1 이상일 경우에만 개수를 감소시킴
            if (item.quantity >= 2)
            {
                item.quantity--;
                // JSON 파일에 저장
                SaveDataToJsonFile();
            }
            // 추가: usingItem 초기화
            inventoryData.usingItem = string.Empty;
            // UI 업데이트
            UpdateUI();
        }
    }

    private void SaveDataToJsonFile()
    {
        string jsonData = JsonUtility.ToJson(inventoryData, true);
        string filePath = Path.Combine(Application.dataPath, "Resources", jsonFilePath + ".json");
        File.WriteAllText(filePath, jsonData);
    }

    // 추가: UIText 요소 초기화
    private void InitializeUIText()
    {
        uiItemText.text = string.Empty;
    }

    // 추가: UIText 요소 업데이트
    private void UpdateUIItemText()
    {
        if (inventoryData.usingItem != null && inventoryData.usingItem != string.Empty)
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
}
