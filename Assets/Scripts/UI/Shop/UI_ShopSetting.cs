using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShopSetting : MonoBehaviour
{
    public GameObject Random;
    public GameObject Shop;
    public GameObject ShopGachaScreen;
    private bool IsRandom = false;
    private bool IsShop = false;

    private void Start()
    {
        Random.SetActive(true);
    }
    public void OnClick_Random()
    {
        Random.SetActive(true);
        Shop.SetActive(false);
    }

    public void OnClick_Shop()
    {
         Shop.SetActive(true);
         Random.SetActive(false);
    }

    public void OnClick_Close()
    {
        gameObject.SetActive(false);
    }

    public void OnClick_ShopGacha()
    {
        ShopGachaScreen.SetActive(true);
        ShopGacha.Instance.GachaStart();
    }
}
