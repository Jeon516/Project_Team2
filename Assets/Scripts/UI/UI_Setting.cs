using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Setting : MonoBehaviour
{
    private float BGMVolume = 60f;
    private float SFXVolume = 60f;
    private int BGMOrder = 2;
    private int SFXOrder = 2;
    public GameObject[] BGMSize;
    public GameObject[] SFXSize;

    private void OnEnable()
    {
        PlayerPrefs.GetFloat("BGM", 60.0f);
        PlayerPrefs.GetFloat("SFX", 60.0f);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("BGM", BGMVolume);
        PlayerPrefs.SetFloat("SFX", SFXVolume);
        PlayerPrefs.Save();
    } // BGM, SFX 값 저장
    public void OnClick_BGMVolumeBig()
    {
        if (BGMVolume < 100)
        {
            BGMVolume += 20;
            BGMOrder++;
            BGMSize[BGMOrder].SetActive(false);
            Debug.Log(BGMOrder);
            Debug.Log(BGMSize);
        }
    }
    public void OnClick_BGMVolumeSmall()
    {
        if (BGMVolume > 0)
        {
            BGMVolume -= 20;
            BGMOrder--;
            BGMSize[BGMOrder].SetActive(true);
            Debug.Log(BGMOrder);
            Debug.Log(BGMSize);
        }
    }
    public void OnClick_SFXVolumeBig()
    {
        if (SFXVolume < 100)
        {
            SFXVolume += 20;
            SFXOrder++;
        }
    }
    public void OnClick_SFXVolumeSmall()
    {
        if (SFXVolume > 0)
        {
            SFXVolume -= 20;
            SFXOrder--;
        }
    }
    public void OnClick_Exit()
    {
        gameObject.SetActive(false);
    } // 버튼 X를 누를 때 프리팹 끄기
}
