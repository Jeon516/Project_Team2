using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUse : MonoBehaviour
{
    public GameObject Block; // 사용 불가능을 위한 블럭

    public void OnClick_YesButton()
    {
        AudioManager.Instance.PlaySFX("Eat");
        for (int i=0;i<20;i++)
        {
            if(UI_InvenotrySlot.Instance.SelectedBoundary[i].activeSelf==true)
            {
                UI_InvenotrySlot.Instance.Deleted();
                Block.SetActive(true);
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
