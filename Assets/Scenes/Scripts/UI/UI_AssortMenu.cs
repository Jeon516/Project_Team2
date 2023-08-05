using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AssortMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Exit;

    public void OnClick_MeunOpen()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        Menu.SetActive(true);
        Time.timeScale = 0;
    }
}
