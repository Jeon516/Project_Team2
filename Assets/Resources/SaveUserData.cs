using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class SaveUserData : MonoBehaviour
{
    public InputField inputField;
    public Button saveButton;
    private string PlayerName = "Default";

    private void Awake()
    {
        PlayerPrefs.SetString("Player", PlayerName);
        saveButton.onClick.AddListener(SaveData);
        saveButton.interactable = false;
        inputField.onValueChanged.AddListener(veri);
    }

    private void SaveData()
    {
        AudioManager.Instance.PlaySFX("ButtonClick"); //Play SFX
        string PlayerName = inputField.text;
        PlayerPrefs.SetString("Player", PlayerName);
        PlayerPrefs.Save();      
    }
    public void veri(string text)
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

}