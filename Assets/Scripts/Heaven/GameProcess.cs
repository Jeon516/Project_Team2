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

            if (setDataList != null && setDataList.Sets.Count > 0)
            {
                // Color 필드 랜덤 선택
                List<string> availableColors = new List<string>();
                foreach (SetData setData in setDataList.Sets)
                {
                    availableColors.Add(setData.Color);
                }

                if (availableColors.Count >= 3)
                {
                    int randomIndex1 = Random.Range(0, availableColors.Count);
                    string randomColor1 = availableColors[randomIndex1];
                    availableColors.RemoveAt(randomIndex1);

                    int randomIndex2 = Random.Range(0, availableColors.Count);
                    string randomColor2 = availableColors[randomIndex2];
                    availableColors.RemoveAt(randomIndex2);

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

        // 이미지 할당
        TrainLeft.sprite = trainLSprites[randomIndex1];
        TrainRight.sprite = trainRSprites[randomIndex2];
        SetRandomImage(Ticket, ticketSprites);
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
