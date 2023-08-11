using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DogName : MonoBehaviour
{
    public InputField inputField;
    public Button saveButton; // 인스펙터에서 버튼을 할당하기 위한 변수
    public CollectedDogData CollectedDogDatas;
    private ending_controller endcontrol;

    private void Start()
    {
        endcontrol = FindObjectOfType<ending_controller>();
        saveButton.onClick.AddListener(OnSaveButtonClick);
        saveButton.interactable = false;

        // 입력 필드의 "On Value Changed" 이벤트에 OnInputFieldValueChanged 메서드를 연결합니다.
        inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
    }

    public void OnInputFieldValueChanged(string text)
    {
        if (text.Length >= 1)
        {
            saveButton.interactable = true;
        }
        else
        {
            saveButton.interactable = false;
        }
    }

    public void OnSaveButtonClick()
    {
        string inputValue = inputField.text;
        PlayerPrefs.SetString("DogName", inputValue);
        PlayerPrefs.Save(); // 변경사항을 저장합니다.
        endcontrol.myButton.gameObject.SetActive(true);
        endcontrol.image5.gameObject.SetActive(false);
        endcontrol.image6.gameObject.SetActive(true);

        LoadDogCollect();
        SaveDogData(); // Enroll the User's Dog
    }

    private void LoadDogCollect()
    {
        string jsonFilePath = Path.Combine(Application.persistentDataPath, "collecteddog.json");
        Debug.Log("파일의 경로는" + jsonFilePath);

        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            CollectedDogDatas = JsonUtility.FromJson<CollectedDogData>(jsonData);
            Debug.Log("JSON 파일 불러오기 성공!");
        }
        else
        {
            Debug.LogWarning("JSON 파일이 존재하지 않습니다.");
        }
    }

    private void SaveDogData()
    {
        string inputValue = PlayerPrefs.GetString("DogName");

        DogInfo CollectedDogInfo = new DogInfo
        {
            DogName = inputValue,
            DogInformation = "나의 영원한 친구",
            DogImageName = "User's dog"
        };

        CollectedDogDatas.collectedDogData.Add(CollectedDogInfo);

        string jsonFilePath = Path.Combine(Application.persistentDataPath, "collecteddog.json");
        string jsonData = JsonUtility.ToJson(CollectedDogDatas);

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
}
