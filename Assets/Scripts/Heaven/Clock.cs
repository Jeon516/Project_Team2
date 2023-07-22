using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Clock : MonoBehaviour
{
    public Image circleImage;
    public GameObject ClockStick;
    public float fillDuration = 30f; // ���� �ð�
    public GameObject GameResult;
    public float fillTimer = 0f;

    private void Update()
    {
        if (fillTimer < fillDuration && HeavenGameManager.Instance.Play)
        {
            ClockMove();
        }

        if(fillTimer >= fillDuration)
        {
            GameResult.SetActive(true);
            PlayerPrefs.SetInt("IsHeaven", 0);
        }
    }

    private void ClockMove()
    {
        fillTimer += Time.deltaTime;
        float fillAmount = fillTimer / fillDuration;
        circleImage.fillAmount = fillAmount;

        ClockStick.transform.Rotate(Vector3.forward, -360 / fillDuration * Time.deltaTime);
    } // �ð� �����̱�
}
