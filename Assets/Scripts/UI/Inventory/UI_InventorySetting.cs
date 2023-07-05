using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InventorySetting : MonoBehaviour
{
    public GameObject ImageDesribe;
    public GameObject TextDescribe;
    public GameObject[] IsImage; // 음식 없으면 SetActive를 false 있으면 true로 설정

    public static UI_InventorySetting Instance { get; private set; } = null;

    private void Awake()
    {
        Instance = this;
    }

    public void OnClick_InventoryCancel()
    {
        gameObject.SetActive(false);
    } // 'X' 버튼 

    public void OnClick_Trash()
    {
        UI_InvenotrySlot.Instance.Deleted();
    } // '버리기' 버튼 

    public void OnClick_Use()
    {
        UI_InvenotrySlot.Instance.Deleted();
    } // '사용' 버튼
}
