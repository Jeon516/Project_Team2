using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } = null;

    public AudioSource bgmSource;
    public AudioSource[] sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    } 

    public void PlayBGM(string name)
    {
        AudioClip bgmClip = Resources.Load<AudioClip>("Music/BGM/" + name);
        if (bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.volume = PlayerPrefs.GetFloat("BGM");
            bgmSource.Play();
        }
        else
        {
            Debug.LogError("BGM이 나오지 않습니다");
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySFX(string name)
    {
        AudioClip sfxClip = Resources.Load<AudioClip>("Music/SFX/" + name);

        if (sfxClip != null)
        {
            for (int i = 0; i < sfxSource.Length; ++i)
            {
                if (sfxSource[i].isPlaying == false)
                {
                    sfxSource[i].clip = sfxClip;
                    sfxSource[i].volume = PlayerPrefs.GetFloat("SFX");
                    sfxSource[i].spatialBlend = 0;
                    sfxSource[i].Play();
                    return;
                }
            }
        }
    }

    public void StopSFX()
    {
        for (int i = 0; i < sfxSource.Length; ++i)
        {
            if (sfxSource[i].isPlaying)
            {
                sfxSource[i].Stop();
            }
        }
    }
}
