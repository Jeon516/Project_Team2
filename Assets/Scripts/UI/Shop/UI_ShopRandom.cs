using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShopRandom : MonoBehaviour
{
    public GameObject FreeBuyQuestion;
    public GameObject NoGold;
    public Button AdvertiseButton;
    public Button FreeButton;
    public Button NoFreeButton;
    public Text FreeChance;
    private int IsRandomFree;

    private void Awake()
    {
        AdvertiseButton.interactable = false;
        FreeButton.interactable = true;
        NoFreeButton.interactable = false;

        IsRandomFree = PlayerPrefs.GetInt("IsRandomFree",0);
        PlayerPrefs.SetInt("IsRandomFree", IsRandomFree);
    }
    private void Update()
    {
        IsRandomFree = PlayerPrefs.GetInt("IsRandomFree", 0);
        PlayerPrefs.SetInt("IsRandomFree", IsRandomFree);

        if (IsRandomFree>=1)
        {
            FreeButton.interactable = false;
        }
        FreeChance.text = (1 - IsRandomFree).ToString() + " / 1";
    }
    public void OnClick_Free()
    {
        if (PlayerPrefs.GetInt("Gold") >= 2000)
            FreeBuyQuestion.SetActive(true);
        else
            NoGold.SetActive(true);
    }

    public void OnClick_Buy(bool Yes)
    {
        FreeBuyQuestion.SetActive(false);
    }

    public void OnClick_FreeClose()
    {
        FreeBuyQuestion.SetActive(false);
    }
}
