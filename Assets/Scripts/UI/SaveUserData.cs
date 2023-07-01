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
        //Inverse Linear 
        UserData userData = LoadData();

        //Data to UserData
        if (userData != null)
        {
            userData.name = inputField.text;

            //linear
            string jsonData = JsonUtility.ToJson(userData);

            //Save data to file
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
            //inverse linear
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
