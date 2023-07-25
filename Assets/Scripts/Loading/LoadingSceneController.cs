using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    public static LoadingSceneController Instance { get; private set; } = null;
    //private static LoadingSceneController instance;
    //public static LoadingSceneController Instance => instance;

    private void Awake()
    {
        /*if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;*/
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(true);
    }
}
