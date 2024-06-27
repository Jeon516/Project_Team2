using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpbringingInteraction : MonoBehaviour
{
    public Text ChatText;
    public Text ActionNum;
    public Text LikeNumText;
    public Text InteractionText;
    public RectTransform ChatTransform;

    public GameObject Like; // ȣ���� �̹���
    public GameObject LikeNum; // ȣ���� ���� 
    public GameObject[] Interaction;
    public GameObject InteractionQuestion;
    public GameObject ChatSet;
    public GameObject[] PlusButton;
    public GameObject TutorialInformation;

    private int order = 0;
    private string[] Chat = { "������ ȣ������ ���� �˷��帮�ڽ��ϴ�.","ȣ������ ���⼭ Ȯ�� �����ϸ�,\n ���� ��ȣ �ۿ��� ���ؼ��� ���� �� �ֽ��ϴ�."
            ,"�̹����� ����� ��� ���帱 �״� �� �� ���� �غ��ðڽ��ϱ�?","��ȣ �ۿ��� �Ϸ翡 ���� �� ���� ����� �� �ֽ��ϴ�.",
    "�̷��� ȹ���� ȣ������ ������ ������ �̲���� ���� ����� �� �ֽ��ϴ�."};
   
    void Start()
    {
        InteractionText.text = InteractionManager.Instance.MiddleInteractionText.text;
        ChatSet.SetActive(true);
        ChatDisplay();
        ChatTransform.anchoredPosition = new Vector2(0, -220);
    }

    void Update()
    {
        LikeNumText.text = ActionNum.text;
        if (Input.GetMouseButtonDown(0) && order ==1)
        {
            Debug.Log(order + "�Դϴ�");
            ChatSet.SetActive(true);
            LikeNum.SetActive(true);
            Like.SetActive(true);
            ChatDisplay();
        }
        else if(Input.GetMouseButtonDown(0) && order == 2)
        {
            int Gold = PlayerPrefs.GetInt("Gold") + 2000;
            PlayerPrefs.SetInt("Gold", Gold);
            Debug.Log(order + "�Դϴ�");
            ChatSet.SetActive(true);
            LikeNum.SetActive(false);
            Like.SetActive(false);
            Interaction[0].SetActive(true);
            ChatDisplay();
            ChatTransform.anchoredPosition = new Vector2(0, 285);
        }
        else if (Input.GetMouseButtonDown(0) && order == 4)
        {
            Debug.Log(order + "�Դϴ�");
            ChatSet.SetActive(true);
            PlusButton[0].SetActive(true);
            PlusButton[1].SetActive(true);
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order == 5)
        {
            Debug.Log(order + "�Դϴ�");
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
        InteractionText.text = InteractionManager.Instance.MiddleInteractionText.text;
        InteractionQuestion.SetActive(true);
    }

    public void Onclick_Yes()
    {
        Interaction[0].SetActive(false);
        ChatSet.SetActive(true);
        LikeNum.SetActive(true);
        Like.SetActive(true);
        ChatDisplay();
    }
}
