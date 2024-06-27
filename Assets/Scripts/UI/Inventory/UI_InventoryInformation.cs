using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventoryInformation : MonoBehaviour
{
    public Image FoodImage;
    public Text FoodText;
    public Text FoodNameText;

    public static UI_InventoryInformation Instance { get; private set; } = null;

    private void Awake()
    {
        Instance = this;
    }

    public void SwitchImage(Image SelectedImage)
    {
        FoodImage.GetComponent<Image>().sprite = SelectedImage.sprite;
    } // �̹��� �ٲٱ�

    public void SwitchImageNull()
    {
        FoodImage.GetComponent<Image>().sprite = null;
        FoodText.text = "";
        if(FoodNameText)
        {
            FoodNameText.text = "";
        }
    } // �̹��� ���ֱ� �� �ؽ�Ʈ ���ֱ�

}