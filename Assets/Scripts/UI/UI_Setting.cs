using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Setting : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SFXSlider;
    public Text BGMText;
    public Text SFXText;

    private void OnEnable()
    {
        BGMSlider.value = PlayerPrefs.GetFloat("BGM", 50.0f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFX", 50.0f);
        BGMText.text = $"{(int)BGMSlider.value}";
        SFXText.text = $"{(int)SFXSlider.value}";
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("BGM", BGMSlider.value);
        PlayerPrefs.SetFloat("SFX", SFXSlider.value);
        PlayerPrefs.Save();
    } // BGM, SFX �� ����

    public void OnValueChanged_BGM()
    {
        BGMText.text = ((int)BGMSlider.value).ToString();
    } // �����̴� ���� ���� BGM �ؽ�Ʈ ǥ��

    public void OnValueChanged_SFX()
    {
        SFXText.text = ((int)SFXSlider.value).ToString();
    } // �����̴� ���� ���� SFX �ؽ�Ʈ ǥ��

    public void OnClick_Exit()
    {
        gameObject.SetActive(false);
    } // ��ư X�� ���� �� ������ ����
}
