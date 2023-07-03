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
    private bool IsGift; // 상호작용 하나
    public Text ActionNumText;
    public Text DayText;
    public GameObject Stat;
    public GameObject[] StatValue; // 스탯 막대기 

    Dictionary<int, string> StatOrder = new Dictionary<int, string>(); // 스탯 딕셔너리
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
        DayText.text = HeavenGameManager.Instance.Day.ToString()+"일 째";
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
            PlayerPrefs.SetInt("ActionNum", ActionNum);
            IsPlay = false;
        }
    } // 놀아주기 버튼
    public void OnClick_Walk()
    {
        if (IsWalk)
        {
            ActionNum += Random.Range(0, 2) + 2;
            ActionNumText.text = ActionNum.ToString();
            PlayerPrefs.SetInt("ActionNum", ActionNum);
            IsWalk = false;
        } // 호감도 2,3 올라감
    } // 산책하기 버튼
    public void OnClick_Gift()
    {
        if (IsGift)
        {
            ActionNum += Random.Range(0, 2) + 5;
            ActionNumText.text = ActionNum.ToString();
            PlayerPrefs.SetInt("ActionNum", ActionNum);
            IsGift = false;
        } // 랜덤으로 5,6 호감도 올라감
    } // 선물하기 버튼
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
    } // '무작위 성향 +1' 버튼

    public void OnClick_Want()
    {
        Stat.SetActive(true);
    } // '원하는 성향 +1' 버튼
}
