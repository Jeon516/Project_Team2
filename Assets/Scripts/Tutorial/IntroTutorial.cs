using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroTutorial : MonoBehaviour
{
    public GameObject NameQuestion;
    public Text ChatText;
    private int order;
    private string Name;
    private bool IsClick=true;
    private int Day;

    private string[] Chat = {  "��, �̹��� 7�� �����Ҹ� ����ϰ� �� ���̽Ű���?","Ȥ�� ������ ��� �ǽǱ��?",
    "����... Ȯ���߽��ϴ�", "�ݰ����ϴ�! ���� 7�� �������� �μ��ΰ踦 �ð� �� '����'��� �մϴ�.",
        "������ �ؾ� �� �Ͽ� ���� �˷��帱 �״�,�� ���ð� �����Ͻø� �˴ϴ�."};

    private void Awake()
    {
        Day = PlayerPrefs.GetInt("Day", 0);
        PlayerPrefs.SetInt("Day", Day);
    }

    private void Start()
    {
        if (Day >= 1)
            gameObject.SetActive(false);
        else if (Day / 20 == 0 && Day % 20 == 0)
        {
            gameObject.SetActive(true);
            ChatText.text = Chat[0];
            order = 1;
        }
    }
    void Update()
    {

        if (order == Chat.Length && Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
            Day++;
            PlayerPrefs.SetInt("Day", Day);
            SceneManager.LoadScene("Heaven");
        }
        if (Input.GetMouseButtonDown(0) && Chat.Length>order && IsClick)
        {
            ChatText.text = Chat[order];
            if (order == 2)
            {
                NameCreate();
                ChatText.text = "";
                order++;
            }
            order++;
        }
    }

    void NameCreate()
    {
        NameQuestion.SetActive(true);
        IsClick = false;
    }

    public void OnClick_Check()
    {
        NameQuestion.SetActive(false);
        IsClick = true;
        ChatText.text = Chat[3];
    }
}
