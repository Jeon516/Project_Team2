using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Pause : MonoBehaviour
{
    public void OnClick_Resume()
    {
        GameManager.Instance.GameState = true;
    }
    public void OnClick_Setting()
    {
        UIManager_World.Instance.SETUI_Setting(true);
    }
    public void OnClick_Main()
    {
        GameManager.Instance.SaveStatus();
        SceneManager.LoadScene("Main");
    }
   
}
