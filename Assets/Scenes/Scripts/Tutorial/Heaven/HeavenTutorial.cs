using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HeavenTutorial : MonoBehaviour
{
    public RectTransform ChatTransform;
    public RectTransform NameTransform;
    public Text ChatText;
    public Button StartButton;

    public GameObject RealStart;
    public GameObject TutorialStart;
    public GameObject TutorialChat;
    public GameObject BlockScreen;
    public GameObject[] TutorialInformation;

    private int order;
    private int Day;
    private bool IsClick = true;

    public Image GetColorImage;
    public Image GetEnblem;
    public Text GetText;

    public Image SetColorImage;
    public Image SetEnblem;
    public Text SetText;

    private string[] Chat = {  "앞에 있는 버튼을 누르면 업무가 시작됩니다. \n한 번 눌러보시겠어요?", 
        "저희가 해야 할 일은 강아지들이 들고 오는 티켓이 \n열차와 일치하는지 확인하고 안내하는 일입니다.",
    "왼쪽 열차의 티켓이면 \n왼쪽을 터치하고,", "오른쪽 열차의 티켓이면 \n오른쪽을 터치하면 됩니다.", 
        "가끔 다른 티켓을 가지고 오는 강아지가 있는데, \n그건 교환해줘야 하니 아래의 버튼을 눌러 돌려보내세요.",
    "색깔, 이름, 문양 중 하나라도 다르면 \n그 천국으로 갈 수 없으니 꼼꼼히 봐주세요!", "아 참, 실수하면 업무 시간이 줄어듭니다! \n자, 그럼 한 번 직접 해볼까요?"};

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
            SetColorImage.sprite = GetColorImage.sprite;
            SetEnblem.sprite = GetEnblem.sprite;
            SetText.text = GetText.text;
            StartButton.interactable=false;
        }
        else
            gameObject.SetActive(false);
        ChatText.text = Chat[0];
        order = 1;
    }

    void Update()
    {
        if (order == Chat.Length && Input.GetMouseButtonDown(0) && IsClick)
        {
            RealStart.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (Input.GetMouseButtonDown(0) && order == 1 && IsClick)
        {
            StartButton.interactable = true;
            TutorialChat.SetActive(false);
            ModifyTextRectTransform(-585, 120, 500, 200);
        }
        else if (Input.GetMouseButtonDown(0) && order == 2 && IsClick)
        {
            TutorialInformation[4].SetActive(false);
            for (int i=0;i<4; i++)
                TutorialInformation[i].SetActive(true);
            ModifyRectTransform(485,-296,900,700);
            ModifyTextRectTransform(-290, 105, 500, 200);
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order == 3 && IsClick)
        {
            for (int i = 2; i < 4; i++)
                TutorialInformation[i].SetActive(false);
            for (int i = 4; i < 6; i++)
                TutorialInformation[i].SetActive(true);
            ModifyRectTransform(-485, -296, 900, 700);
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order == 4 && IsClick)
        {
            for (int i = 4; i < 6; i++)
                TutorialInformation[i].SetActive(false);
            TutorialInformation[6].SetActive(true);
            ModifyRectTransform(0, 53, 1800, 800);
            ModifyTextRectTransform(-590, 120, 500, 200);
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order == 5 && IsClick)
        {
            for (int i = 1; i < 3; i++)
                TutorialInformation[i].SetActive(true);
            TutorialInformation[4].SetActive(true);
            TutorialInformation[6].SetActive(false);
            ModifyRectTransform(0, -250, 1800, 800);
            ChatDisplay();
        } 
        else if (Input.GetMouseButtonDown(0) && order == 6 && IsClick)
        {
            for (int i = 7; i < 9; i++)
                TutorialInformation[i].SetActive(true);
            TutorialInformation[1].SetActive(false);
            TutorialInformation[2].SetActive(false);
            TutorialInformation[4].SetActive(false);
            ChatDisplay();
        } 
    }

    public void OnClick_TutorialStart()
    {
        IsClick = false;
        TutorialStart.SetActive(false);
        BlockScreen.SetActive(true);
        StartCoroutine(Wait());
    } // 튜토리얼의 시작 버튼 누를 때

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 3; i++)
            TutorialInformation[i].SetActive(true);
        TutorialInformation[4].SetActive(true);
        IsClick = true;
        TutorialChat.SetActive(true);
        ChatDisplay();
    } // 기다리는 동안 게임 화면 보여주기

    void ChatDisplay()
    {
        ChatText.text = Chat[order];
        order++;
    }

    private void ModifyRectTransform(int x, int y, int width, int height)
    {
        // Width, Height, Pos X, Pos Y 변경
        ChatTransform.sizeDelta = new Vector2(width, height);
        ChatTransform.anchoredPosition = new Vector2(x, y);
    }

    private void ModifyTextRectTransform(int x, int y, int width, int height)
    {
        // Width, Height, Pos X, Pos Y 변경
        NameTransform.sizeDelta = new Vector2(width, height);
        NameTransform.anchoredPosition = new Vector2(x, y);
    }
}
