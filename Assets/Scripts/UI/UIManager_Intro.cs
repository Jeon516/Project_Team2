using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager_Intro : MonoBehaviour
{
    public GameObject IntroSetting; // 환경 설정
    public GameObject NewQuestion;
    public GameObject NoData;
    private int Day;

    private void Awake()
    {
        Day= PlayerPrefs.GetInt("Day", 0);
        PlayerPrefs.SetInt("Day", Day);
    }
    private void Start()
    {
        AudioManager.Instance.playBGM("IntroMusic");
    }
    public void OnClick_Start()
    {
        if(Day/20 == 0 && Day%20==0)
        {
            NewQuestion.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Loading");
        }
    } // 'Game Start'을 누를 때

    public void OnClick_NewStart(bool Yes)
    {
        if(Yes)
        {
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
        if (Day / 20 == 0 && Day % 20 == 0)
        {
            SceneManager.LoadScene("Loading");
        }
        else
        {
            NoData.SetActive(true);
        }
    } // 'Load Start'을 누를 때

    public void OnClick_Setting()
    {
        IntroSetting.SetActive(true); // ȯ�漳�� Ű��
    } // 'Setting'을 누를 때
    public void OnClick_Exit()
    {
        Application.Quit(); // 어플 종료
    } // 'Exit'을 누를 때
}
