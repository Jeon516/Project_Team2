using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager_Intro : MonoBehaviour
{
    public GameObject IntroSetting; // ȯ�漳��
    public GameObject Name;

    private void Start()
    {
        AudioManager.Instance.playBGM("IntroMusic");
    }
    public void OnClick_Start()
    {
        SceneManager.LoadScene("Loading");
    } // 'Game Start'�� ������ ��
    public void OnClick_Load()
    {
        SceneManager.LoadScene("Heaven");
    } // 'Load Start'�� ������ ��
    public void OnClick_Setting()
    {
        IntroSetting.SetActive(true); // ȯ�漳�� Ű��
    } // 'Setting'�� ������ ��
    public void OnClick_Name()
    {
        Name.SetActive(true);
    }
    public void OnClick_Exit()
    {
        Application.Quit(); // ���� ����
    } // 'Exit'�� ������ ��
}
