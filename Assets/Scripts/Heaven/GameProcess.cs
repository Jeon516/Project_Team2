using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class GameProcess : MonoBehaviour
{
    public Button[] configurableButtons;
    public Image TrainLeft;
    public Image TrainRight;
    public Image Ticket;
    private int correct = 0;

    private int dayValue;
    private int TL;
    private int TR;
    private int R;
    public int SN;
    public int AN;
    public int SE;
    public int AE;
    public string emblemcolor;

    private List<Sprite> trainLSprites = new List<Sprite>();
    private List<Sprite> trainRSprites = new List<Sprite>();
    private List<Sprite> ticketSprites = new List<Sprite>();
    private List<string> availableColors = new List<string>();
    public delegate void ButtonClickedDelegate();
    public static event ButtonClickedDelegate OnButtonClicked;
    private List<SetData> setDataList = new List<SetData>(); // Move setDataList to class level
    private TextLoader textLoader;
    private EmblemLoader emblemLoader;

    private void Start()
    {
        dayValue = HeavenGameManager.Instance.Day;
        Debug.Log("Day: " + dayValue);
        textLoader = GetComponent<TextLoader>();
        emblemLoader = GetComponent<EmblemLoader>();

        TL = 40;
        TR = 40;
        R = 20;

        if (dayValue >= 1 && dayValue <= 5)
        {
            TL = 40;
            TR = 40;
            R = 20;
            SN = 80;
            AN = 20;
            SE = 80;
            AE = 20;
        }
        else if (dayValue >= 6 && dayValue <= 15)
        {
            TL--;
            TR--;
            R += 2;
            SN -= 2;
            AN += 2;
            SE -= 2;
            AE += 2;
        }
        else if (dayValue >= 16 && dayValue <= 20)
        {
            TL = 30;
            TR = 30;
            R = 40;
            SN = 60;
            AN = 40;
            SE = 60;
            AE = 40;
        }

        string JSONFilePath = "JsonFiles/Game/Set";
        string JSONFullPath = Path.Combine("Assets/Resources", JSONFilePath);
        JSONFullPath += ".json";

        if (File.Exists(JSONFullPath))
        {
            string json = File.ReadAllText(JSONFullPath);
            SetDataList setDataList = JsonUtility.FromJson<SetDataList>(json);
            

            if (setDataList != null && setDataList.Sets.Count > 0)
            {
                string randomColor3 = GetComponent<GameProcess>().GetRandomColor3();

            List<SetData> matchingSets = setDataList.Sets.FindAll(setData =>
                setData.Color == randomColor3 &&
                setData.emblemAssets == EmblemLoader.Emblempng &&
                setData.Sort == TextLoader.textValue
            );

            if (matchingSets.Count > 0)
            {
                // At least one matching set found, correct answer
                correct++;
                Debug.Log("Correct! Correct count: " + correct);
            }
            else
            {
                // No matching set found, wrong answer
                Debug.Log("Wrong! Correct count: " + correct);
            }
                foreach (SetData setData in setDataList.Sets)
                {
                    availableColors.Add(setData.Color);
                }
            }
            else
            {
                Debug.LogError("셋에 데이터가 없습니다.");
            }
        }
        else
        {
            Debug.LogError("파일을 찾을 수 없습니다: " + JSONFullPath);
        }

        foreach (Button button in configurableButtons)
        {
            button.onClick.AddListener(() => OnButtonClick());
        }

        SetRandomImages();
    }
    

    private void LoadImagesIntoList(string folderPath, List<Sprite> spriteList)
    {
        Sprite[] loadedSprites = Resources.LoadAll<Sprite>(folderPath);
        spriteList.Clear();
        spriteList.AddRange(loadedSprites);
    }
    private Sprite GetTrainSprite(string color, bool isLeftSide)
    {
        string folderPath = isLeftSide ? "Image/Heaven/Train(Size)/LeftSide/" : "Image/Heaven/Train(Size)/RightSide/";
        string imagePath = folderPath + color;

        Sprite sprite = Resources.Load<Sprite>(imagePath);
        if (sprite == null)
        {
            Debug.LogError("해당 컬러에 대한 기차 이미지를 찾을 수 없습니다: " + color);
        }

        return sprite;
    }

public void SetRandomImages()
{
    LoadImagesIntoList("Image/Heaven/Train(Size)/LeftSide", trainLSprites);
    LoadImagesIntoList("Image/Heaven/Train(Size)/RightSide", trainRSprites);
    LoadImagesIntoList("Image/Heaven/tickets", ticketSprites);

    int randomIndex1 = Random.Range(0, trainLSprites.Count);
    int randomIndex2 = Random.Range(0, trainRSprites.Count);

    while (randomIndex1 == randomIndex2)
    {
        randomIndex2 = Random.Range(0, trainRSprites.Count);
    }

    string randomColor1 = availableColors[randomIndex1];
    string randomColor2 = availableColors[randomIndex2];
    Debug.Log("Random Color 1: " + randomColor1);
    Debug.Log("Random Color 2: " + randomColor2);

    int randomValue = Random.Range(1, TL + TR + R + 1);
    string randomColor3;
    if (randomValue <= TL)
    {
        randomColor3 = randomColor1;
    }
    else if (randomValue <= TL + TR)
    {
        randomColor3 = randomColor2;
    }
    else
    {
        List<string> remainingColors = availableColors.FindAll(color => color != randomColor1 && color != randomColor2);
        int remainingIndex = Random.Range(0, remainingColors.Count);
        randomColor3 = remainingColors[remainingIndex];
    }
    emblemcolor = randomColor3;

    Debug.Log("Random Color 3: " + randomColor3);

    TrainLeft.sprite = GetTrainSprite(randomColor1, true);
    TrainRight.sprite = GetTrainSprite(randomColor2, false);
    Ticket.sprite = GetTicketSprite(randomColor3);
    List<SetData> matchingSets = setDataList.FindAll(setData => setData.Color == randomColor3 && setData.emblemAssets == EmblemLoader.Emblempng && setData.Sort == TextLoader.textValue);
        {
            correct++;
            Debug.Log("Correct! Correct count: " + correct);
        }

}
public string GetRandomColor3()
{
    return emblemcolor;
}

private Sprite GetTicketSprite(string color)
{
    foreach (Sprite ticketSprite in ticketSprites)
    {
        if (ticketSprite.name.Contains(color))
        {
            return ticketSprite;
        }
    }

    Debug.LogError("해당 컬러에 대한 티켓 이미지를 찾을 수 없습니다: " + color);
    return null;
}

private string GetRandomColor3FromGameProcess()
{
    GameProcess gameProcessScript = GetComponent<GameProcess>();

    return gameProcessScript.GetRandomColor3();
}

    private void OnButtonClick()
    {
        SetRandomImages();
        if (OnButtonClicked != null)
        {
            OnButtonClicked();
        }
    }
}


[System.Serializable]
public class SetDataList
{
    public List<SetData> Sets;
}

[System.Serializable]
public class SetData
{
    public string Color;
    public string emblemAssets;
    public string Sort;
}
