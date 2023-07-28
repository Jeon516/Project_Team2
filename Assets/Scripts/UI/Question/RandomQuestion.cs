using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomQuestion : MonoBehaviour
{
   public void OnClick_YesButton()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (UpbringingGameManager.Instance.ActionNum >= 50)
        {
            int ChangeNum = PlayerPrefs.GetInt("ActionNum") - 50;
            PlayerPrefs.SetInt("ActionNum", ChangeNum);

            int StatOrderNum = Random.Range(0, 5);
            int StatNum = Random.Range(0, 2);

            while (PlayerPrefs.GetInt(UpbringingGameManager.Instance.StatOrder[StatOrderNum]) + UpbringingGameManager.Instance.Cal[StatNum] > 4
                || PlayerPrefs.GetInt(UpbringingGameManager.Instance.StatOrder[StatOrderNum]) + UpbringingGameManager.Instance.Cal[StatNum] < -4)
            {
                StatOrderNum = Random.Range(0, 5);
                StatNum = Random.Range(0, 2);
            }

            PlayerPrefs.SetInt(UpbringingGameManager.Instance.StatOrder[StatOrderNum], PlayerPrefs.GetInt(UpbringingGameManager.Instance.StatOrder[StatOrderNum]) + UpbringingGameManager.Instance.Cal[StatNum]);
            PlayerPrefs.SetFloat(UpbringingGameManager.Instance.StatOrder[StatOrderNum] + "X", PlayerPrefs.GetFloat(UpbringingGameManager.Instance.StatOrder[StatOrderNum] + "X") - 111 * UpbringingGameManager.Instance.Cal[StatNum]);
        }
        gameObject.SetActive(false);
    }

    public void OnClick_NoButton()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        gameObject.SetActive(false);
    }
}
