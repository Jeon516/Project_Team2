using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } = null;

    public Image image_HP;
    public Text text_Score;
    public Text text_Stage;

    public GameObject ui_GameOver;
    public GameObject ui_Setting;
    public GameObject ui_Pause;

    public int currentScore;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        currentScore = 0;
        SetUI_Score(currentScore);
    }
    public void SetUI_Score(int score)
    {
        text_Score.text = $"Score : {score}";
    }
    public void SetUI_HP(int current, int max)
    {
        image_HP.fillAmount = (float)current / max;
    }
    public void SetUI_Stage(int value)
    {
        text_Stage.text = $"{value}";
    }
    public void SetUI_Pause(bool state)
    {
        ui_Pause.SetActive(state);
    }
    public void SetUI_Setting(bool state)
    {
        ui_Setting.SetActive(state);
    }
    public void SetUI_GameOver(bool state)
    {
        ui_GameOver.SetActive(state);
    }
}
