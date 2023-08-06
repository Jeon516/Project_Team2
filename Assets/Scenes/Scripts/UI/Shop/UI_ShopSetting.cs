using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShopSetting : MonoBehaviour
{
    public GameObject Random;
    public GameObject Shop;
    public GameObject StarShop;
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
        StarShop.SetActive(false);
        Random.SetActive(true);
        Shop.SetActive(false);
    }

    public void OnClick_Shop()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        StarShop.SetActive(false);
        Shop.SetActive(true);
        Random.SetActive(false);
    }

    public void OnClick_StarShop()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        StarShop.SetActive(true);
        Shop.SetActive(false);
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
        Gold -= 16000;
        PlayerPrefs.SetInt("Gold", Gold);
        PlayerPrefs.SetInt("IsRandomFree", 0); // 0을 1로 바꿔야함
        ShopGachaScreen.SetActive(true);
        ShopGacha.Instance.GachaStart();
    }
}
