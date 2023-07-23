using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_QuestionNew : MonoBehaviour
{
    public void OnClick_NextNew()
    {
        PlayerPrefs.SetInt("ActionNum", 0);
        PlayerPrefs.SetInt("Gold", 0);
        PlayerPrefs.SetInt("Day", 0);
        PlayerPrefs.SetInt("IsHeaven", 1);
        SceneManager.LoadScene("Heaven");
    } // New Start
    public void OnClick_Cancel()
    {
        gameObject.SetActive(false);
    } // 팝업창 없애기
}
