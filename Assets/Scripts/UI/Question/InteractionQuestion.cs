using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionQuestion : MonoBehaviour
{
    private int InteractionNum;

    public static InteractionQuestion Instance { get; private set; } = null;

    private void Awake()
    {
        Instance = this;
    }

    public void OnClick_Play(int index)
    {
        InteractionNum = 0;
    } // 놀아주기 버튼
    public void OnClick_Walk(int index)
    {
        InteractionNum = 1;
    } // 산책하기 버튼
    public void OnClick_Gift(int index)
    {
        InteractionNum = 2;
    } // 선물하기 버튼

    public void BlockButton()
    {
        for (int i = 0; i < 3; i++)
        {
            UpbringingGameManager.Instance.BlockInteraction[i].SetActive(true);
        }
    } // 상호작용 막기

    public void OnClick_Button(bool Yes)
    {
        if (Yes && PlayerPrefs.GetInt("Interaction")<1)
        {
            if (InteractionNum == 0)
            {
                UpbringingGameManager.Instance.ActionNum++;
            }
            else if (InteractionNum == 1)
            {
                UpbringingGameManager.Instance.ActionNum += Random.Range(0, 2) + 2;
            }
            else if (InteractionNum == 2)
            {
                UpbringingGameManager.Instance.ActionNum += Random.Range(0, 2) + 5;
            }
            UpbringingGameManager.Instance.ActionNumText.text = UpbringingGameManager.Instance.ActionNum.ToString();
            PlayerPrefs.SetInt("ActionNum", UpbringingGameManager.Instance.ActionNum);
            UpbringingGameManager.Instance.InteractionChance++;
            BlockButton();
        }
        gameObject.SetActive(false);
    }
}
