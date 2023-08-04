using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionQuestion : MonoBehaviour
{
    private int InteractionNum;
    private int InteractionOne;
    private int InteractionTwo;
    private int InteractionThree;
    public GameObject NoMoney;

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

    public void BlockButton(int index)
    {
        UpbringingGameManager.Instance.BlockInteraction[index].SetActive(true);
    } // 상호작용 막기

    public void OnClick_Button(bool Yes)
    {
        if (Yes && PlayerPrefs.GetInt("Interaction")<1)
        {
            AudioManager.Instance.PlaySFX("Interaction");
            if (InteractionNum == 0)
            {
                if (UpbringingGameManager.Instance.Gold >= 1000)
                {
                    UpbringingGameManager.Instance.ActionNum += InteractionManager.Instance.LowFlavor;
                    InteractionManager.Instance.ConservationText.text = InteractionManager.Instance.LowInteractionConservation;
                    UpbringingGameManager.Instance.Gold -= 1000;
                    PlayerPrefs.SetInt("Gold", UpbringingGameManager.Instance.Gold);
                    UpbringingGameManager.Instance.InteractionOneChance++;
                    PlayerPrefs.SetInt("InteractionOneChance", UpbringingGameManager.Instance.InteractionOneChance);
                    BlockButton(0);
                }
                else
                {
                    NoMoney.SetActive(true);
                }
            }
            else if (InteractionNum == 1)
            {
                if (UpbringingGameManager.Instance.Gold >= 2000)
                {
                    UpbringingGameManager.Instance.ActionNum += InteractionManager.Instance.MiddleFlavor;
                    InteractionManager.Instance.ConservationText.text = InteractionManager.Instance.MiddleInteractionConservation;
                    UpbringingGameManager.Instance.Gold -= 2000;
                    PlayerPrefs.SetInt("Gold", UpbringingGameManager.Instance.Gold);
                    UpbringingGameManager.Instance.InteractionTwoChance++;
                    PlayerPrefs.SetInt("InteractionTwoChance", UpbringingGameManager.Instance.InteractionTwoChance);
                    BlockButton(1);
                }
                else
                {
                    NoMoney.SetActive(true);
                }
            }
            else if (InteractionNum == 2)
            {
                if (UpbringingGameManager.Instance.Gold >= 3000)
                {
                    UpbringingGameManager.Instance.ActionNum += InteractionManager.Instance.HighFlavor;
                    InteractionManager.Instance.ConservationText.text = InteractionManager.Instance.HighInteractionConservation;
                    UpbringingGameManager.Instance.Gold -= 3000;
                    PlayerPrefs.SetInt("Gold", UpbringingGameManager.Instance.Gold);
                    UpbringingGameManager.Instance.InteractionThreeChance++;
                    PlayerPrefs.SetInt("InteractionThreeChance", UpbringingGameManager.Instance.InteractionThreeChance);
                    BlockButton(2);
                }
                else
                {
                    NoMoney.SetActive(true);
                }
            }
            UpbringingGameManager.Instance.ActionNumText.text = UpbringingGameManager.Instance.ActionNum.ToString();
            PlayerPrefs.SetInt("ActionNum", UpbringingGameManager.Instance.ActionNum);
        }
        else
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
        }
        gameObject.SetActive(false);
    }
}
