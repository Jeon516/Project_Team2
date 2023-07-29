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
    } // 팝업창 없애기
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
            // 파일이 존재하면 파일 내용을 읽어옵니다.
            string jsonData = File.ReadAllText(jsonFilePath);

            // JSON 데이터를 InventoryData 객체로 역직렬화합니다.
            InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(jsonData);

            // 데이터를 삭제할 로직을 추가합니다.
            inventoryData.itemList.Clear();

            // 수정된 데이터를 다시 JSON 형식으로 직렬화합니다.
            string updatedJsonData = JsonUtility.ToJson(inventoryData);

            // 수정된 데이터를 다시 파일에 씁니다.
            File.WriteAllText(jsonFilePath, updatedJsonData);

            Debug.Log("Inventory JSON data cleared.");
        }
        else
        {
            Debug.Log("Inventory JSON file not found.");
        }
    }
}
