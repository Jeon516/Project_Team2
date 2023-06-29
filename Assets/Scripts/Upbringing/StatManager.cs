using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public int Energy;
    public int Sociality;
    public int Deliberation;
    public int Curiosoty;
    public int Love;

    public static StatManager Instance { get; private set; } = null;
    private void Awake()
    {
        Energy = PlayerPrefs.GetInt("Energy", 0);
        PlayerPrefs.SetInt("Energy", Energy);

        Sociality = PlayerPrefs.GetInt("Sociality", 0);
        PlayerPrefs.SetInt("Sociality", Sociality);

        Deliberation = PlayerPrefs.GetInt("Deliberation", 0);
        PlayerPrefs.SetInt("Deliberation", Deliberation);

        Curiosoty = PlayerPrefs.GetInt("Curiosoty", 0);
        PlayerPrefs.SetInt("Curiosoty", Curiosoty);

        Love = PlayerPrefs.GetInt("Love", 0);
        PlayerPrefs.SetInt("Love", Love);
        Instance = this;
    }
}
