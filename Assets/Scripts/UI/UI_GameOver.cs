using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour
{
    public void OnClick_Retry()
    {
        SceneManager.LoadScene("World");
    }
    public void OnClick_Main()
    {
        SceneManager.LoadScene("Main");
    }
}
