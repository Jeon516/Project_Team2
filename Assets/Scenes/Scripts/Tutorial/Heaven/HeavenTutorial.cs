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

    private string[] Chat = {  "�տ� �ִ� ��ư�� ������ ������ ���۵˴ϴ�. \n�� �� �������ðھ��?", 
        "���� �ؾ� �� ���� ���������� ��� ���� Ƽ���� \n������ ��ġ�ϴ��� Ȯ���ϰ� �ȳ��ϴ� ���Դϴ�.",
    "���� ������ Ƽ���̸� \n������ ��ġ�ϰ�,", "������ ������ Ƽ���̸� \n�������� ��ġ�ϸ� �˴ϴ�.", 
        "���� �ٸ� Ƽ���� ������ ���� �������� �ִµ�, \n�װ� ��ȯ����� �ϴ� �Ʒ��� ��ư�� ���� ������������.",
    "����, �̸�, ���� �� �ϳ��� �ٸ��� \n�� õ������ �� �� ������ �Ĳ��� ���ּ���!", "�� ��, �Ǽ��ϸ� ���� �ð��� �پ��ϴ�! \n��, �׷� �� �� ���� �غ����?"};

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

    private void ModifyTextRectTransform(int x, int y, int width, int height)
    {
        // Width, Height, Pos X, Pos Y ����
        NameTransform.sizeDelta = new Vector2(width, height);
        NameTransform.anchoredPosition = new Vector2(x, y);
    }
}
