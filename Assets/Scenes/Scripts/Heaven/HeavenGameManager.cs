using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeavenGameManager : MonoBehaviour
{
    public int Money;
    public int Day;
    public Text DayText;
    public GameObject GameStart;
    public bool Play = false;
    public int Gold;
    public Text GoldText;
    public GameObject LoadingScreen;
    private int IsHeaven;

    public static HeavenGameManager Instance { get; private set; } = null;

    private void Awake()
    {
        Day = PlayerPrefs.GetInt("Day");
        PlayerPrefs.SetInt("Day", Day);
        Instance = this;
        Gold = PlayerPrefs.GetInt("Gold",100000);
        PlayerPrefs.SetInt("Gold", Gold);
        GoldText.text = Gold.ToString();
        IsHeaven = PlayerPrefs.GetInt("IsHeaven");
        PlayerPrefs.SetInt("IsHeaven", IsHeaven);

        LoadingScreen.SetActive(false);
    }

    private void Start()
    {
        AudioManager.Instance.PlayBGM("Heaven");
        if (Day % 20 == 0)
        {
            DayText.text = "20일째";
        }
        else
        {
            DayText.text = (Day % 20).ToString() + "일째";
        }
       if (Day >=2)
            GameStart.SetActive(true);
       else
            GameStart.SetActive(false);
    }

    public void OnClick_LeftStation()
    {
        DogWait.Instance.ChangeImage();
    }
    public void OnClick_RightStation()
    {
        DogWait.Instance.ChangeImage();
    }
    public void OnClick_CancelStation()
    {
        DogWait.Instance.ChangeImage();
    }

    public void OnClick_GameStart()
    {
        AudioManager.Instance.PlaySFX("GameStart");
        GameStart.SetActive(false);
        Play = true;
    } // ���� ��ŸƮ

    public void OnClick_NextScene()
    {
        if (Day / 20 == 0 && Day % 20 == 1)
        {
            StartCoroutine(TutorialLoadingScene());
        }
        else
        {
            PlayerPrefs.SetInt("IsHeaven", 0);
            LoadingScreen.SetActive(true);
            StartCoroutine(LoadingScene());
        }
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

    public IEnumerator TutorialLoadingScene()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync("Tutorial");

        while (!loading.isDone) //씬 로딩 완료시 로딩완료시 완료된다.
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
        }
    }
}
