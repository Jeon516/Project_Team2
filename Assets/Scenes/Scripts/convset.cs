using UnityEngine;
using UnityEngine.UI;

public class convset : MonoBehaviour
{
    public Text uiText;
    public Image uiImage;
    public int maxNumImages = 10;

    private bool isDogText = true;

    public void OnButtonClick()
    {
        // 텍스트 변경
        SwitchText();

        // 이미지 할당
        LoadImagesForDayValue();
    }

    private void SwitchText()
    {
        isDogText = !isDogText;

        int dayValue = PlayerPrefs.GetInt("DayValue", 0) % 32;

        // 시작 텍스트를 초기화합니다.
        string initialText = "";

        // 특정 DayValue에 따라 시작 텍스트를 변경합니다.
        switch (dayValue)
        {
            case 1:
                initialText = "강아지";
                break;
            case 2:
                initialText = "강아지";
                break;
            case 3:
                initialText = "강아지";
                break;
            case 4:
                initialText = "강아지";
                break;
            case 5:
                initialText = "강아지";
                break;
            case 6:
                initialText = "강아지";
                break;
            case 7:
                initialText = "강아지";
                break;
            case 8:
                initialText = "강아지";
                break;
            case 9:
                initialText = "강아지";
                break;
            case 10:
                initialText = "강아지";
                break;
            case 11:
                initialText = "강아지";
                break;
            case 12:
                initialText = "강아지";
                break;
            case 13:
                initialText = "유저";
                break;
            case 14:
                initialText = "강아지";
                break;
            case 15:
                initialText = "강아지";
                break;
            case 16:
                initialText = "강아지";
                break;
            case 17:
                initialText = "강아지";
                break;
            case 18:
                initialText = "강아지";
                break;
            case 19:
                initialText = "강아지";
                break;
            case 20:
                initialText = "강아지";
                break;
            case 21:
                initialText = "강아지";
                break;
            case 22:
                initialText = "유저";
                break;
            case 23:
                initialText = "강아지";
                break;
            case 24:
                initialText = "강아지";
                break;
            case 25:
                initialText = "강아지";
                break;
            case 26:
                initialText = "강아지";
                break;
            case 27:
                initialText = "강아지";
                break;
            case 28:
                initialText = "강아지";
                break;
            case 29:
                initialText = "강아지";
                break;
            case 30:
                initialText = "강아지";
                break;
            case 31:
                initialText = "강아지";
                break;
            case 0:
                initialText = "강아지";
                break;
            default:
                initialText = "강아지";
                break;
        }
        uiText.text = isDogText ? initialText : "유저";
    }

    private void LoadImagesForDayValue()
    {
        int dayValue = PlayerPrefs.GetInt("DayValue", 0);

        int remainder = dayValue % 32;

        string imagePath = "Image/10DayPrefeb/Day" + remainder;

        Sprite imageSprite = Resources.Load<Sprite>(imagePath);

        if (imageSprite != null)
        {
            uiImage.sprite = imageSprite;
        }
        else
        {
            Debug.LogError("Image not found at path: " + imagePath);
        }
    }
}
