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

    private string[] Chat = {  "�տ� �ִ� ��ư�� ������ ������ �����ϰ� �˴ϴ�. �� �� �������ðھ��?", 
        "���� Ƽ�ϰ� ������ ���̽���? ���� �ؾ� �� ���� ������ ž���� �������� Ƽ���� ������ ������ ��ġ�ϴ��� Ȯ���ϰ� �ȳ��ϴ� ���Դϴ�.",
    "���� ������ Ƽ���̸� ������ ��ġ�ϰ�,", "������ ������ Ƽ���̸� �������� ��ġ�ϸ� �˴ϴ�.", 
        "���� ���� �ٸ� Ƽ���� ������ ���� �������� �ִµ�, �װ� ��ȯ����� �ϴ� �Ʒ��� ��ư�� ���� ���������� �մϴ�.",
    "�� ��, �Ǽ��� �ϰ� �Ǹ� ���� �ð��� �پ��� �����ϼ���!", "���? �������� ����� ����? ��, �׷� �� �� ���� �غ����?"};

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
