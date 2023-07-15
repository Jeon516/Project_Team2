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

                if (dayValue >= 1 && dayValue <= 5)
                {
                    TL = 40;
                    TR = 40;
                    R = 20;
                }
                else if (dayValue >= 6 && dayValue <= 15)
                {
                    TL--;
                    TR--;
                    R += 2;
                }
                else if (dayValue >= 16 && dayValue <= 20)
                {
                    TL = 30;
                    TR = 30;
                    R = 40;
                }

                if (availableColors.Count >= 3)
                {
                    // Fisher-Yates 알고리즘을 사용하여 availableColors 리스트를 섞습니다.
                    int n = availableColors.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = Random.Range(0, n + 1);
                        string value = availableColors[k];
                        availableColors[k] = availableColors[n];
                        availableColors[n] = value;
                    }

                    // 첫 3개의 색상을 가져옵니다.
                    string randomColor1 = availableColors[0];
                    string randomColor2 = availableColors[1];
                    string randomColor3 = availableColors[2];

                    // 랜덤 컬러를 파일 경로에 할당
                    string TrainLPath = "Image/Heaven/Train(Size)/LeftSide/" + randomColor1;
                    string TrainRPath = "Image/Heaven/Train(Size)/RightSide/" + randomColor2;
                    string TicketPath = "Image/Heaven/tickets/" + randomColor3;

                    // 이미지를 리스트로 로드
                    LoadImagesIntoList(TrainLPath, trainLSprites);
                    LoadImagesIntoList(TrainRPath, trainRSprites);
                    LoadImagesIntoList(TicketPath, ticketSprites);

                    // 초기 이미지 할당
                    SetRandomImage(TrainLeft, trainLSprites);
                    SetRandomImage(TrainRight, trainRSprites);
                    SetRandomImage(Ticket, ticketSprites);

                    // 로그 출력
                    Debug.Log("Random Color 1: " + randomColor1);
                    Debug.Log("Random Color 2: " + randomColor2);
                    Debug.Log("Random Color 3: " + randomColor3);
                }
                else
                {
                    Debug.LogError("사용 가능한 색상이 충분하지 않습니다.");
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
    }

    private void LoadImagesIntoList(string folderPath, List<Sprite> spriteList)
    {
        Sprite[] loadedSprites = Resources.LoadAll<Sprite>(folderPath);
        spriteList.Clear();
        spriteList.AddRange(loadedSprites);
    }

    private void SetRandomImage(Image image, List<Sprite> spriteList)
    {
        if (spriteList.Count > 0)
        {
            int randomIndex = Random.Range(0, spriteList.Count);
            image.sprite = spriteList[randomIndex];
        }
    }

    private void OnButtonClick()
    {
        // 이미지를 다시 로드하고 새로운 이미지 할당
        LoadImagesIntoList("Image/Heaven/Train(Size)/LeftSide", trainLSprites);
        LoadImagesIntoList("Image/Heaven/Train(Size)/RightSide", trainRSprites);
        LoadImagesIntoList("Image/Heaven/tickets", ticketSprites);

        // 랜덤 컬러를 선택
        int randomIndex1 = Random.Range(0, trainLSprites.Count);
        int randomIndex2 = Random.Range(0, trainRSprites.Count);

        // randomIndex1과 randomIndex2가 서로 다른 값이 될 때까지 반복합니다.
        while (randomIndex1 == randomIndex2)
        {
            randomIndex2 = Random.Range(0, trainRSprites.Count);
        }

        // 이미지 할당
        TrainLeft.sprite = trainLSprites[randomIndex1];
        TrainRight.sprite = trainRSprites[randomIndex2];

        // 로그 출력
        string randomColor1 = availableColors[randomIndex1];
        string randomColor2 = availableColors[randomIndex2];
        Debug.Log("Random Color 1: " + randomColor1);
        Debug.Log("Random Color 2: " + randomColor2);

        // randomColor3 선택
        string randomColor3;
        int randomValue = Random.Range(1, 101);
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
        Debug.Log("Random Color 3: " + randomColor3);

    Ticket.sprite = GetTicketSprite(randomColor3);
    }

    private Sprite GetTicketSprite(string color)
    {
        // color에 해당하는 Ticket 이미지를 리소스에서 찾아 반환합니다.
        string path = "Image/Heaven/tickets/" + color;
        Sprite ticketSprite = Resources.Load<Sprite>(path);
        return ticketSprite;
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
