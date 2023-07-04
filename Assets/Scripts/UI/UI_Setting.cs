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
    } // ¹öÆ° X¸¦ ´©¸¦ ¶§ ÇÁ¸®ÆÕ ²ô±â
    public void OnClick_MusicSetting()
    {
        MusicSetting.SetActive(true);
    } // 'È¯°æ¼³Á¤' Å°±â
    public void OnClick_Exit()
    {
        Application.Quit();
    } // ¾Û ²ô±â
}
