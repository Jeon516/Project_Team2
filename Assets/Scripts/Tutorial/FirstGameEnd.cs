using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstGameEnd : MonoBehaviour
{
    public Text NameText;
    public Text ChatText;
    public GameObject GhostImage;
    private int order;
    private int Day;
    private string PlayerName;
    private Color PlayerNameColor;
    private Color PlayerChatColor;
    private Color JayNameColor;
    private Color JayChatColor;

    private string[] Chat = {  "�����ϼ̽��ϴ�! \n ������ �����ó׿�!",
        "(�����Ҹ� ������ ���� ������ \n���� ���� ���ĺ��� �ִ�.)",
    "����. �Ʊ���� �ñ��� �� �ִµ�, \n�� ���ɵ鵵 ��������?",
        "�� �¾ƿ�. ������ ����� ������ \n������ �������� ���� �ſ�.",
        "����� ã���� ����� ã�� �� ������.. \n�ΰ��� ������ �����Ǿ ���� �Ұ����ؿ�.",
        "���������� �����.. \n��κ� ������� ����̴ϱ��",
        "����� ã���� ����� ������? \n�ִٸ� ���� �����ְ� �;�.",
    "������ �� ������.. \n������ �� ������ �˷��帱�Կ�.",
    "(���̰� �����ִ� ������ �����´�)",
    };

    private void Awake()
    {
        Day = PlayerPrefs.GetInt("Day");
        PlayerPrefs.SetInt("Day", Day);
        PlayerName = PlayerPrefs.GetString("Player");

        PlayerNameColor = new Color32(47, 110, 255, 255);
        PlayerChatColor = new Color32(50, 50, 50, 255);
        JayNameColor = new Color32(84, 84, 84, 255);
        JayChatColor = new Color32(123, 123, 123, 255);
    }

    private void Start()
    {
        if (Day%20 == 1 && Day/20==0)
        {
            NameText.text = "����";
            gameObject.SetActive(true);
            ChatText.text = Chat[0];
            order = 1;
        }
        else
            gameObject.SetActive(false);
    }

    void Update()
    {
        if (order == Chat.Length && Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("Upbringing");
        }
        else if (Input.GetMouseButtonDown(0) && (order == 3 || order == 7))
        {
            GhostImage.SetActive(true);
            NameText.text = "����";
            ChatText.text = Chat[order];
            NameText.color = JayNameColor;
            ChatText.color = JayChatColor;
            order++;
        }
        else if (Input.GetMouseButtonDown(0) && (order == 2||order==6))
        {
            GhostImage.SetActive(true);
            NameText.text = PlayerName;
            ChatText.text = Chat[order];
            NameText.color = PlayerNameColor;
            ChatText.color = PlayerChatColor;
            order++;
        }
        else if (Input.GetMouseButtonDown(0) && (order==1|| order == 8))
        {
            GhostImage.SetActive(true);
            NameText.text = "";
            ChatText.text = Chat[order];
            order++;
        }
        else if (Input.GetMouseButtonDown(0) && Chat.Length > order)
        {
            ChatText.text = Chat[order];
            order++;
        }
    }
}
