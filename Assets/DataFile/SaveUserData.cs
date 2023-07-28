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
        filePath = "UserData"; // The file name without extension since it's in the Resources folder
        saveButton.onClick.AddListener(SaveData);
    }

    private void SaveData()
    {
        AudioManager.Instance.PlaySFX("ButtonClick"); //Play SFX

        // Load existing data if available
        UserData userData = LoadData();

        // Update data with new input
        if (userData != null)
        {
            userData.name = inputField.text;

            // Convert to JSON
            string jsonData = JsonUtility.ToJson(userData);

            // Save data to file
            File.WriteAllText(Path.Combine(Application.dataPath, "Resources/UserData.json"), jsonData);

            Debug.Log("Data saved to: " + filePath);
        }
        else
        {
            Debug.LogError("Failed to load existing data.");
        }
    }

    private UserData LoadData()
    {
        TextAsset jsonData = Resources.Load<TextAsset>(filePath);
        if (jsonData != null)
        {
            UserData userData = JsonUtility.FromJson<UserData>(jsonData.text);
            return userData;
        }
        else
        {
            Debug.LogWarning("Data file does not exist. Creating a new one.");
            return new UserData();
        }
    }
}
