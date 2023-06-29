using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpbringingGameManager : MonoBehaviour
{
    private int ActionNum; // 호감도
    private bool IsPlay;
    private bool IsWalk;
    private bool IsGift;
    public Text NextDayText;
    public Text ActionNumText;
    public Text DayText;
    public GameObject Stat;
    public GameObject[] StatValue; // 스탯 막대기 

    public static UpbringingGameManager Instance { get; private set; } = null;
    private void Awake()
    {
        ActionNum = PlayerPrefs.GetInt("ActionNum", 1000);
        PlayerPrefs.SetInt("ActionNum", ActionNum);
        Instance = this;
    }
    private void Start()
    {
        DayText.text = HeavenGameManager.Instance.Day.ToString()+"일 째";
        ActionNumText.text = ActionNum.ToString();
        IsPlay = true; IsWalk = true; IsGift = true;

        //AudioManager.Instance.StopBGM();
        //AudioManager.Instance.playBGM("Upbringing");
    }
    private void NextDay()
    {
        if (HeavenGameManager.Instance.Day < 20)
        {
            NextDayText.text = "다음 날";

        } // 20일 내의 시간은 다음 날로 텍스트
        else
        {
            NextDayText.text = "육성 종료";
        } // 20일 째에는 육성 종료 텍스트
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

        } // 20일 내의 시간은 천국 씬으로 넘어감
        else
        {
            HeavenGameManager.Instance.Day = 0;
            PlayerPrefs.SetInt("Day", HeavenGameManager.Instance.Day);
        } // 20일 째에는 강아지 유령의 정체가 밝혀지는 순간
    }
    public void OnClick_Play()
    {
        if (IsPlay)
        {
            ActionNum++;
            ActionNumText.text = ActionNum.ToString();
            IsPlay = false;
        }
    } // 놀아주기 버튼
    public void OnClick_Walk()
    {
        if (IsWalk)
        {
            ActionNum += Random.Range(0, 2) + 2;
            ActionNumText.text = ActionNum.ToString();
            IsWalk = false;
        } // 호감도 2,3 올라감
    } // 산책하기 버튼
    public void OnClick_Gift()
    {
        if (IsGift)
        {
            ActionNum += Random.Range(0, 2) + 5;
            ActionNumText.text = ActionNum.ToString();
            IsGift = false;
        } // 랜덤으로 5,6 호감도 올라감
    } // 선물하기 버튼
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
        /*if (ActionNum >= 100)
        {
            ActionNum -= 100;
            Stat.SetActive(true);
        }*/
        Stat.SetActive(true);
    }
    private void StatMove()
    {
        ;
    } // 스탯을 찍으면 스탯창에 반영
}
