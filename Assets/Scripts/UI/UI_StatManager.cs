using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatManager : MonoBehaviour
{
    public GameObject[] State; // 스탯 막대기들

    public void Onclick_Increase(int index)
    {
        if(index==0 && StatManager.Instance.Energy<4)
        {
            StatManager.Instance.Energy++;
            PlayerPrefs.SetInt("Energy", StatManager.Instance.Energy);
        }
        else if (index == 1 && StatManager.Instance.Sociality < 4)
        {
            StatManager.Instance.Sociality++;
            PlayerPrefs.SetInt("Sociality", StatManager.Instance.Sociality);
        }
        else if (index == 2 && StatManager.Instance.Deliberation < 4)
        {
            StatManager.Instance.Deliberation++;
            PlayerPrefs.SetInt("Deliberation", StatManager.Instance.Deliberation);
        }
        else if (index == 3 && StatManager.Instance.Curiosoty < 4)
        {
            StatManager.Instance.Curiosoty++;
            PlayerPrefs.SetInt("Curiosoty", StatManager.Instance.Curiosoty);
        }
        else if (index == 4 && StatManager.Instance.Love < 4)
        {
            StatManager.Instance.Love++;
            PlayerPrefs.SetInt("Love", StatManager.Instance.Love);
        }
        RectTransform rectTransform = State[index].GetComponent<RectTransform>();
        rectTransform.anchoredPosition -= new Vector2(111, 0f);
        gameObject.SetActive(false);
    } // 왼쪽 버튼 누르면 각 스탯 증가, 위쪽부터 아래까지 0 ~ 4번째
    public void Onclick_Decrease(int index)
    {
        if (index == 0 && StatManager.Instance.Energy < 4)
        {
            StatManager.Instance.Energy--;
            PlayerPrefs.SetInt("Energy", StatManager.Instance.Energy);
        }
        else if (index == 1 && StatManager.Instance.Sociality < 4)
        {
            StatManager.Instance.Sociality--;
            PlayerPrefs.SetInt("Sociality", StatManager.Instance.Sociality);
        }
        else if (index == 2 && StatManager.Instance.Deliberation < 4)
        {
            StatManager.Instance.Deliberation--;
            PlayerPrefs.SetInt("Deliberation", StatManager.Instance.Deliberation);
        }
        else if (index == 3 && StatManager.Instance.Curiosoty < 4)
        {
            StatManager.Instance.Curiosoty--;
            PlayerPrefs.SetInt("Curiosoty", StatManager.Instance.Curiosoty);
        }
        else if (index == 4 && StatManager.Instance.Love < 4)
        {
            StatManager.Instance.Love--;
            PlayerPrefs.SetInt("Love", StatManager.Instance.Love);
        }
        RectTransform rectTransform = State[index].GetComponent<RectTransform>();
        rectTransform.anchoredPosition += new Vector2(111, 0f);
        gameObject.SetActive(false);
    } // 오른쪽 버튼 누르면 각 스탯 감소, 위쪽부터 아래까지 0 ~ 4번째
}
