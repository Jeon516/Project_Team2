using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class SaveUserData : MonoBehaviour
{
    public InputField inputField;
    public Button saveButton;

    private string filePath;
    private string PlayerName;

    private void Awake()
    {
        PlayerName = PlayerPrefs.GetString("Player", "플레이어");
        PlayerPrefs.SetString("Player", PlayerName);
        filePath = "UserData"; // The file name without extension since it's in the Resources folder
        saveButton.onClick.AddListener(SaveData);
    }
    private void OnInputValueChanged(string newText)
    {
        newText = newText.Replace(" ", "");
        if(newText != null)
        {
            inputField.text = newText;
        }
    }

    private void SaveData()
    {
        AudioManager.Instance.PlaySFX("ButtonClick"); //Play SFX

        UserData userData = LoadData();

        if (userData != null)
        {
            if(inputField.text != null)
                {

                    OnInputValueChanged(inputField.text);
                    userData.name = inputField.text;
                    PlayerPrefs.SetString("Player", inputField.text);
                    string jsonData = JsonUtility.ToJson(userData);
                    File.WriteAllText(Path.Combine(Application.dataPath, "Resources/UserData.json"), jsonData);
                    Debug.Log("Data saved to: " + filePath);
                }
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
