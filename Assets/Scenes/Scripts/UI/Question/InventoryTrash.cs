using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTrash : MonoBehaviour
{
    public void OnClick_YesButton()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        for (int i = 0; i < 20; i++)
        {
            if (UI_InvenotrySlot.Instance.SelectedBoundary[i].activeSelf == true)
            {
                UI_InvenotrySlot.Instance.Deleted();
                
                gameObject.SetActive(false);
            }
        }
    }

    public void OnClick_NoButton()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        gameObject.SetActive(false);
    }
}
