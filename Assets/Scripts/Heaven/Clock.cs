using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Clock : MonoBehaviour
{
    public Image circleImage;
    public float fillDuration = 30f;
    private float fillTimer = 0f;

    private float Daytime = 0;
    private int minutes;
    private int hours;

    private void Update()
    {
        if (fillTimer < fillDuration)
        {
            ClockMove();
        }

        if(fillTimer >= fillDuration)
        {
            NextLoadScene();
        }
    }

    private void NextLoadScene()
    {
        SceneManager.LoadScene("Upbringing");
    } // 다음 씬으로 이동

    private void ClockMove()
    {
        fillTimer += Time.deltaTime;
        float fillAmount = fillTimer / fillDuration;
        circleImage.fillAmount = fillAmount;

        Daytime += Time.deltaTime * (540 / fillDuration);

        hours = 9 + (int)Daytime / 60;
        minutes = (int)Daytime % 60;

        if (hours > 12)
        {
            hours -= 12;
        }
    } // 시계 움직이기
}
