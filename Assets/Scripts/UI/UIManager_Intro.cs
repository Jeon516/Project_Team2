using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager_Intro : MonoBehaviour
{
    public GameObject IntroSetting; // 환경설정

    private void Start()
    {
        AudioManager.Instance.playBGM("IntroMusic");
    }
    public void OnClick_Start()
    {
        SceneManager.LoadScene("Heaven");
    } // 'Game Start'를 눌렀을 때
    public void OnClick_Load()
    {
        SceneManager.LoadScene("Heaven");
    } // 'Load Start'를 눌렀을 때
    public void OnClick_Setting()
    {
        IntroSetting.SetActive(true); // 환경설정 키기
    } // 'Setting'을 눌렀을 때
    public void OnClick_Exit()
    {
        Application.Quit(); // 게임 종료
    } // 'Exit'을 눌렀을 때
}
