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
    private int Day;

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

        Day = PlayerPrefs.GetInt("Day");
        PlayerPrefs.SetInt("Day", Day);
    }
    private void Start()
    {
        DayText.text = Day.ToString() + "�� °";
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
        else
        {
            for (int i = 0; i < 3; i++)
            {
                BlockInteraction[i].SetActive(false);
            }
        }
    }

    public void OnClick_NextDay()
    {
        PlayerPrefs.SetInt("ActionNum", ActionNum);
        PlayerPrefs.SetInt("Interaction", 0);
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
            if (Day < 20)
            {
                Day++;
                PlayerPrefs.SetInt("IsHeaven", 1);
                PlayerPrefs.SetInt("Day", Day);
                SceneManager.LoadScene("Heaven");

            } // 20�� ���� �ð��� õ�� ������ �Ѿ
            else
            {
                Day = 0;
                PlayerPrefs.SetInt("Day", Day);
            } // 20�� °���� ������ ������ ��ü�� �������� ����
        }
    }
}
