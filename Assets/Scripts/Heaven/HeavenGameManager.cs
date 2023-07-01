using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeavenGameManager : MonoBehaviour
{
    public int Money;
    public int Day;
    public Text DayText;

    public static HeavenGameManager Instance { get; private set; } = null;
    private void Awake()
    {
        Day = PlayerPrefs.GetInt("Day", 1);
        PlayerPrefs.SetInt("Day", Day);
        Instance = this;
    }
    private void Start()
    {
        DayText.text = Day.ToString() + "ÀÏ Â°";
    }
    public void OnClick_LeftStation()
    {
        Debug.Log("Left");
    }
    public void OnClick_RightStation()
    {
        Debug.Log("Right");
    }
    public void OnClick_CancelStation()
    {
        Debug.Log("Cancel");
    }

}
