using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InformationSetting : MonoBehaviour
{
    public GameObject DogTag;
    public GameObject FoodLow;
    public GameObject FoodMiddle;
    public GameObject FoodHigh;
    public GameObject LevelButton;
    public Text NameText;
    public Text InfoText;
    private bool IsDog = false;
    private bool IsFood = false;

    private void Start()
    {
        NameText.text = "";
        InfoText.text = "";
        DogTag.SetActive(false);
        LevelButton.SetActive(false);
        FoodLow.SetActive(false);
        FoodMiddle.SetActive(false);
        FoodHigh.SetActive(false);
    }
    public void OnClick_Close()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        FoodMiddle.SetActive(false);
        FoodLow.SetActive(false);
        FoodHigh.SetActive(false);
        LevelButton.SetActive(false);
        DogTag.SetActive(false);
        gameObject.SetActive(false);
        UI_InventoryInformation.Instance.SwitchImageNull();
    }

    public void OnClick_DogTag()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (!IsDog)
        {
            NameText.text = "";
            InfoText.text = "";
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
        NameText.text = "";
        InfoText.text = "";
        Orderfood.Instance.Selected[Orderfood.Instance.selectorder].SetActive(false);
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
        NameText.text = "";
        InfoText.text = "";
        Orderfood.Instance.Selected[Orderfood.Instance.selectorder].SetActive(false);
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
        NameText.text = "";
        InfoText.text = "";
        Orderfood.Instance.Selected[Orderfood.Instance.selectorder].SetActive(false);
        FoodHigh.SetActive(true);
        FoodLow.SetActive(false);
        FoodMiddle.SetActive(false);
        LevelButton.SetActive(false);
        DogTag.SetActive(false);
        IsFood = false;
    }
}
