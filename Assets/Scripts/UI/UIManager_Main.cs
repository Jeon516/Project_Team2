using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager_Main : MonoBehaviour
{
    public GameObject ui_Setting;
    public void OnClick_Start()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Stage", 1);
        SceneManager.LoadScene("World");
    }
    public void OnClick_Load()
    {
        SceneManager.LoadScene("World");
    }
    public void OnClick_Setting()
    {
        ui_Setting.SetActive(true);
    }
    public void OnClick_Exit()
    {
        Application.Quit();
    }
}
