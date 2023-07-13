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

    public GameObject Like; // 호감도 이미지
    public GameObject LikeNum; // 호감도 숫자 
    public GameObject[] Interaction;
    public GameObject InteractionQuestion;
    public GameObject ChatSet;
    public GameObject[] PlusButton;
    public GameObject TutorialInformation;

    private int order = 0;
    private bool IsClick = false;
    private string[] Chat = { "다음은 호감도에 대해 알려드리겠습니다.","호감도는 여기서 확인 가능하며, 오직 상호 작용을 통해서만 얻을 수 있습니다."
            ,"한 번 직접 해보시겠습니까?","상호 작용은 하루에 한 번만 가능하며, 3가지 상호 작용 중 단 하나만 사용할 수 있습니다.",
    "이렇게 획득한 호감도는 유령의 성향을 이끌어내는 데에 사용할 수 있습니다."};
   
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
