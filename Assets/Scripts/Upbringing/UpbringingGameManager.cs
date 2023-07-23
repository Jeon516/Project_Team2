using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpbringingGameManager : MonoBehaviour
{
    public int ActionNum; // Flavor
    public Text ActionNumText;
    public int Gold; // Money
    public Text GoldText;
    public Text DayText;
    public GameObject Stat;
    public GameObject[] StatValue; // Stat stick
    public GameObject[] BlockInteraction;

    public GameObject RandomQuestion;
    public GameObject InteractionQuestion;
    public GameObject NextDayQuestion;

    public Dictionary<int, string> StatOrder = new Dictionary<int, string>(); // Stat Dictonary
    public int[] Cal = new int[2];

    public int InteractionChance;

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

        Instance = this;
    }
    private void Start()
    {
        DayText.text = HeavenGameManager.Instance.Day.ToString()+"일 째";
        ActionNumText.text = ActionNum.ToString();
    }

    private void Update()
    {
        ActionNum= PlayerPrefs.GetInt("ActionNum", 1000);
        PlayerPrefs.SetInt("ActionNum", ActionNum);
        ActionNumText.text = PlayerPrefs.GetInt("ActionNum", 1000).ToString();

        Gold = PlayerPrefs.GetInt("Gold");
        PlayerPrefs.SetInt("Gold", Gold);
        GoldText.text = Gold.ToString();

        InteractionChance = PlayerPrefs.GetInt("Interaction", 0);
        PlayerPrefs.SetInt("Interaction", InteractionChance);
        if(InteractionChance>=1)
        {
            for (int i = 0; i < 3; i++)
            {
                BlockInteraction[i].SetActive(true);
            }
        } // One Interaction
    }

    public void OnClick_NextDay()
    {
        PlayerPrefs.SetInt("ActionNum", ActionNum);
        NextDayQuestion.SetActive(true);
    }
    public void OnClick_Random()
    {
        if(ActionNum>=50)
            RandomQuestion.SetActive(true);
    } // RandomStat

    public void OnClick_Want()
    {
        if(ActionNum>=100)
            Stat.SetActive(true);
    } // WantStat

    public void OnClick_InteractiQuestion()
    {
        InteractionQuestion.SetActive(true);
    }

    public void NextDayAnswer(bool Yes)
    {
        NextDayQuestion.SetActive(false);
        if (Yes)
        {
            if (HeavenGameManager.Instance.Day < 20)
            {
                HeavenGameManager.Instance.Day++;
                PlayerPrefs.SetInt("IsHeaven", 1);
                PlayerPrefs.SetInt("Day", HeavenGameManager.Instance.Day);
                SceneManager.LoadScene("Heaven");

            } // 20일 내의 시간은 천국 씬으로 넘어감
            else
            {
                HeavenGameManager.Instance.Day = 0;
                PlayerPrefs.SetInt("Day", HeavenGameManager.Instance.Day);
            } // 20일 째에는 강아지 유령의 정체가 밝혀지는 순간
        }
    }
}
