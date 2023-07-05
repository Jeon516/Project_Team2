using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InventorySetting : MonoBehaviour
{
    public GameObject ImageDesribe;
    public GameObject TextDescribe;
    public GameObject[] IsImage; // ���� ������ SetActive�� false ������ true�� ����

    public static UI_InventorySetting Instance { get; private set; } = null;

    private void Awake()
    {
        Instance = this;
    }

    public void OnClick_InventoryCancel()
    {
        gameObject.SetActive(false);
    } // 'X' ��ư 

    public void OnClick_Trash()
    {
        UI_InvenotrySlot.Instance.Deleted();
    } // '������' ��ư 

    public void OnClick_Use()
    {
        UI_InvenotrySlot.Instance.Deleted();
    } // '���' ��ư
}
