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
        PlayerPrefs.SetInt("IsHeaven", 1);
    } // Data Initialize
}
