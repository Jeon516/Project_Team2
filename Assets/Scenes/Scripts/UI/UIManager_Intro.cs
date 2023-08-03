using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;



public class UIManager_Intro : MonoBehaviour
{
    public GameObject IntroSetting; // 환경 설정
    public GameObject NewQuestion;
    public GameObject NoData;
    private string InventoryjsonFilePath;
     private string CollectedfoodjsonFilePath;
    private string CollectedDogjsonFilePath;
    private string PlayerName;
    private int Day;
    private int IsHeaven;

    private void Awake()
    {
        Day= PlayerPrefs.GetInt("Day", 0);
        PlayerPrefs.SetInt("Day", Day);
        IsHeaven = PlayerPrefs.GetInt("IsHeaven", 1);
        PlayerPrefs.SetInt("IsHeaven", IsHeaven);
        PlayerName = PlayerPrefs.GetString("Player", "플레이어");
        PlayerPrefs.SetString("Player", PlayerName);
    }
    private void Start()
    {
        InventoryjsonFilePath = Path.Combine(Application.persistentDataPath, "inventory.json");
        CollectedfoodjsonFilePath = Path.Combine(Application.persistentDataPath, "collectfood.json");
        CollectedDogjsonFilePath = Path.Combine(Application.persistentDataPath, "collecteddog.json");

        AudioManager.Instance.PlayBGM("Intro");
    }
    public void OnClick_Start()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if ((Day/20 == 0 && Day%20==0)|| (Day / 20 == 0 && Day % 20 == 1))
        {
            Restart();
        }
        else
        {
            NewQuestion.SetActive(true);
        }
    } // 'Game Start'을 누를 때

    public void OnClick_NewStart(bool Yes)
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (Yes)
        {
            Restart();
            PlayerPrefs.SetInt("Day", 0);
            SceneManager.LoadScene("Loading");
        }
        else
        {
            NewQuestion.SetActive(false);
        }
    } // 새로 시작할 것인지

    public void OnClick_Load()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (Day / 20 == 0 && Day % 20 == 0 || Day / 20 == 0 && Day % 20 == 1)
        {
            NoData.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Loading");
        }
    } // 'Load Start'을 누를 때

    public void OnClick_Setting()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        IntroSetting.SetActive(true); // ȯ�漳�� Ű��
    } // 'Setting'을 누를 때
    public void OnClick_Exit()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        Application.Quit(); // 어플 종료
    } // 'Exit'을 누를 때

    private void Restart()
    {
        ClearInventoryData();
        ClearCollectFoodData();
        ClearCollectDogData();
        PlayerPrefs.SetString("Player", "");
        PlayerPrefs.SetInt("Day", 0);
        PlayerPrefs.SetInt("ActionNum", 1000);
        PlayerPrefs.SetInt("Gold", 0);
        PlayerPrefs.SetInt("IsHeaven", 1);
        PlayerPrefs.SetInt("IsRandomFree", 0);
        PlayerPrefs.SetInt("Interaction", 0);

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
        SceneManager.LoadScene("Loading");
    } // Data Initialize

    private void ClearInventoryData()
    {
        if (File.Exists(InventoryjsonFilePath))
        {
            string jsonData = File.ReadAllText(InventoryjsonFilePath);
            InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(jsonData);
            inventoryData.itemList.Clear();

            string updatedJsonData = JsonUtility.ToJson(inventoryData);
            File.WriteAllText(InventoryjsonFilePath, updatedJsonData);

            Debug.Log("Inventory JSON data cleared.");
        }
        else
        {
            Debug.Log("Inventory JSON file not found.");
        }
    }

    private void ClearCollectFoodData()
    {
        if (File.Exists(CollectedfoodjsonFilePath))
        {
            string jsonData = File.ReadAllText(CollectedfoodjsonFilePath);
            Food CollectionFoodData = JsonUtility.FromJson<Food>(jsonData);
            CollectionFoodData.foods.Clear();

            string updatedJsonData = JsonUtility.ToJson(CollectionFoodData);
            File.WriteAllText(CollectedfoodjsonFilePath, updatedJsonData);

            Debug.Log("CollectionFood JSON data cleared.");
        }
        else
        {
            Debug.Log("CollectionFood JSON file not found.");
        }
    }

    private void ClearCollectDogData()
    {
        if (File.Exists(InventoryjsonFilePath))
        {
            string jsonData = File.ReadAllText(InventoryjsonFilePath);
            CollectedDogData CollectionDogData = JsonUtility.FromJson<CollectedDogData>(jsonData);
            CollectionDogData.collectedDogData.Clear();

            string updatedJsonData = JsonUtility.ToJson(CollectionDogData);
            File.WriteAllText(InventoryjsonFilePath, updatedJsonData);

            Debug.Log("CollectionDog JSON data cleared.");
        }
        else
        {
            Debug.Log("CollectionDog JSON file not found.");
        }
    }
}

