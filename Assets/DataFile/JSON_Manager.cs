using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class JSON_Manager : MonoBehaviour
{
    public string jsonFilePath = "Assets/DataFile/foodinfo.json"; // JSON Data Path

    void Start()
    {
        // JSON Load
        string jsonText = LoadJSONFromFile(jsonFilePath);

        if (string.IsNullOrEmpty(jsonText))
        {
            Debug.Log("Error loading JSON file.");
            return;
        }

        // JSON to Object
        MyDataObject dataObject = JsonUtility.FromJson<MyDataObject>(jsonText);

        // Data loging
        Debug.Log("Class: " + dataObject.myClass);
        Debug.Log("Sort: " + dataObject.sort);
        Debug.Log("Name: " + dataObject.name);
        Debug.Log("Item Text: " + dataObject.itemText);
        Debug.Log("Conv: " + dataObject.conv);
        Debug.Log("1st Stat Type: " + dataObject.firstStatType);
        Debug.Log("1st Stat Value: " + dataObject.firstStatValue);
        Debug.Log("2nd Stat Type: " + dataObject.secondStatType);
        Debug.Log("2nd Stat Value: " + dataObject.secondStatValue);
        Debug.Log("Probability: " + dataObject.probability);

        // Additional Logic Adjustment
    }

    // Load JSON From File
    string LoadJSONFromFile(string filePath)
    {
        string jsonText = null;

        try
        {
            string fullPath = Path.Combine(Application.streamingAssetsPath, filePath);

            if (File.Exists(fullPath))
            {
                jsonText = File.ReadAllText(fullPath);
            }
            else
            {
                Debug.Log("File does not exist: " + filePath);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("Error loading JSON file: " + e.Message);
        }

        return jsonText;
    }
}

[System.Serializable]
public class MyDataObject
{
    public string myClass;
    public string sort;
    public string name;
    public string itemText;
    public string conv;
    public string firstStatType;
    public int firstStatValue;
    public string secondStatType;
    public int secondStatValue;
    public float probability;
}
