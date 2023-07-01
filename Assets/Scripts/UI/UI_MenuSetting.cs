using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MenuSetting : MonoBehaviour
{
    public GameObject Setting;
    public GameObject Inventory;

    public void OnClick_Setting()
    {
        Setting.SetActive(true);
    }
    public void OnClick_Inventory()
    {
        Inventory.SetActive(true);
    }
}
