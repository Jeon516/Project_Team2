using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InvenotrySlot : MonoBehaviour
{
    public GameObject[] SelectedBoundary;
    public GameObject[] Icon;
    public Image[] SelectedImage;
    public Button[] buttons;

    private int[] IsCheck; // �������� �� ��������
    private int SelectedNum; // ���õ� ����
    public static UI_InvenotrySlot Instance { get; private set; } = null;

    private void Awake()
    {
        Instance = this;
        IsCheck = new int[buttons.Length];
    }
    private void OnEnable()
    {
        Debug.Log(buttons.Length);
        Initialized();
    } 

    public void Selected(Button button)
    {
        Debug.Log(button);
        AudioManager.Instance.PlaySFX("ButtonClick");
        for (int i=0;i< buttons.Length; i++)
        {
            if (buttons[i] == button)
            {
                Debug.Log(i + "�� �νĵǾ����ϴ�");
                if (IsCheck[i] == 0 && SelectedImage[i].GetComponent<Image>().sprite != null)
                {
                    IsCheck[i] = 1;
                    SelectedBoundary[i].SetActive(true);
                    UI_InventoryInformation.Instance.SwitchImage(SelectedImage[i]);
                    SelectedNum = i;
                } // ������ �� �̹����� �ִ� ���
                else if (IsCheck[i] == 0 && SelectedImage[i].GetComponent<Image>().sprite == null)
                {
                    UI_InventoryInformation.Instance.SwitchImageNull();
                } // ������ �� �̹����� ���� ���
                else if (IsCheck[i] == 1)
                {
                    IsCheck[i] = 0;
                    SelectedBoundary[i].SetActive(false);
                    UI_InventoryInformation.Instance.SwitchImageNull();
                } // ���� ��ҵ� �� �׵θ��� �̹��� ���� �����
            }
            else if (IsCheck[i] == 1)
            {
                IsCheck[i] = 0;
                SelectedBoundary[i].SetActive(false);
            } // ��ư ������ �ʴ� �͵��� ����
        }
    }
    public void Deleted()
    {
        Debug.Log(SelectedNum);
        SelectedImage[SelectedNum].GetComponent<Image>().sprite = null;
        SelectedBoundary[SelectedNum].SetActive(false);
        Icon[SelectedNum].SetActive(false);
        UI_InventoryInformation.Instance.SwitchImageNull();
    } // �κ��丮 �̹��� �� ū �̹��� ����
    private void Initialized()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            IsCheck[i] = 0;
            SelectedBoundary[i].SetActive(false);
            UI_InventoryInformation.Instance.SwitchImageNull();
        }
    } // �ʱ�ȭ ���·� �����.
}
