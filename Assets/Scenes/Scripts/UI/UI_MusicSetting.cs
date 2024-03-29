using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MusicSetting : MonoBehaviour
{
    private float BGMVolume;
    private float SFXVolume;
    private int BGMOrder;
    private int SFXOrder;
    public GameObject[] BGMSize;
    public GameObject[] SFXSize;

    private void Start()
    {
        BGMVolume = PlayerPrefs.GetFloat("BGM",0.6f) * 100;
        SFXVolume = PlayerPrefs.GetFloat("SFX",0.6f) * 100;
        BGMOrder = PlayerPrefs.GetInt("BGMOrder", 2);
        SFXOrder = PlayerPrefs.GetInt("SFXOrder", 2);

        for (int i = 0; i <= BGMOrder; i++)
        {
            BGMSize[i].SetActive(false);
        }
        for (int i = BGMOrder + 1; i < 5; i++)
        {
            BGMSize[i].SetActive(true);
        }

        for (int i = 0; i <= SFXOrder; i++)
        {
            SFXSize[i].SetActive(false);
        }
        for (int i = SFXOrder + 1; i < 5; i++)
        {
            SFXSize[i].SetActive(true);
        }
    }
    public void OnClick_BGMVolumeBig()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (BGMVolume < 100)
        {
            BGMVolume += 20;
            BGMOrder++;
            for(int i=0;i<=BGMOrder;i++)
            {
                BGMSize[i].SetActive(false);
            }
            for (int i = BGMOrder+1; i < 5; i++)
            {
                BGMSize[i].SetActive(true);
            }
            PlayerPrefs.SetFloat("BGM", BGMVolume / 100.0f);
            PlayerPrefs.SetInt("BGMOrder", BGMOrder);
            AudioManager.Instance.bgmSource.volume = PlayerPrefs.GetFloat("BGM", 0.6f);
        }
    }
    public void OnClick_BGMVolumeSmall()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (BGMVolume > 0)
        {
            BGMVolume -= 20;
            BGMOrder--;
            for (int i = 0; i <= BGMOrder; i++)
            {
                BGMSize[i].SetActive(false);
            }
            for (int i = BGMOrder+1; i < 5; i++)
            {
                BGMSize[i].SetActive(true);
            }
            PlayerPrefs.SetFloat("BGM", BGMVolume / 100.0f);
            PlayerPrefs.SetInt("BGMOrder", BGMOrder);
            AudioManager.Instance.bgmSource.volume = PlayerPrefs.GetFloat("BGM", 0.6f);
        }
    }
    public void OnClick_SFXVolumeBig()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (SFXVolume < 100)
        {
            SFXVolume += 20;
            SFXOrder++;
            for (int i = 0; i <= SFXOrder; i++)
            {
                SFXSize[i].SetActive(false);
            }
            for (int i = SFXOrder + 1; i < 5; i++)
            {
                SFXSize[i].SetActive(true);
            }
            PlayerPrefs.SetFloat("SFX", SFXVolume / 100.0f);
            PlayerPrefs.SetInt("SFXOrder", SFXOrder);

            for (int i = 0; i < AudioManager.Instance.sfxSource.Length; ++i)
            {
                AudioManager.Instance.sfxSource[i].volume = PlayerPrefs.GetFloat("SFX", 0.6f);
            }
        }
    }
    public void OnClick_SFXVolumeSmall()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (SFXVolume > 0)
        {
            SFXVolume -= 20;
            SFXOrder--;
            for (int i = 0; i <= SFXOrder; i++)
            {
                SFXSize[i].SetActive(false);
            }
            for (int i = SFXOrder + 1; i < 5; i++)
            {
                SFXSize[i].SetActive(true);
            }
            PlayerPrefs.SetFloat("SFX", SFXVolume / 100.0f);
            PlayerPrefs.SetInt("SFXOrder", SFXOrder);

            for (int i = 0; i < AudioManager.Instance.sfxSource.Length; ++i)
            {
                AudioManager.Instance.sfxSource[i].volume = PlayerPrefs.GetFloat("SFX", 0.6f);
            }
        }
    }
    public void OnClick_Close()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        gameObject.SetActive(false);
    }
}
