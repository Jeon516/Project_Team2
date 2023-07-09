using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeavenTutorial : MonoBehaviour
{
    public Text ChatText;
    public Button StartButton;
    private int order;
    private int Day;

    private string[] Chat = {  "앞에 있는 버튼을 누르면 업무를 시작하게 됩니다. 한 번 눌러보시겠어요?", 
        "저기 티켓과 열차가 보이시죠? 저희가 해야 할 일은 열차에 탑승할 강아지의 티켓이 열차의 정보와 일치하는지 확인하고 안내하는 일입니다.",
    "왼쪽 열차의 티켓이면 왼쪽을 터치하고,", "오른쪽 열차의 티켓이면 오른쪽을 터치하면 됩니다.", 
        "가끔 색이 다른 티켓을 가지고 오는 강아지가 있는데, 그건 교환해줘야 하니 아래의 버튼을 눌러 돌려보내야 합니다.",
    "아 참, 실수를 하게 되면 업무 시간이 줄어드니 주의하세요!", "어때요? 생각보다 어렵지 않죠? 자, 그럼 한 번 직접 해볼까요?"};

    private void Awake()
    {
        Day = PlayerPrefs.GetInt("Day");
        PlayerPrefs.SetInt("Day", Day);
    }

    private void Start()
    {
        if (Day == 1)
        {
            gameObject.SetActive(true);
            StartButton.interactable=false;
        }
        ChatText.text = Chat[0];
        order = 1;
    }

    void Update()
    {
        if (order == Chat.Length && Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0) && Chat.Length > order)
        {
            ChatText.text = Chat[order];
            order++;
        }
    }

}
