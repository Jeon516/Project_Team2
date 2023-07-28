using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Setting : MonoBehaviour
{
    public GameObject MusicSetting;
    public GameObject EndQuestion;

    public void OnClick_Resume()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        gameObject.SetActive(false);
        Time.timeScale = 1;
    } // ��ư X�� ���� �� ������ ����
    public void OnClick_MusicSetting()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        MusicSetting.SetActive(true);
    } // 'ȯ�漳��' Ű��
    public void OnClick_Exit()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        EndQuestion.SetActive(true);
    } // �� ����
    public void OnClick_EndYes()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        Application.Quit();
    }
    public void OnClick_EndNo()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        EndQuestion.SetActive(false);
    }
}
