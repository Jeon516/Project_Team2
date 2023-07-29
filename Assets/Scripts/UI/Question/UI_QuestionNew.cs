using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_QuestionNew : MonoBehaviour
{
    public void OnClick_NextNew()
    {
        Restart();
        SceneManager.LoadScene("Heaven");
    } // New Start
    public void OnClick_Cancel()
    {
        gameObject.SetActive(false);
    } // 팝업창 없애기
    private void Restart()
    {
        PlayerPrefs.SetInt("Day", 0);
        PlayerPrefs.SetInt("ActionNum", 1000);
        PlayerPrefs.SetInt("Gold", 1000);
        PlayerPrefs.SetInt("Interaction", 0);
        PlayerPrefs.SetInt("IsRandomFree", 0);
        PlayerPrefs.SetInt("IsHeaven", 1);

        PlayerPrefs.SetInt("Energy", 0);
        PlayerPrefs.SetFloat("EnergyX", 0);

        PlayerPrefs.SetInt("Sociality", 0);
        PlayerPrefs.SetFloat("SocialityX", 0);

        PlayerPrefs.SetInt("Deliberation", 0);
        PlayerPrefs.SetFloat("DeliberationX", 0);

        PlayerPrefs.SetInt("Curiosoty", 0);
        PlayerPrefs.SetFloat("CuriosotyX", 0);

        PlayerPrefs.SetInt("Love", 0);
        PlayerPrefs.SetFloat("LoveX", 0);
    } // Data Initialize
}
