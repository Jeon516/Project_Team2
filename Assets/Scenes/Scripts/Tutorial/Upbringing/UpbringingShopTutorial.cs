using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpbringingShopTutorial : MonoBehaviour
{
    private int order=0;
    private int Day;
    private int TutorialDay;
    private bool IsClick = false;

    public GameObject[] Block = new GameObject[6];
    public Text ChatText;
    public GameObject ChatSet;
    public GameObject TutorialShop;
    public Button TutorialShopButton;
    public RectTransform ChatTransform;
    public GameObject CancelButton;
    public GameObject FreeButton;
    public GameObject QuestioinTutorial;

    private string[] Chat = {  "������ ���� �ֹ��� �Ϸ翡 �� ������ �����ϴ� �����Ͻñ� �ٶ��ϴ�.","�� �̹��� ����� ��� ���帱 �״� �� �� �ֹ��غ�����."};

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && 1 == order && IsClick)
        {
            ChatText.text = Chat[order];
            FreeButton.SetActive(true);
            Block[0].SetActive(false);
            order++;
            ChatTransform.anchoredPosition = new Vector2(0, -360);
        }
    }

    public void Onclick_Shop()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        TutorialShopButton.interactable = false;
        TutorialShop.SetActive(true);
        IsClick = true;
        ChatText.text = Chat[order];
        for(int i=0;i<6;i++)
        {
            Block[i].SetActive(true);
        }
        order++;
    }

    public void Onclick_TutorialGacha()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        ChatText.text = "";
        ChatSet.SetActive(false);
        FreeButton.SetActive(false);
    }
    public void Onclick_YesButton()
    {
        CancelButton.SetActive(false);
        StartCoroutine(Wait());
    }
    public void Onclick_NoButton()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        FreeButton.SetActive(true);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
    }
}
