using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InvenotrySlot : MonoBehaviour
{
    public GameObject DebugSelect;
    public Image SelectedImage;

    public static UI_InvenotrySlot Instance { get; private set; } = null;

    private void Awake()
    {
        Instance = this;
    }
    
    public void Selected()
    {
        Debug.Log("Slot");
        DebugSelect.SetActive(true);
        UI_InventoryInformation.Instance.SwitchImage(SelectedImage);
    }
}
