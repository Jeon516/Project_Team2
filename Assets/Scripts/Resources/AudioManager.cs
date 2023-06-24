using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } = null;

    public AudioSource BGMSource;
    private float SavedVolume;
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
                BGMSource.volume = PlayerPrefs.GetFloat("BGM")/100.0f;
                BGMSource.Play();
            }
        }
        else { Debug.Log("akd"); }
    }
    private void Update()
    {
        BGMSource.volume = PlayerPrefs.GetFloat("BGM") / 100.0f;
    }
}
