using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_World : MonoBehaviour
{
    public static UIManager_World Instance { get; private set; } = null;

    public Image image_HP;
    public Text text_Score;
    public Text text_Stage;
    public GameObject ui_Pause;
    public GameObject ui_Setting;
    public GameObject ui_GameOver;

    private void Awake()
    {
        Instance = this;
    }
    public void SETUI_HP(int current, int max)
    {
        image_HP.fillAmount = (float)current / max;
    }
    public void SETUI_Score(int value)
    {
        text_Score.text = string.Format("{0,10:D8}", value);
    }
    public void SETUI_Stage(int value)
    {
        text_Stage.text = string.Format("{0,2:D2}", value);
    }
    public void SETUI_Pause(bool state)
    {
        ui_Pause.SetActive(state);
    }
    public void SETUI_Setting(bool state)
    {
        ui_Setting.SetActive(state);
    }
    public void SETUI_GameOver(bool state)
    {
        ui_GameOver.SetActive(state);
    }
}
