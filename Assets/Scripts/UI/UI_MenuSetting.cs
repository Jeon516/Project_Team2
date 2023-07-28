using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MenuSetting : MonoBehaviour
{
    public GameObject Setting;
    public GameObject Inventory;
    public GameObject Shop;
    public GameObject Information;

    public void OnClick_Setting()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        Setting.SetActive(true);
    }
    public void OnClick_Shop()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        Shop.SetActive(true);
    }
    public void OnClick_Informaition()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        Information.SetActive(true);
    }
    public void OnClick_Inventory()
    {
        AudioManager.Instance.PlaySFX("dog-panting");
        Inventory.SetActive(true);
    }
}
