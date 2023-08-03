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
    public GameObject LastEvent; // FGT용 팝업창
    public GameObject EndingEvent; // 엔딩 이벤트

    public Dictionary<int, string> StatOrder = new Dictionary<int, string>(); // Stat Dictonary
    public int[] Cal = new int[2];

    public int InteractionChance;
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

        Day = PlayerPrefs.GetInt("Day");
        PlayerPrefs.SetInt("Day", Day);
        IsHeaven = PlayerPrefs.GetInt("IsHeaven");
        PlayerPrefs.SetInt("IsHeaven", IsHeaven);
        IsRandomFree = PlayerPrefs.GetInt("IsRandomFree");
        PlayerPrefs.SetInt("IsRandomFree", IsRandomFree);

        LoadingScreen.SetActive(false);
        EndingEvent.SetActive(false);

        if(CollectedDog.Instance.CollectedDogDatas.collectedDogData.Count==32)
        {
            EndingEvent.SetActive(true);
        }
    }
    private void Start()
    {
        AudioManager.Instance.PlayBGM("Upbringing");
        AudioManager.Instance.PlaySFX("UpbringingIn");

        if (Day%20==0)
        {
            DayText.text = "20일 째";
        }
        else
        {
            DayText.text = (Day % 20).ToString() + "일 째";
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
                PlayerPrefs.SetInt("Interaction", 0);
                StartCoroutine(LoadingScene());
            } // 20일 내의 시간은 천국 씬으로 넘어감
            else if(Day % 20 == 0)
            {
                LastEvent.SetActive(true);
                Day++;
                PlayerPrefs.SetInt("Day", Day);
                PlayerPrefs.SetInt("IsHeaven", 1);
                PlayerPrefs.SetInt("IsRandomFree", 0);
                PlayerPrefs.SetInt("Interaction", 0);
                CollectedDog.Instance.LastEvent();
            } // 20일 째에는 강아지 유령의 정체가 밝혀지는 순간
        }
    }

    public void Onclick_Ending()
    {
        StartCoroutine(EndingScene());
    }

    public IEnumerator LoadingScene()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync("Loading");

        while (!loading.isDone) //씬 로딩 완료시 로딩완료시 완료된다.
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
        }
    }

    public IEnumerator EndingScene()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync("Ending");

        while (!loading.isDone) //씬 로딩 완료시 로딩완료시 완료된다.
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
        }
    }
}
