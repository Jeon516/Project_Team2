using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class GameProcess : MonoBehaviour
{
    public Button[] configurableButtons; // 버튼 배열

    public Image TrainLeft; // TrainLeft 이미지 UI 요소
    public Image TrainRight; // TrainRight 이미지 UI 요소
    public Image Ticket; // Ticket 이미지 UI 요소

    private int dayValue;
    private int TL;
    private int TR;
    private int R;

    private List<Sprite> trainLSprites = new List<Sprite>();
    private List<Sprite> trainRSprites = new List<Sprite>();
    private List<Sprite> ticketSprites = new List<Sprite>();

    private List<string> availableColors = new List<string>();

    private void Start()
    {
        dayValue = HeavenGameManager.Instance.Day;
        Debug.Log("Day: " + dayValue);

        TL = 40;
        TR = 40;
        R = 20;

        string JSONFilePath = "JsonFiles/Game/Set";
        string JSONFullPath = Path.Combine("Assets/Resources", JSONFilePath);
        JSONFullPath += ".json";

        if (File.Exists(JSONFullPath))
        {
            string json = File.ReadAllText(JSONFullPath);
            SetDataList setDataList = JsonUtility.FromJson<SetDataList>(json);

            if (setDataList != null && setDataList.Sets.Count > 0)
            {
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

        // 버튼에 클릭 이벤트 리스너 추가
        foreach (Button button in configurableButtons)
        {
            button.onClick.AddListener(() => OnButtonClick());
        }

        // 초기 이미지 할당
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


private void SetRandomImages()
{
    // 이미지를 다시 로드하고 새로운 이미지 할당
    LoadImagesIntoList("Image/Heaven/Train(Size)/LeftSide", trainLSprites);
    LoadImagesIntoList("Image/Heaven/Train(Size)/RightSide", trainRSprites);
    LoadImagesIntoList("Image/Heaven/tickets", ticketSprites);

    // 랜덤 컬러를 선택
    int randomIndex1 = Random.Range(0, trainLSprites.Count);
    int randomIndex2 = Random.Range(0, trainRSprites.Count);

    // 중복되지 않는 이미지가 선택될 때까지 반복
    while (randomIndex1 == randomIndex2)
    {
        randomIndex2 = Random.Range(0, trainRSprites.Count);
    }

    // 로그 출력
    string randomColor1 = availableColors[randomIndex1];
    string randomColor2 = availableColors[randomIndex2];
    Debug.Log("Random Color 1: " + randomColor1);
    Debug.Log("Random Color 2: " + randomColor2);

    // randomColor3 선택
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

    // 로그 출력
    Debug.Log("Random Color 3: " + randomColor3);

    // 이미지 할당
    TrainLeft.sprite = GetTrainSprite(randomColor1, true);
    TrainRight.sprite = GetTrainSprite(randomColor2, false);
    Ticket.sprite = GetTicketSprite(randomColor3);

    // 기타 남은 로직 처리...
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


    private void OnButtonClick()
    {
        // 새로운 이미지 할당
        SetRandomImages();
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
