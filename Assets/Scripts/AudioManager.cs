using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}= null;

    public AudioSource bgmSource;
    public AudioSource[] sfxSource;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayBGM(string name)
    {
        AudioClip bgmClip = Resources.Load<AudioClip>("BGM/" + name);
        if (bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.volume = PlayerPrefs.GetFloat("BGM");
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        if(bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }
    public void PlaySFX(string name)
    {
        AudioClip sfxClip = Resources.Load<AudioClip>("SFX/" + name);

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
    public void PlaySFX(string name, Vector2 position)
    {
        AudioClip sfxClip = Resources.Load< AudioClip > ("SFX/" + name);

        if(sfxClip!=null)
        {
            for(int i=0;i<sfxSource.Length;++i)
            {
                if(sfxSource[i].isPlaying==false)
                {
                    sfxSource[i].clip = sfxClip;
                    sfxSource[i].volume = PlayerPrefs.GetFloat("SFX");
                    sfxSource[i].spatialBlend = 1;
                    sfxSource[i].transform.position = position;
                    sfxSource[i].Play();
                    return;
                }
            }
        }
    }
}
