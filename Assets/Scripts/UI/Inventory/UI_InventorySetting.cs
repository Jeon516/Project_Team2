using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InventorySetting : MonoBehaviour
{
    public GameObject ImageDesribe;
    public GameObject TextDescribe;

    public static UI_InventorySetting Instance { get; private set; } = null;

    private int[] IsCheck = new int[18]; // ´­·¶´ÂÁö ¾È ´­·¶´ÂÁö
    private void Awake()
    {
        Instance = this;
        for (int i=0;i<18;i++)
        {
            IsCheck[i] = 0;
        }
    }

    public void OnClick_InventoryCancel()
    {
        gameObject.SetActive(false);
    }
}
