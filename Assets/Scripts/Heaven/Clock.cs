using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public Image circleImage;
    public float fillDuration = 30f;
    private float fillTimer = 0f;

    private float Daytime = 0;
    private int minutes;
    private int hours;
    public Text ClockText;


    private void Update()
    {
        if (fillTimer < fillDuration)
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
            ClockText.text = string.Format("{0:00}:{1:00}", hours, minutes);
        }
    }
}
