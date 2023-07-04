using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShopSetting : MonoBehaviour
{
    public GameObject Random;
    public GameObject Shop;
    private bool IsRandom = false;
    private bool IsShop = false;

    public void OnClick_Random()
    {
        if (!IsRandom)
        { 
            Random.SetActive(true);
            Shop.SetActive(false);
            IsRandom = true;
            IsShop = false;
        }
        else
        {
            Random.SetActive(false);
            IsRandom = false;
        }
    }

    public void OnClick_Shop()
    {
        if (!IsShop)
        {
            Shop.SetActive(true);
            Random.SetActive(false);
            IsShop = true;
            IsRandom = false;
        }
        else
        {
            Shop.SetActive(false);
            IsShop = false;
        }
    }

    public void OnClick_Close()
    {
        gameObject.SetActive(false);
    }
}
