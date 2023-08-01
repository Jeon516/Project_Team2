using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpbringingInformation : MonoBehaviour
{
    public Text ChatText;
    public RectTransform ChatTransform;
    public GameObject TutorialInformationButton;
    public GameObject TutorialInformation;
    public GameObject NextDay;
    public GameObject Tutorial;

    private int order = 0;
    private bool IsClick = false;
    private string[] Chat = { "마지막으로 이것만 알려드리고 \n설명을 마치도록 하겠습니다.","여기 도감이 보이시죠? \n한 번 열어보시겠어요?"
            ,"여기서 지금까지 확인한 음식과 \n강아지의 정보를 확인할 수 있습니다.","어떻게 기록되는지에 대해선 저희에게 알려진 바는 없지만...뭐, 이 세계 자체가 특별하니까요. 의외로 흔한 일 일수도 있죠.",
    "제 설명은 여기까지입니다."," 아 참, 볼 일을 모두 마쳤다면 주무시고 내일 출근하시면 됩니다. 그럼 다음에 뵙겠습니다!"};

    // Start is called before the first frame update
    void Start()
    {
        ChatDisplay();
        ChatTransform.anchoredPosition = new Vector2(0, -296);
        IsClick = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && order == 1 && IsClick)
        {
            TutorialInformationButton.SetActive(true);
            ChatDisplay();
            IsClick = false;
        }
        else if (Input.GetMouseButtonDown(0) && order == 4 && IsClick)
        {
            TutorialInformation.SetActive(false);
            ChatTransform.anchoredPosition = new Vector2(0, -296);
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order == 5 && IsClick)
        {
            NextDay.SetActive(true);
            ChatTransform.anchoredPosition = new Vector2(-220, -296);
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order < Chat.Length && IsClick)
        {
            ChatDisplay();
        }
    }

    void ChatDisplay()
    {
        ChatText.text = Chat[order];
        order++;
    }

    public void Onclick_Information()
    {
        TutorialInformationButton.SetActive(false);
        IsClick = true;
        TutorialInformation.SetActive(true);
        ChatTransform.anchoredPosition = new Vector2(0, -225);
        ChatDisplay(); 
    }

    public void Onclick_Nextday()
    {
        Tutorial.SetActive(false);
    }
}
