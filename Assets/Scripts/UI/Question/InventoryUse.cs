using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUse : MonoBehaviour
{
    public GameObject Block; // ��� �Ұ����� ���� ��

    public void OnClick_YesButton()
    {
        for(int i=0;i<20;i++)
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
        gameObject.SetActive(false);
    }
}
