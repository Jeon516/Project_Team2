using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeavenTutorialFour : MonoBehaviour
{
    public Text ChatText;

    public GameObject RealStart;
    public GameObject TutorialStart;
    public GameObject TutorialChat;
    public GameObject BlockScreen;
    public GameObject BackgroundScreen;
    public GameObject[] HeavenObject;

    private int order=0;
    private int Day;

    public Image GetColorImage;
    public Image GetEnblem;
    public Text GetText;

    public Image SetColorImage;
    public Image SetEnblem;
    public Text SetText;

    private string[] ChatFour = {  "잠시만요! 일을 시작하기 전에 알려드릴 소식이 있어요.","어제 다른 관리소에서 천국 이름이\n 다른 티켓이 나타났다고 해요.",
    "그래서 오늘부터는 열차와 티켓이 어떤 천국의 열차인지,\n어떤 천국의 티켓인지 더 꼼꼼하게 확인해야 해요.","전달드릴 소식은 이게 끝입니다.\n그럼, 오늘 하루도 파이팅입니다!"};

    private string[] ChatSeven = {  "잠시만요! 일을 시작하기 전에 알려드릴 소식이 있어요.","어제 다른 관리소에서 천국 엠블럼이\n 다른 티켓이 나타났다고 해요.",
    "그래서 오늘부터는 열차와 티켓의 엠블럼이 일치하는지\n 더 꼼꼼하게 확인해야 해요.","전달드릴 소식은 이게 끝입니다.\n그럼, 오늘 하루도 파이팅입니다!"};

    private string[] Chat = {  "잠시만요! 일을 시작하기 전에 알려드릴 소식이 있어요.","어제 다른 관리소에서 천국 이름이 다른 티켓이\n 나타났다고 해요.",
    "그래서 오늘부터는 열차와 티켓이 어떤 천국의 열차인지,\n어떤 천국의 티켓인지 더 꼼꼼하게 확인해야 해요.","전달드릴 소식은 이게 끝입니다.\n그럼, 오늘 하루도 파이팅입니다!"};

    private void Awake()
    {
        Day = PlayerPrefs.GetInt("Day");
        PlayerPrefs.SetInt("Day", Day);

        if(Day==4)
        {
            Chat = ChatFour;
        }
        else if(Day==7)
        {
            Chat = ChatSeven;
        }
    }

    private void Start()
    {
        TutorialChat.SetActive(false);
        if (Day == 4 || Day==7)
        {
            SetColorImage.sprite = GetColorImage.sprite;
            SetEnblem.sprite = GetEnblem.sprite;
            SetText.text = GetText.text;
        }
        else
            gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && order == 1)
        {
            HeavenObject[0].SetActive(true);
            ChatText.text = Chat[order];
            order++;
        }
        else if (Input.GetMouseButtonDown(0) && order == 2)
        {
            HeavenObject[1].SetActive(true);
            HeavenObject[2].SetActive(true);
            ChatText.text = Chat[order];
            order++;
        }
        else if (Input.GetMouseButtonDown(0) && order == 3)
        {
            HeavenObject[0].SetActive(false);
            HeavenObject[1].SetActive(false);
            HeavenObject[2].SetActive(false);
            BlockScreen.SetActive(false);
            ChatText.text = Chat[order];
            order++;
        }
        else if (Input.GetMouseButtonDown(0) && order == 4)
        {
            BackgroundScreen.SetActive(false);
            TutorialChat.SetActive(false);
            RealStart.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void Onclick_FakeStart()
    {
        TutorialStart.SetActive(false);
        BlockScreen.SetActive(true);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        BackgroundScreen.SetActive(true);
        TutorialChat.SetActive(true);
        ChatText.text = Chat[0];
        order++;
    } // 기다리는 동안 게임 화면 보여주기
}
