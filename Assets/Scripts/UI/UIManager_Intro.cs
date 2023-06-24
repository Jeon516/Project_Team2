using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager_Intro : MonoBehaviour
{
    public GameObject IntroSetting; // ȯ�漳��

    public void OnClick_Start()
    {
        SceneManager.LoadScene("Heaven_Width");
    } // 'Game Start'�� ������ ��
    public void OnClick_Load()
    {
        SceneManager.LoadScene("Heaven_Width");
    } // 'Load Start'�� ������ ��
    public void OnClick_Setting()
    {
        IntroSetting.SetActive(true); // ȯ�漳�� Ű��
    } // 'Setting'�� ������ ��
    public void OnClick_Exit()
    {
        Application.Quit(); // ���� ����
    } // 'Exit'�� ������ ��
}
