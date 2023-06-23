using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Setting : MonoBehaviour
{
    public Slider slider_BGM;
    public Slider slider_SFX;
    public Text text_BGM;
    public Text text_SFX;

    private void OnEnable()
    {
        slider_BGM.value = PlayerPrefs.GetFloat("BGM", 0.5f);
        slider_SFX.value = PlayerPrefs.GetFloat("SFX", 0.5f);
        text_BGM.text = $"{slider_BGM.value}";
        text_SFX.text = $"{slider_SFX.value}";
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat("BGM", slider_BGM.value);
        PlayerPrefs.SetFloat("SFX", slider_SFX.value);
    }
    public void OnValueChanged_BGM()
    {
        text_BGM.text = $"{slider_BGM.value}";
    }
    public void OnValueChanged_SFX()
    {
        text_SFX.text = $"{slider_SFX.value}";
    }
    public void OnClick_Exit()
    {
        gameObject.SetActive(false);
    }
}
