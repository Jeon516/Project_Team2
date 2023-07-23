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

    public static HeavenGameManager Instance { get; private set; } = null;
    private void Awake()
    {
        Day = PlayerPrefs.GetInt("Day");
        PlayerPrefs.SetInt("Day", Day);
        Instance = this;
        Gold = PlayerPrefs.GetInt("Gold");
        PlayerPrefs.SetInt("Gold", Gold);
    }

    private void Start()
    {
        DayText.text = Day.ToString() + "일째";
        if (Day >=2)
            GameStart.SetActive(true);
        else
            GameStart.SetActive(false);
    }

    public void OnClick_LeftStation()
    {
        DogWait.Instance.ChangeImage();
        Debug.Log("Left");
    }
    public void OnClick_RightStation()
    {
        DogWait.Instance.ChangeImage();
        Debug.Log("Right");
    }
    public void OnClick_CancelStation()
    {
        DogWait.Instance.ChangeImage();
        Debug.Log("Cancel");
    }

    public void OnClick_GameStart()
    {
        GameStart.SetActive(false);
        Play = true;
    } // ���� ��ŸƮ

    public void OnClick_NextScene()
    {
        if(Day / 20 == 0 && Day % 20 == 1)
            SceneManager.LoadScene("Tutorial");
        else
            SceneManager.LoadScene("Upbringing");
    }
}
