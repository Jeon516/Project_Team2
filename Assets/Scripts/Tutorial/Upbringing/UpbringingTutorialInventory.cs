using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpbringingTutorialInventory : MonoBehaviour
{
    private int order = 0;
    private int Day;
    private int TutorialDay;
    private bool IsClick = false;

    public Text ChatText;
    public GameObject ChatSet;
    public GameObject TutorialEat;
    public RectTransform ChatTransform;

    private string[] Chat = { "������ ������ �̰����� Ȯ���� �� �ֽ��ϴ�.", "��� ������ ������ ���� ���̳׿�. �� �� Ȯ���غ��ðھ��?",
    "������ ���Ŀ� ���� ������ ���⼭ Ȯ���� �� �ֽ��ϴ�.","������ �Ϸ翡 �� ������ ���� �� ������, ������ �ϳ��ۿ� ������ ���ɿ��� �� ������ �Կ����ô�.",
    "�̷��� ������ �Կ��� �� �̰����� ������ ������ ���ϴ� ���� Ȯ���� �� �ֽ��ϴ�."};

    private void Awake()
    {
        Day = PlayerPrefs.GetInt("Day");
        PlayerPrefs.SetInt("Day", Day);
        TutorialDay = PlayerPrefs.GetInt("TutorialDay", 0);
        PlayerPrefs.SetInt("TutorialDay", TutorialDay);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && order == 2 && IsClick)
        {
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order == 3 && IsClick)
        {
            ChatDisplay();
        }
    }

    public void Onclick_Cancel()
    {
        TutorialEat.SetActive(true);
        ChatSet.SetActive(true);
        IsClick = true;
        ChatDisplay();
    }

    public void Onclick_TutorialInventory()
    {
        TutorialEat.SetActive(false);
        order++;
        ChatDisplay();
        IsClick = true;
    }

    void ChatDisplay()
    {
        ChatText.text = Chat[order];
        order++;
    }
}
