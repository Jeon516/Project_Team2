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
    public GameObject LastEvent;
    public GameObject EndingEvent; // ���� �̺�Ʈ

    public Dictionary<int, string> StatOrder = new Dictionary<int, string>(); // Stat Dictonary
    public int[] Cal = new int[2];

    public int InteractionOneChance;
    public int InteractionTwoChance;
    public int InteractionThreeChance;
    public int Day;
    private int IsHeaven;
    private int IsRandomFree;

    public static UpbringingGameManager Instance { get; private set; } = null;
    public GameObject LoadingScreen;

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

        Day = PlayerPrefs.GetInt("Day",1);
        PlayerPrefs.SetInt("Day", Day);
        IsHeaven = PlayerPrefs.GetInt("IsHeaven",0);
        PlayerPrefs.SetInt("IsHeaven", IsHeaven);
        IsRandomFree = PlayerPrefs.GetInt("IsRandomFree",0);
        PlayerPrefs.SetInt("IsRandomFree", IsRandomFree);
        InteractionOneChance = PlayerPrefs.GetInt("InteractionOneChance", 0);
        PlayerPrefs.SetInt("InteractionOneChance", InteractionOneChance);
        InteractionTwoChance = PlayerPrefs.GetInt("InteractionTwoChance", 0);
        PlayerPrefs.SetInt("InteractionTwoChance", InteractionTwoChance);
        InteractionThreeChance = PlayerPrefs.GetInt("InteractionThreeChance", 0);
        PlayerPrefs.SetInt("InteractionThreeChance", InteractionThreeChance);

        LoadingScreen.SetActive(false);
        EndingEvent.SetActive(false);
    }
    private void Start()
    {
        AudioManager.Instance.PlayBGM("Upbringing");
        AudioManager.Instance.PlaySFX("UpbringingIn");

        if (Day % 20 == 0)
        {
            DayText.text = "20��°";
        }
        else
        {
            Debug.Log("������ ����Ǿ����ϴ�");
            DayText.text = (Day % 20).ToString() + "��°";
        }
        ActionNumText.text = ActionNum.ToString();
        GoldText.text = Gold.ToString();
        LastEvent.SetActive(false);
    }

    private void Update()
    {
        ActionNum = PlayerPrefs.GetInt("ActionNum", 1000);
        PlayerPrefs.SetInt("ActionNum", ActionNum);
        ActionNumText.text = PlayerPrefs.GetInt("ActionNum", 1000).ToString();

        Gold = PlayerPrefs.GetInt("Gold");
        PlayerPrefs.SetInt("Gold", Gold);
        GoldText.text = Gold.ToString();

        InteractionOneChance = PlayerPrefs.GetInt("InteractionOneChance", 0);
        InteractionTwoChance = PlayerPrefs.GetInt("InteractionTwoChance", 0);
        InteractionThreeChance = PlayerPrefs.GetInt("InteractionThreeChance", 0);

        if (InteractionOneChance >= 1)
        {
            BlockInteraction[0].SetActive(true);
        } 
        else
        {
            BlockInteraction[0].SetActive(false);
        }
        if (InteractionTwoChance >= 1)
        {
            BlockInteraction[1].SetActive(true);
        }
        else
        {
            BlockInteraction[1].SetActive(false);
        }
        if (InteractionThreeChance >= 1)
        {
            BlockInteraction[2].SetActive(true);
        }
        else
        {
            BlockInteraction[2].SetActive(false);
        }
    }

    public void OnClick_NextDay()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        PlayerPrefs.SetInt("ActionNum", ActionNum);
        NextDayQuestion.SetActive(true);
    }
    public void OnClick_Random()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (ActionNum>=100)
            RandomQuestion.SetActive(true);
    } // RandomStat

    public void OnClick_Want()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (ActionNum>=150)
            Stat.SetActive(true);
    } // WantStat

    public void OnClick_InteractiQuestion()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        InteractionQuestion.SetActive(true);
    }

    public void NextDayAnswer(bool Yes)
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        NextDayQuestion.SetActive(false);
        if (Yes)
        {
            AudioManager.Instance.PlaySFX("NextDay");
            if (Day %20!=0)
            {
                Day++;
                PlayerPrefs.SetInt("Day", Day);
                PlayerPrefs.SetInt("IsHeaven", 1);
                PlayerPrefs.SetInt("IsRandomFree", 0);
                PlayerPrefs.SetInt("InteractionOneChance", 0);
                PlayerPrefs.SetInt("InteractionTwoChance", 0);
                PlayerPrefs.SetInt("InteractionThreeChance", 0);
                StartCoroutine(LoadingScene());
            } // 20�� ���� �ð��� õ�� ������ �Ѿ
            else if(Day % 20 == 0)
            {
                LastEvent.SetActive(true);
                Day++;
                PlayerPrefs.SetInt("Day", Day);
                PlayerPrefs.SetInt("IsHeaven", 1);
                PlayerPrefs.SetInt("IsRandomFree", 0);
                PlayerPrefs.SetInt("InteractionOneChance", 0);
                PlayerPrefs.SetInt("InteractionTwoChance", 0);
                PlayerPrefs.SetInt("InteractionThreeChance", 0);
                CollectedDog.Instance.LastEvent();
            } // 20�� °���� ������ ������ ��ü�� �������� ����
        }
    }

    public void Onclick_Ending()
    {
        StartCoroutine(EndingScene());
    }

    public IEnumerator LoadingScene()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync("Loading");

        while (!loading.isDone) //�� �ε� �Ϸ�� �ε��Ϸ�� �Ϸ�ȴ�.
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
        }
    }

    public IEnumerator EndingScene()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync("Ending");

        while (!loading.isDone) //�� �ε� �Ϸ�� �ε��Ϸ�� �Ϸ�ȴ�.
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
        }
    }
}
