using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpbringingGameManager : MonoBehaviour
{
    private int ActionNum; // ȣ����
    private int RandomNumber = Random.Range(0, 2);
    private bool IsPlay;
    private bool IsWalk;
    private bool IsGift;
    public Text NextDayText;
    public Text ActionNumText;
    public Text DayText;

    public static UpbringingGameManager Instance { get; private set; } = null;
    private void Awake()
    {
        ActionNum = PlayerPrefs.GetInt("ActionNum", 0);
        PlayerPrefs.SetInt("ActionNum", ActionNum);
        Instance = this;
    }
    private void Start()
    {
        DayText.text = HeavenGameManager.Instance.Day.ToString()+"�� °";
        ActionNumText.text = ActionNum.ToString();
        IsPlay = true; IsWalk = true; IsGift = true;

        AudioManager.Instance.StopBGM();
        AudioManager.Instance.playBGM("Upbringing");
    }
    private void NextDay()
    {
        if (HeavenGameManager.Instance.Day < 20)
        {
            NextDayText.text = "���� ��";

        } // 20�� ���� �ð��� ���� ���� �ؽ�Ʈ
        else
        {
            NextDayText.text = "���� ����";
        } // 20�� °���� ���� ���� �ؽ�Ʈ
        PlayerPrefs.SetInt("ActionNum", ActionNum);
    }
    public void OnClick_NextDay()
    {
        NextDay();
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
            IsPlay = false;
        }
    } // ȣ���� 1�ö�
    public void OnClick_Walk()
    {
        if (IsWalk)
        {
            ActionNum += RandomNumber + 2;
            ActionNumText.text = ActionNum.ToString();
            IsWalk = false;
        }
    } // �������� 2,3 ȣ���� �ö�
    public void OnClick_Gift()
    {
        if (IsGift)
        {
            ActionNum += RandomNumber + 5;
            ActionNumText.text = ActionNum.ToString();
            IsGift = false;
        }
    } // �������� 5,6 ȣ���� �ö�
    public void OnClick_Random()
    {
        if(ActionNum>=50)
        {
            ActionNum -= 50; 
            ActionNumText.text = ActionNum.ToString();
        }
    } 
    public void OnClick_Want()
    {
        if (ActionNum >= 100)
        {
            ActionNum -= 100;
            ActionNumText.text = ActionNum.ToString();
        }
    }
}