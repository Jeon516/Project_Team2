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
        Instance = this; // �ν��Ͻ�ȭ
        DontDestroyOnLoad(gameObject);

        /*if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } // �� �ѱ� �� ���� �ձ�*/
    } 

    private void Update()
    {
        /*bgmSource.volume = PlayerPrefs.GetFloat("BGM",0.6f);
        for(int i=0;i<sfxSource.Length;++i)
        {
            sfxSource[i].volume= PlayerPrefs.GetFloat("SFX",0.6f);
        }*/
    } // BGM, SFX�� �ٷ� �ݿ��ϱ�

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
            Debug.LogError("BGM�� ������ �ʽ��ϴ�");
        }
    }

    public void StopBGM()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
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
