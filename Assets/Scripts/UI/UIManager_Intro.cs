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
    private int IsHeaven;

    private void Awake()
    {
        Day= PlayerPrefs.GetInt("Day", 0);
        PlayerPrefs.SetInt("Day", Day);
        IsHeaven = PlayerPrefs.GetInt("IsHeaven", 1);
        PlayerPrefs.SetInt("IsHeaven", IsHeaven);
    }
    private void Start()
    {
        AudioManager.Instance.PlayBGM("IntroMusic");
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

        PlayerPrefs.SetInt("Day", 2);
        PlayerPrefs.SetInt("ActionNum", 1000);
        PlayerPrefs.SetInt("Gold", 100000);
        PlayerPrefs.SetInt("IsHeaven", 1);
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
}
