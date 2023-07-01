using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class SaveUserData : MonoBehaviour
{
    public InputField inputField;
    public Button saveButton;

    private string filePath;

    private void Awake()
    {
        filePath = Path.Combine(Application.dataPath, "DataFile/UserData.json");
        saveButton.onClick.AddListener(SaveData);
    }

    private void SaveData()
    {
        // 데이터를 로드하여 기존 데이터 객체로 역직렬화
        UserData userData = LoadData();

        // 입력받은 데이터를 UserData 객체에 추가
        if (userData != null)
        {
            userData.name = inputField.text;

            // UserData 객체를 JSON 형태로 직렬화
            string jsonData = JsonUtility.ToJson(userData);

            // JSON 데이터를 파일에 저장
            File.WriteAllText(filePath, jsonData);

            Debug.Log("Data saved to: " + filePath);
        }
        else
        {
            Debug.LogError("Failed to load existing data.");
        }
    }

    private UserData LoadData()
    {
        if (File.Exists(filePath))
        {
            // 파일이 존재하면 JSON 데이터를 읽어와서 UserData 객체로 역직렬화
            string jsonData = File.ReadAllText(filePath);
            UserData userData = JsonUtility.FromJson<UserData>(jsonData);
            return userData;
        }
        else
        {
            Debug.LogWarning("Data file does not exist. Creating a new one.");
            return new UserData();
        }
    }
}
