using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Setting : MonoBehaviour
{
    public GameObject MusicSetting;

    public void OnClick_Resume()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    } // ��ư X�� ���� �� ������ ����
    public void OnClick_MusicSetting()
    {
        MusicSetting.SetActive(true);
    } // 'ȯ�漳��' Ű��
    public void OnClick_Exit()
    {
        Application.Quit();
    } // �� ����
}
