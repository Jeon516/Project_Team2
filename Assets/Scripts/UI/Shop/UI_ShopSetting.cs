using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShopSetting : MonoBehaviour
{
    public GameObject Random;
    public GameObject Shop;
    public GameObject ShopGachaScreen;
    public int Gold;
    private bool IsRandom = false;
    private bool IsShop = false;

    public static UI_ShopSetting Instance { get; private set; } = null;

    private void Start()
    {
        Gold = PlayerPrefs.GetInt("Gold");
        PlayerPrefs.SetInt("Gold", Gold);
        Random.SetActive(true);
    }
    private void Update()
    {
        Gold = PlayerPrefs.GetInt("Gold");
        PlayerPrefs.SetInt("Gold", Gold);
    }
    public void OnClick_Random()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        Random.SetActive(true);
        Shop.SetActive(false);
    }

    public void OnClick_Shop()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        Shop.SetActive(true);
         Random.SetActive(false);
    }

    public void OnClick_Close()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        gameObject.SetActive(false);
    }

    public void OnClick_ShopGacha()
    {
        AudioManager.Instance.PlaySFX("GachaClose");
        Gold -= 2000;
        PlayerPrefs.SetInt("Gold", Gold);
        PlayerPrefs.SetInt("IsRandomFree", 0);
        ShopGachaScreen.SetActive(true);
        ShopGacha.Instance.GachaStart();
    }
}
