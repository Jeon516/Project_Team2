using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    public int score = 0;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            UIManager_World.Instance.SETUI_Score(score);
        }
    }
    private int stage = 0;
    public int Stage
    {
        get
        {
            return stage;
        }
        set
        {
            stage = value;
            UIManager_World.Instance.SETUI_Stage(stage);
        }
    }

    private bool gameState = true;
    public bool GameState
    {
        get
        {
            return gameState;
        }
        set
        {
            gameState = value;
            Time.timeScale = gameState ? 1 : 0;
            UIManager_World.Instance.SETUI_Pause(!gameState);
        }
    }

    private bool isGameOver = false;
    public bool IsGameOver
    {
        get 
        {
            return isGameOver;
        }
        set
        {
            isGameOver = value;
            if(isGameOver)
            {
                StopAllCoroutines();
                PlayerPrefs.SetInt("Score", 0);
                PlayerPrefs.SetInt("Stage", 1);
                AudioManager.Instance.StopBGM();
                UIManager_World.Instance.SETUI_GameOver(true);
            }
        }
    }

    public Action onSpawn;
    public GameObject[] spawnGroup;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Score = PlayerPrefs.GetInt("Score", 0);
        Stage = PlayerPrefs.GetInt("Stage", 1);
        GameState = true;
        AudioManager.Instance.PlayBGM("worldBGM");
        StartCoroutine(Routine());
    }
    public void SaveStatus()
    {
        PlayerPrefs.SetInt("Score", Score);
        PlayerPrefs.SetInt("Stage", Stage);
    }
    IEnumerator Routine()
    {
        PlayerHP player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();

        while (true)
        {
            int spawnCount = Stage;

            player.HP = player.maxHP;

            if(Stage==1)
            {
                spawnGroup[0].SetActive(true);
            }

            yield return new WaitForSeconds(5f);
            for(int i=0;i<spawnCount;++i)
            {
                Debug.Log("REa");
                onSpawn?.Invoke();
                yield return new WaitForSeconds(2f);
            }
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length <= 0);
            AudioManager.Instance.PlaySFX("levelcomplete");
            Score += 1000 * Stage;
            yield return new WaitForSeconds(10f);
            Stage++;
        }
    }
}
