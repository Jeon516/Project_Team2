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

    public static Clock Instance { get; private set; } = null;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (fillTimer < fillDuration && HeavenGameManager.Instance.Play)
        {
            ClockMove();
        }

        if(fillTimer >= fillDuration)
        {
            GameResult.SetActive(true);
            circleImage.fillAmount = 1;
            ClockStick.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
    }

    private void ClockMove()
    {
        fillTimer += Time.deltaTime;

        float fillAmount = fillTimer / fillDuration;
        circleImage.fillAmount = fillAmount;

        float clockSpeed = -360f / fillDuration; // 시계바늘의 속도 (1초에 움직이는 각도)
        float rotationAngle = clockSpeed * fillTimer; // 시계바늘이 1바퀴 도는데 걸리는 시간과 맞춤
        ClockStick.transform.localEulerAngles = new Vector3(0f, 0f, rotationAngle);
    }
} // �ð� �����̱�

