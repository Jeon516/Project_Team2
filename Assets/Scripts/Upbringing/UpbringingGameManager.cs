using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpbringingGameManager : MonoBehaviour
{
    private int ActionNum; // ȣ����
    private bool IsPlay;
    private bool IsWalk;
    private bool IsGift; // ��ȣ�ۿ� �ϳ�
    public Text ActionNumText;
    public Text DayText;
    public GameObject Stat;
    public GameObject[] StatValue; // ���� ����� 

    Dictionary<int, string> StatOrder = new Dictionary<int, string>(); // ���� ��ųʸ�
    int[] Cal = new int[2];

    public static UpbringingGameManager Instance { get; private set; } = null;
    private void Awake()
    {
        StatOrder.Add(0, "Energy");
        StatOrder.Add(1, "Sociality");
        StatOrder.Add(2, "Deliberation");
        StatOrder.Add(3, "Curiosoty");
        StatOrder.Add(4, "Love");

        Cal[0] = 1;
        Cal[1] = -1;

        ActionNum = PlayerPrefs.GetInt("ActionNum", 1000);
        PlayerPrefs.SetInt("ActionNum", ActionNum);
        Instance = this;
    }
    private void Start()
    {
        DayText.text = HeavenGameManager.Instance.Day.ToString()+"�� °";
        ActionNumText.text = ActionNum.ToString();
        IsPlay = true; IsWalk = true; IsGift = true;

        //AudioManager.Instance.StopBGM();
        //AudioManager.Instance.playBGM("Upbringing");
    }

    private void Update()
    {
        ActionNumText.text = PlayerPrefs.GetInt("ActionNum", 1000).ToString();
    }

    public void OnClick_NextDay()
    {
        Debug.Log(HeavenGameManager.Instance.Day);
        PlayerPrefs.SetInt("ActionNum", ActionNum);
        if (HeavenGameManager.Instance.Day<20)
        {
            HeavenGameManager.Instance.Day++;
            PlayerPrefs.SetInt("Day", HeavenGameManager.Instance.Day);
            SceneManager.LoadScene("Heaven");

        } // 20�� ���� �ð��� õ�� ������ �Ѿ
        else
        {
            HeavenGameManager.Instance.Day = 0;
            PlayerPrefs.SetInt("Day", HeavenGameManager.Instance.Day);
        } // 20�� °���� ������ ������ ��ü�� �������� ����
    }
    public void OnClick_Play()
    {
        if (IsPlay)
        {
            ActionNum++;
            ActionNumText.text = ActionNum.ToString();
            PlayerPrefs.SetInt("ActionNum", ActionNum);
            IsPlay = false;
        }
    } // ����ֱ� ��ư
    public void OnClick_Walk()
    {
        if (IsWalk)
        {
            ActionNum += Random.Range(0, 2) + 2;
            ActionNumText.text = ActionNum.ToString();
            PlayerPrefs.SetInt("ActionNum", ActionNum);
            IsWalk = false;
        } // ȣ���� 2,3 �ö�
    } // ��å�ϱ� ��ư
    public void OnClick_Gift()
    {
        if (IsGift)
        {
            ActionNum += Random.Range(0, 2) + 5;
            ActionNumText.text = ActionNum.ToString();
            PlayerPrefs.SetInt("ActionNum", ActionNum);
            IsGift = false;
        } // �������� 5,6 ȣ���� �ö�
    } // �����ϱ� ��ư
    public void OnClick_Random()
    {
        if(ActionNum>=50)
        {
            ActionNum -= 50; 
            ActionNumText.text = ActionNum.ToString();
            PlayerPrefs.SetInt("ActionNum", ActionNum);

            int StatOrderNum = Random.Range(0, 5);
            int StatNum = Random.Range(0, 2);

            while(PlayerPrefs.GetInt(StatOrder[StatOrderNum]) + Cal[StatNum]>4 
                || PlayerPrefs.GetInt(StatOrder[StatOrderNum]) + Cal[StatNum] < -4)
            {
                StatOrderNum = Random.Range(0, 5);
                StatNum = Random.Range(0, 2);
            }

            PlayerPrefs.SetInt(StatOrder[StatOrderNum], PlayerPrefs.GetInt(StatOrder[StatOrderNum])+Cal[StatNum]);
            PlayerPrefs.SetFloat(StatOrder[StatOrderNum] + "X", PlayerPrefs.GetFloat(StatOrder[StatOrderNum] + "X") - 111 * Cal[StatNum]);
        }
    } // '������ ���� +1' ��ư

    public void OnClick_Want()
    {
        Stat.SetActive(true);
    } // '���ϴ� ���� +1' ��ư
}
