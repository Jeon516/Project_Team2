using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InformationSetting : MonoBehaviour
{
    public GameObject DogTag;
    public GameObject FoodLow;
    public GameObject FoodMiddle;
    public GameObject FoodHigh;
    public GameObject LevelButton;
    private bool IsDog = false;
    private bool IsFood = false;

    private void Start()
    {
        DogTag.SetActive(false);
        LevelButton.SetActive(false);
        FoodLow.SetActive(false);
        FoodMiddle.SetActive(false);
        FoodHigh.SetActive(false);
    }
    public void OnClick_Close()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        gameObject.SetActive(false);
    }

    public void OnClick_DogTag()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (!IsDog)
        {
            DogTag.SetActive(true);
            FoodLow.SetActive(false);
            FoodMiddle.SetActive(false);
            FoodHigh.SetActive(false);
            LevelButton.SetActive(false);
            IsDog = true;
            IsFood = false;
        }
        else
        {
            DogTag.SetActive(false);
            IsDog = false;
        }
    }

    public void OnClick_FoodTag()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (!IsFood)
        {
            LevelButton.SetActive(true);
            IsFood = true;
            IsDog = false;
        }
        else
        {
            LevelButton.SetActive(false);
            IsFood = false;
        }
    }

    public void OnClick_FoodLow()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        FoodLow.SetActive(true);
        FoodMiddle.SetActive(false);
        FoodHigh.SetActive(false);
        LevelButton.SetActive(false);
        DogTag.SetActive(false);
        IsFood = false;
    }

    public void OnClick_FoodMiddle()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        FoodMiddle.SetActive(true);
        FoodLow.SetActive(false);
        FoodHigh.SetActive(false);
        LevelButton.SetActive(false);
        DogTag.SetActive(false);
        IsFood = false;
    }

    public void OnClick_FoodHigh()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        FoodHigh.SetActive(true);
        FoodLow.SetActive(false);
        FoodMiddle.SetActive(false);
        LevelButton.SetActive(false);
        DogTag.SetActive(false);
        IsFood = false;
    }
}
