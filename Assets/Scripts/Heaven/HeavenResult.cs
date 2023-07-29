using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavenResult : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlaySFX("GameOver");   
    }
}
