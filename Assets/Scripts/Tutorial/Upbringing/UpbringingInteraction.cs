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

    public GameObject Like; // 호감도 이미지
    public GameObject LikeNum; // 호감도 숫자 
    public GameObject[] Interaction;
    public GameObject InteractionQuestion;
    public GameObject ChatSet;
    public GameObject[] PlusButton;
    public GameObject TutorialInformation;

    private int order = 0;
    private string[] Chat = { "다음은 호감도에 대해 알려드리겠습니다.","호감도는 여기서 확인 가능하며,\n 오직 상호 작용을 통해서만 얻을 수 있습니다."
            ,"이번에도 비용을 대신 내드릴 테니 한 번 직접 해보시겠습니까?","상호 작용은 하루에 각각 한 번만 사용할 수 있습니다.",
    "이렇게 획득한 호감도는 유령의 성향을 이끌어내는 데에 사용할 수 있습니다."};
   
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
            Debug.Log(order + "입니다");
            ChatSet.SetActive(true);
            LikeNum.SetActive(true);
            Like.SetActive(true);
            ChatDisplay();
        }
        else if(Input.GetMouseButtonDown(0) && order == 2)
        {
            int Gold = PlayerPrefs.GetInt("Gold") + 2000;
            PlayerPrefs.SetInt("Gold", Gold);
            Debug.Log(order + "입니다");
            ChatSet.SetActive(true);
            LikeNum.SetActive(false);
            Like.SetActive(false);
            Interaction[0].SetActive(true);
            ChatDisplay();
            ChatTransform.anchoredPosition = new Vector2(0, 285);
        }
        else if (Input.GetMouseButtonDown(0) && order == 4)
        {
            Debug.Log(order + "입니다");
            ChatSet.SetActive(true);
            PlusButton[0].SetActive(true);
            PlusButton[1].SetActive(true);
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order == 5)
        {
            Debug.Log(order + "입니다");
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
