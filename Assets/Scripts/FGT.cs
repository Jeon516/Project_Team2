using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FGT : MonoBehaviour
{
    private int Day;
    public GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        Day = PlayerPrefs.GetInt("Day");
        PlayerPrefs.SetInt("Day", Day);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Restart();
            Canvas.SetActive(false);
            SceneManager.LoadSceneAsync("Intro_Width");
        }
    }

    private void Restart()
    {

        PlayerPrefs.SetInt("Day", 0);
        PlayerPrefs.SetInt("ActionNum", 1000);
        PlayerPrefs.SetInt("Gold", 100000);
        PlayerPrefs.SetInt("IsHeaven", 1);
        PlayerPrefs.SetInt("Interaction", 0);

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
        SceneManager.LoadScene("Loading");
    }
}
