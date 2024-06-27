using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Check : MonoBehaviour
{
    public void OnClick_Check()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        gameObject.SetActive(false);
    }

}
