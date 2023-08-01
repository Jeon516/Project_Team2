using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstGameEnd : MonoBehaviour
{
    public Text ChatText;
    private int order;
    private int Day;

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
    }

    private void Start()
    {
        Debug.Log(Day);
        if (Day%20 == 1 && Day/20==0)
        {
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
        if (Input.GetMouseButtonDown(0) && Chat.Length > order)
        {
            ChatText.text = Chat[order];
            order++;
        }
    }
}
