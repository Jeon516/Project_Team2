using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpbringingInteraction : MonoBehaviour
{
    public Text ChatText;
    public Text ActionNum;
    public Text LikeNumText;
    public RectTransform ChatTransform;

    public GameObject Like; // ȣ���� �̹���
    public GameObject LikeNum; // ȣ���� ���� 
    public GameObject[] Interaction;
    public GameObject InteractionQuestion;
    public GameObject ChatSet;
    public GameObject[] PlusButton;
    public GameObject TutorialInformation;

    private int order = 0;
    private bool IsClick = false;
    private string[] Chat = { "������ ȣ������ ���� �˷��帮�ڽ��ϴ�.","ȣ������ ���⼭ Ȯ�� �����ϸ�, ���� ��ȣ �ۿ��� ���ؼ��� ���� �� �ֽ��ϴ�."
            ,"�� �� ���� �غ��ðڽ��ϱ�?","��ȣ �ۿ��� �Ϸ翡 �� ���� �����ϸ�, 3���� ��ȣ �ۿ� �� �� �ϳ��� ����� �� �ֽ��ϴ�.",
    "�̷��� ȹ���� ȣ������ ������ ������ �̲���� ���� ����� �� �ֽ��ϴ�."};
   
    void Start()
    {
        ChatDisplay();
        IsClick = true;
        ChatTransform.anchoredPosition = new Vector2(0, 120);
    }

    void Update()
    {
        LikeNumText.text = ActionNum.text;
        if (Input.GetMouseButtonDown(0) && order ==1 && IsClick)
        {
            LikeNum.SetActive(true);
            Like.SetActive(true);
            ChatDisplay();
        }
        else if(Input.GetMouseButtonDown(0) && order == 2 && IsClick)
        {
            LikeNum.SetActive(false);
            Like.SetActive(false);
            Interaction[0].SetActive(true);
            ChatDisplay();
            IsClick = false;
            //ChatTransform.anchoredPosition = new Vector2(0, 180);
        }
        else if (Input.GetMouseButtonDown(0) && order == 4 && IsClick)
        {
            PlusButton[0].SetActive(true);
            PlusButton[1].SetActive(true);
            ChatDisplay();
            //ChatTransform.anchoredPosition = new Vector2(0, 180);
        }
        else if (Input.GetMouseButtonDown(0) && order == 5 && IsClick)
        {
            TutorialInformation.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    void ChatDisplay()
    {
        ChatText.text = Chat[order];
        order++;
    }

    public void Onclick_TutorialInteraction()
    {
        ChatSet.SetActive(false);
        Interaction[0].SetActive(false);
        InteractionQuestion.SetActive(true);
    }

    public void Onclick_Yes()
    {
        //ChatTransform.anchoredPosition = new Vector2(0, -296);
        ChatSet.SetActive(true);
        LikeNum.SetActive(true);
        Like.SetActive(true);
        ChatDisplay();
        IsClick = true;
    }
}
