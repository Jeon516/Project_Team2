using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MenuSetting : MonoBehaviour
{
    public GameObject Setting;
    public GameObject CloseButton;

    public void OnClick_Setting()
    {
        Setting.SetActive(true);
    }
}
