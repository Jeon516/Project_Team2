using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{
    public string name;
    public int dia;
    public List<string> dog_list;
    public List<string> food_list;
}

public class UserDataManager : MonoBehaviour
{
    private UserData userData;
    private string savePath; // JSON FILE PATH

    private void Awake()
    {
        savePath = "UserData";
    }

    private void Start()
    {
        // USER_DATA_SETTING(DEFAULT)
        userData = new UserData();
        userData.name = "DEFAULT";
        userData.dia = 0;
        userData.dog_list = new List<string>();
        userData.food_list = new List<string>();

        // SAVE_USER_DATA
        SaveUserData(userData);

        // LOAD_USER_DATA
        UserData loadedData = LoadUserData();
        if (loadedData != null)
        {
            Debug.Log("Loaded name: " + loadedData.name);
            Debug.Log("Loaded dia: " + loadedData.dia);
            Debug.Log("Loaded dog_list: " + string.Join(", ", loadedData.dog_list));
            Debug.Log("Loaded food_list: " + string.Join(", ", loadedData.food_list));
            // ADDITIONAL CODE PROGRAMMER SHOULD INPUT
        }
        else
        {
            Debug.Log("Failed to load user data.");
        }
    }

    private void SaveUserData(UserData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(savePath, json);
    }

    private UserData LoadUserData()
    {
        if (PlayerPrefs.HasKey(savePath))
        {
            string json = PlayerPrefs.GetString(savePath);
            return JsonUtility.FromJson<UserData>(json);
        }
        return null;
    }
}
