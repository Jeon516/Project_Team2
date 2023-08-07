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

    private string[] ChatFour = {  "��ø���! ���� �����ϱ� ���� �˷��帱 �ҽ��� �־��.","���� �ٸ� �����ҿ��� õ�� �̸���\n �ٸ� Ƽ���� ��Ÿ���ٰ� �ؿ�.",
    "�׷��� ���ú��ʹ� ������ Ƽ���� � õ���� ��������,\n� õ���� Ƽ������ �� �Ĳ��ϰ� Ȯ���ؾ� �ؿ�.","���޵帱 �ҽ��� �̰� ���Դϴ�.\n�׷�, ���� �Ϸ絵 �������Դϴ�!"};

    private string[] ChatSeven = {  "��ø���! ���� �����ϱ� ���� �˷��帱 �ҽ��� �־��.","���� �ٸ� �����ҿ��� õ�� ������\n �ٸ� Ƽ���� ��Ÿ���ٰ� �ؿ�.",
    "�׷��� ���ú��ʹ� ������ Ƽ���� ������ ��ġ�ϴ���\n �� �Ĳ��ϰ� Ȯ���ؾ� �ؿ�.","���޵帱 �ҽ��� �̰� ���Դϴ�.\n�׷�, ���� �Ϸ絵 �������Դϴ�!"};

    private string[] Chat = {  "��ø���! ���� �����ϱ� ���� �˷��帱 �ҽ��� �־��.","���� �ٸ� �����ҿ��� õ�� �̸��� �ٸ� Ƽ����\n ��Ÿ���ٰ� �ؿ�.",
    "�׷��� ���ú��ʹ� ������ Ƽ���� � õ���� ��������,\n� õ���� Ƽ������ �� �Ĳ��ϰ� Ȯ���ؾ� �ؿ�.","���޵帱 �ҽ��� �̰� ���Դϴ�.\n�׷�, ���� �Ϸ絵 �������Դϴ�!"};

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
    } // ��ٸ��� ���� ���� ȭ�� �����ֱ�
}
