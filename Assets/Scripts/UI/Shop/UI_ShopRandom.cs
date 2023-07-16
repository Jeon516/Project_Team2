using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShopRandom : MonoBehaviour
{
    public GameObject FreeBuyQuestion;

    public void OnClick_Free()
    {
        FreeBuyQuestion.SetActive(true);
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
