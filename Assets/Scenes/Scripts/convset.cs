using UnityEngine;
using UnityEngine.UI;

public class convset : MonoBehaviour
{
    public RectTransform ChatTransform;
    public RectTransform NameTransform;
    public Text uiText;
    public Image uiImage;
    private Sprite[] imageSprites;

    private string PlayerName;
    private bool isDogText = true;
    private int TotalOrder = 10;
    private int CurrentOrder = 0;
    private int DayValue = 0;

    private void Awake()
    {
        ModifyRectTransform(0, -180, 1800, 1000);
        ModifyTextRectTransform(-597, -30, 500, 200);
    }
    private void Start()
    {
        PlayerName = PlayerPrefs.GetString("Player", "플레이어");
        DayValue = PlayerPrefs.GetInt("Day", 0);
        DayValue /= 20;

        if (DayValue >= 32)
            DayValue = 1;

        TotalOrder = 20;
        CurrentOrder = 0;
        LoadImagesForDayValue();
        SwitchText();
    }

    public void OnButtonClick()
    {
        SwitchText();
        if(CurrentOrder>=TotalOrder)
        {
            gameObject.SetActive(false);
        }
        else
        {
            uiImage.sprite = imageSprites[CurrentOrder];
            CurrentOrder++;
        }
    }

    private void SwitchText()
    {
        isDogText = !isDogText;

        // 시작 텍스트를 초기화합니다.
        string initialText = "";

        // 특정 DayValue에 따라 시작 텍스트를 변경합니다.
        switch (DayValue)
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
                initialText = PlayerName;
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
                initialText = PlayerName;
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
        uiText.text = isDogText ? initialText : PlayerName;
    }

    private void LoadImagesForDayValue()
    {
        string imagePath = "Image/10DayPrefeb/Day" + DayValue;

        imageSprites = Resources.LoadAll<Sprite>(imagePath);

        if (imageSprites != null)
        {
            TotalOrder=imageSprites.Length;
            uiImage.sprite = imageSprites[0];
            CurrentOrder++;
        }
        else
        {
            Debug.LogError("Image not found at path: " + imagePath);
        }
    }

    private void ModifyRectTransform(int x, int y, int width, int height)
    {
        // Width, Height, Pos X, Pos Y 변경
        ChatTransform.sizeDelta = new Vector2(width, height);
        ChatTransform.anchoredPosition = new Vector2(x, y);
    }

    private void ModifyTextRectTransform(int x, int y, int width, int height)
    {
        // Width, Height, Pos X, Pos Y 변경
        NameTransform.sizeDelta = new Vector2(width, height);
        NameTransform.anchoredPosition = new Vector2(x, y);
    }
}
