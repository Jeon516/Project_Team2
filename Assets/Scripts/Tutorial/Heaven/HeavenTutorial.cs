using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HeavenTutorial : MonoBehaviour
{
    public int Delay;
    public RectTransform ChatTransform;
    public Text ChatText;
    public Button StartButton;

    public GameObject RealStart;
    public GameObject TutorialStart;
    public GameObject TutorialChat;
    public GameObject BlockScreen;
    public GameObject[] TutorialInformation;

    private int order;
    private int Day;
    private int TutorialDay;
    private bool IsClick = true;

    private string[] Chat = {  "�տ� �ִ� ��ư�� ������ ������ �����ϰ� �˴ϴ�. �� �� �������ðھ��?", 
        "���� Ƽ�ϰ� ������ ���̽���? ���� �ؾ� �� ���� ������ ž���� �������� Ƽ���� ������ ������ ��ġ�ϴ��� Ȯ���ϰ� �ȳ��ϴ� ���Դϴ�.",
    "���� ������ Ƽ���̸� ������ ��ġ�ϰ�,", "������ ������ Ƽ���̸� �������� ��ġ�ϸ� �˴ϴ�.", 
        "���� ���� �ٸ� Ƽ���� ������ ���� �������� �ִµ�, �װ� ��ȯ����� �ϴ� �Ʒ��� ��ư�� ���� ���������� �մϴ�.",
    "�� ��, �Ǽ��� �ϰ� �Ǹ� ���� �ð��� �پ��� �����ϼ���!", "���? �������� ����� ����? ��, �׷� �� �� ���� �غ����?"};

    private void Awake()
    {
        Day = PlayerPrefs.GetInt("Day");
        PlayerPrefs.SetInt("Day", Day);
        TutorialDay = PlayerPrefs.GetInt("TutorialDay", 0);
        PlayerPrefs.SetInt("TutorialDay", TutorialDay);
    }

    private void Start()
    {
        if (Day == 0 && TutorialDay == 0)
        {
            gameObject.SetActive(true);
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
        }
        else if (Input.GetMouseButtonDown(0) && order == 2 && IsClick)
        {
            TutorialInformation[4].SetActive(false);
            for (int i=0;i<4; i++)
                TutorialInformation[i].SetActive(true);
            ModifyRectTransform(485,-296,900,700);
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
            ModifyRectTransform(0, 53, 1500, 800);
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order == 5 && IsClick)
        {
            for (int i = 7; i < 9; i++)
                TutorialInformation[i].SetActive(true);
            TutorialInformation[1].SetActive(false);
            TutorialInformation[6].SetActive(false);
            ModifyRectTransform(0, -296, 1500, 800);
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order == 6 && IsClick)
        {
            for (int i = 1; i < 9; i++)
                TutorialInformation[i].SetActive(false);
            ModifyRectTransform(0, -296, 1500, 800);
            ChatDisplay();
        }
    }

    public void OnClick_TutorialStart()
    {
        IsClick = false;
        TutorialStart.SetActive(false);
        BlockScreen.SetActive(true);
        StartCoroutine(Wait());
    } // Ʃ�丮���� ���� ��ư ���� ��

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 3; i++)
            TutorialInformation[i].SetActive(true);
        TutorialInformation[4].SetActive(true);
        IsClick = true;
        TutorialChat.SetActive(true);
        ChatDisplay();
    } // ��ٸ��� ���� ���� ȭ�� �����ֱ�

    void ChatDisplay()
    {
        ChatText.text = Chat[order];
        order++;
    }

    private void ModifyRectTransform(int x, int y, int width, int height)
    {
        // Width, Height, Pos X, Pos Y ����
        ChatTransform.sizeDelta = new Vector2(width, height);
        ChatTransform.anchoredPosition = new Vector2(x, y);
    }
}
