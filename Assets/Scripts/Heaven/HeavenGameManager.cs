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

    public static HeavenGameManager Instance { get; private set; } = null;
    private void Awake()
    {
        Day = 1;
        PlayerPrefs.SetInt("Day", 1);
        Instance = this;
    }
    private void Start()
    {
        DayText.text = Day.ToString() + "일 째";
        GameStart.SetActive(true);
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
    } // 게임 스타트

    public void OnClick_NextScene()
    {
        SceneManager.LoadScene("Upbringing");
    }
}
