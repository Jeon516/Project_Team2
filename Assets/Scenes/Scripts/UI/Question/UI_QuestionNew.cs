using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class UI_QuestionNew : MonoBehaviour
{
    private string jsonFilePath;

    private void Start()
    {
        string jsonFileName = "inventory.json";
        jsonFilePath = Path.Combine(Application.persistentDataPath, jsonFileName);
    }
    public void OnClick_NextNew()
    {
        Restart();
        ClearInventoryData();
        SceneManager.LoadScene("Heaven");
    } // New Start
    public void OnClick_Cancel()
    {
        gameObject.SetActive(false);
    } // �˾�â ���ֱ�
    private void Restart()
    {
        PlayerPrefs.SetInt("Day", 0);
        PlayerPrefs.SetInt("ActionNum", 1000);
        PlayerPrefs.SetInt("Gold", 1000);
        PlayerPrefs.SetInt("Interaction", 0);
        PlayerPrefs.SetInt("IsRandomFree", 0);
        PlayerPrefs.SetInt("IsHeaven", 1);

        PlayerPrefs.SetInt("Energy", 0);
        PlayerPrefs.SetFloat("EnergyX", 0);

        PlayerPrefs.SetInt("Sociality", 0);
        PlayerPrefs.SetFloat("SocialityX", 0);

        PlayerPrefs.SetInt("Deliberation", 0);
        PlayerPrefs.SetFloat("DeliberationX", 0);

        PlayerPrefs.SetInt("Curiosoty", 0);
        PlayerPrefs.SetFloat("CuriosotyX", 0);

        PlayerPrefs.SetInt("Love", 0);
        PlayerPrefs.SetFloat("LoveX", 0);
    } // Data Initialize

    private void ClearInventoryData()
    {
        if (File.Exists(jsonFilePath))
        {
            // ������ �����ϸ� ���� ������ �о�ɴϴ�.
            string jsonData = File.ReadAllText(jsonFilePath);

            // JSON �����͸� InventoryData ��ü�� ������ȭ�մϴ�.
            InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(jsonData);

            // �����͸� ������ ������ �߰��մϴ�.
            inventoryData.itemList.Clear();

            // ������ �����͸� �ٽ� JSON �������� ����ȭ�մϴ�.
            string updatedJsonData = JsonUtility.ToJson(inventoryData);

            // ������ �����͸� �ٽ� ���Ͽ� ���ϴ�.
            File.WriteAllText(jsonFilePath, updatedJsonData);

            Debug.Log("Inventory JSON data cleared.");
        }
        else
        {
            Debug.Log("Inventory JSON file not found.");
        }
    }
}
