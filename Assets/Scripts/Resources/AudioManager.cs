using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } = null;

    public AudioSource BGMSource;

    private void Start()
    {
        playBGM("IntroMusic");
    }
    private void Awake()
    {
        Instance = this;
    }
    public void playBGM(string name)
    {
        AudioClip BGMClip = Resources.Load<AudioClip>("Music/" + name);
        if(BGMClip !=null)
        {
            if (BGMClip != null)
            {
                BGMSource.clip = BGMClip;
                BGMSource.Play();
                BGMSource.volume = 0.4f;
            }
        }
    }
    private void Update()
    {
        BGMSource.volume = PlayerPrefs.GetFloat("BGM") / 100.0f;
    }
}
