using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class CollectedDog : MonoBehaviour
{
    public GameObject ShowScreen;
    public GameObject[] Block;
    public Image[] DogImage;
    public Image ShowDogImage;
    public Text ShowDogName;
    public Text ShowDogInformation;

    private int CollectEnergy; 
    private int CollectSociality;
    private int CollectDeliberation;
    private int CollectCuriosoty;
    private int CollectLove;
    //Collect Stat

    private DogStat collectDogDataList;
    private int selectedDogIndex;
    private CollectedDogData CollectedDogDatas;
    private string SelectedDogName;
    private string SelectedDogInformation;
    private string SelectedDogImage;
    // 스탯 계산 후 강아지

    public GameObject[] Selected;
    public Image[] SelectedImage;
    public Text SelectedText;
    public Text SelectedNameText;
    //도감

    public GameObject ChatInformation;
    public GameObject BlackScreen;
    public GameObject DogInformation;
    public GameObject GhostImage;
    public GameObject JayImage;
    private Color InformationChatColor;
    private Color JayNameColor;
    private Color JayChatColor;
    public Text ChatText;
    public Text CharacterText;
    private int order = 0;
    //20일차 이후의 이벤트

    public static CollectedDog Instance { get; private set; } = null;

    public void LastEvent()
    {
        BlackScreen.SetActive(true);
        ChatText.text = "(유령을 돌본지 벌써 20일이 되었다.\n이제 떠나보낼 준비를 해야 할 것 같다.)";
        ChatText.color = InformationChatColor;
        order++;
    }
    private void Awake()
    {
        CollectEnergy = PlayerPrefs.GetInt("Energy");
        CollectSociality = PlayerPrefs.GetInt("Sociality");
        CollectDeliberation = PlayerPrefs.GetInt("Deliberation");
        CollectCuriosoty = PlayerPrefs.GetInt("Curiosoty");
        CollectLove = PlayerPrefs.GetInt("Love");
        GhostImage.SetActive(false);
        JayImage.SetActive(false);

        JayNameColor = new Color32(84, 84, 84, 255);
        JayChatColor = new Color32(123, 123, 123, 255);
        InformationChatColor = new Color32(50, 50, 50, 255);

        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ShowScreen.SetActive(false);
        CollectedDogDatas = new CollectedDogData();
        LoadDogStat();
        LoadDogCollect();
        UnlockDogInformation();
    }

    private void Update()
    {
        Chat();
    }

    private void Chat()
    {
        if (Input.GetMouseButtonDown(0) && order == 1)
        {
            bool Check = false;
            ShowScreen.SetActive(true);
            CalculateProbability();
            for (int i = 0; i < CollectedDogDatas.collectedDogData.Count; i++)
            {
                if (CollectedDogDatas.collectedDogData[i].DogName == collectDogDataList.dogs[selectedDogIndex].Name)
                {
                    ChatText.text = "(눈을 떠보니 전에도 본 듯한 강아지가 나를 지켜보고 있다.)";
                    Check = true;
                    break;
                }
            }

            if (!Check)
            {
                ChatText.text = "(눈을 떠보니 처음 보는 강아지가 나를 지켜보고 있다.)";
            }
            order++;
        }
        else if (Input.GetMouseButtonDown(0) && order == 2)
        {
            DogInformation.SetActive(true);
            ChatInformation.SetActive(false);
            OnClick_CollectDog();
            order++;
        }
        else if (Input.GetMouseButtonDown(0) && order == 3)
        {
            DogInformation.SetActive(false);
            ChatInformation.SetActive(true);
            ChatText.text = "(관리소의 제이에게 데려가 보자.)";
            order++;
            //StartCoroutine(LoadingScene());
        }
        else if(Input.GetMouseButtonDown(0) && order == 4)
        {
            CharacterText.text = "제이";
            CharacterText.color = JayNameColor;
            ChatText.text = "음? 그 강아지는…\n 아하! 그 유령이 기억을 찾았나 보군요 ?";
            ChatText.color = JayChatColor;
            JayImage.SetActive(true);
            order++;
        } 
        else if(Input.GetMouseButtonDown(0) && order == 5)
        {
            order++;
            ChatText.text = "돌보느라 수고하셨습니다!\n업무를 하시는 동안 이 강아지가 열차에 탈 수 있도록\n준비하겠습니다.";
            if(UpbringingGameManager.Instance.Day/20!=1)
            {
                StartCoroutine(LoadingScene());
            }
        }
        else if (Input.GetMouseButtonDown(0) && order == 7)
        {
            order++;
            CharacterText.text = "";
            ChatText.text = "(관리소를 나오니 제이가 작은 유령과 함께 나를 기다리고 있다.)";
            ChatText.color = InformationChatColor;
            GhostImage.SetActive(true);
        }
        else if (Input.GetMouseButtonDown(0) && order == 8)
        {
            order++;
            CharacterText.text = "제이";
            ChatText.text = "하하…눈치채셨나요? 이번에 돌봐주셔야 할 유령입니다.";
            ChatText.color = JayChatColor;
        }
        else if (Input.GetMouseButtonDown(0) && order == 9)
        {
            order++;
            ChatText.text = "유령이 기억을 찾게 된다면 저번처럼 관리소에 데리고 오시면 됩니다.\n그럼, 앞으로도 잘 부탁드립니다!";
        }
        else if(Input.GetMouseButtonDown(0) && order == 10)
        {
            StartCoroutine(LoadingScene());
        }
    }

    public IEnumerator LoadingScene()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync("Loading");

        while (!loading.isDone) //씬 로딩 완료시 로딩완료시 완료된다.
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
        }
    }

    public void OnClick_SelectDog()
    {
        for (int i = 0; i < 33; i++)
        {
            if (Selected[i].activeSelf)
            {
                for (int j = 0; j < CollectedDogDatas.collectedDogData.Count; j++)
                {
                    if (SelectedImage[i].sprite.name == CollectedDogDatas.collectedDogData[j].DogImageName)
                    {
                        SelectedText.text = CollectedDogDatas.collectedDogData[j].DogInformation;
                        SelectedNameText.text = CollectedDogDatas.collectedDogData[j].DogName;
                    }
                }
            }
        }
    } //도감 설명 text를 위함

    private void LoadDogStat()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("JsonFiles/CollectedDog/CollectedDog");
        if (jsonFile != null)
        {
            string jsonData = jsonFile.text;
            collectDogDataList = JsonUtility.FromJson<DogStat>(jsonData);

            if (collectDogDataList != null && collectDogDataList.dogs.Count > 0)
            {
                Debug.Log("Dog Exist in the JSON file.");
            }
            else
            {
                Debug.LogError("Invalid JSON data or empty sets in the JSON file.");
            }
        }
        else
        {
            Debug.LogError("JSON file not found.");
        }
    } // 강아지들의 스탯 확인

    private void LoadDogCollect()
    {
        string jsonFilePath = Path.Combine(Application.persistentDataPath, "collecteddog.json");
        Debug.Log("파일의 경로는"+jsonFilePath);

        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            CollectedDogDatas = JsonUtility.FromJson<CollectedDogData>(jsonData);
            Debug.Log("JSON 파일 불러오기 성공!");
        }
        else
        {
            Debug.LogWarning("JSON 파일이 존재하지 않습니다.");
            SaveNewData();
        }
    }

    private void SaveNewData()
    {
        string jsonFilePath = Path.Combine(Application.persistentDataPath, "collecteddog.json");
        string jsonData = JsonUtility.ToJson(CollectedDogDatas);

        try
        {
            File.WriteAllText(jsonFilePath, jsonData);
            Debug.Log("Inventory JSON file saved successfully.");
        }
        catch (IOException e)
        {
            Debug.LogError("Error writing inventory JSON file: " + e.Message);
        }
    }

    private void CalculateProbability()
    {
        double[] distanceFromDesiredValues = new double[collectDogDataList.dogs.Count];
        for (int i = 0; i < collectDogDataList.dogs.Count; i++)
        {
            DogData dogData = collectDogDataList.dogs[i];
            double distanceSquared = Math.Sqrt(Math.Pow(CollectEnergy - dogData.Energy, 2)+ Math.Pow(CollectSociality - dogData.Sociality, 2)+ Math.Pow(CollectDeliberation - dogData.Deliberation, 2)
                + Math.Pow(CollectCuriosoty - dogData.Curiosoty, 2)+ Math.Pow(CollectLove - dogData.Love, 2));
            distanceFromDesiredValues[i] = Math.Sqrt(distanceSquared);
        }

        double maxDistance = distanceFromDesiredValues.Max();
        double minDistance = distanceFromDesiredValues.Min();

        double[] probabilities = new double[collectDogDataList.dogs.Count];
        for (int i = 0; i < collectDogDataList.dogs.Count; i++)
        {
            // max와 min이 같으면 완전 랜덤
            if(maxDistance==minDistance)
            {
                selectedDogIndex = UnityEngine.Random.Range(0, 32);
                return;
            }
            // 거리 값이 작을수록 높은 확률을 부여하되, 최대값과 최소값 사이로 정규화
            double normalizedDistance = (distanceFromDesiredValues[i] - minDistance) / (maxDistance - minDistance);
            // 정규화된 값을 반전시켜서 작은 값이 높은 확률을 가지도록 함
            double probability = 1 - normalizedDistance;
            // 확률 값을 배열에 저장
            probabilities[i] = probability;
        }

        double totalProbability = probabilities.Sum();
        float totalProbabilityFloat = (float)totalProbability;

        // 랜덤 값을 생성하여 뽑기
        double randomValue = UnityEngine.Random.Range(0, totalProbabilityFloat);
        double cumulativeProbability = 0;
        selectedDogIndex = -1;
        for (int i = 0; i < collectDogDataList.dogs.Count; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randomValue <= cumulativeProbability)
            {
                selectedDogIndex = i;
                break;
            }
        }
    }

    private void OnClick_CollectDog()
    {
        SaveDogData();

        ShowDogInformation.text = SelectedDogInformation;
        ShowDogName.text = SelectedDogName;
        Sprite sprite = Resources.Load<Sprite>("Image/CollectDog/" + SelectedDogImage);
        if(sprite!=null)
        {
            ShowDogImage.sprite = sprite;
        }
        else
        {
            Debug.LogError("이미지를 찾을 수 없습니다");
        }
    }

    private void SaveDogData()
    {
        DogInfo CollectedDogInfo = new DogInfo
        {
            DogName = collectDogDataList.dogs[selectedDogIndex].Name,
            DogInformation = collectDogDataList.dogs[selectedDogIndex].BackStory,
            DogImageName = collectDogDataList.dogs[selectedDogIndex].Image
        };

        SelectedDogName = collectDogDataList.dogs[selectedDogIndex].Name;
        SelectedDogInformation = collectDogDataList.dogs[selectedDogIndex].BackStory;
        SelectedDogImage = collectDogDataList.dogs[selectedDogIndex].Image;

        CollectedDogDatas.collectedDogData.Add(CollectedDogInfo);

        string jsonFilePath = Path.Combine(Application.persistentDataPath, "collecteddog.json");
        string jsonData = JsonUtility.ToJson(CollectedDogDatas);

        try
        {
            File.WriteAllText(jsonFilePath, jsonData);
            Debug.Log("Inventory JSON file saved successfully.");
        }
        catch (IOException e)
        {
            Debug.LogError("Error writing inventory JSON file: " + e.Message);
        }
    } // 스탯에 따른 강아지 수집 및 저장

    private void UnlockDogInformation()
    {
        for(int i=0;i<33;i++)
        {
            for(int j=0;j< CollectedDogDatas.collectedDogData.Count; j++)
            {
                if(DogImage[i].sprite.name == CollectedDogDatas.collectedDogData[j].DogImageName)
                {
                    Block[i].SetActive(false);
                }
            }
        }
    }
}

[System.Serializable]
public class CollectedDogData
{
    public List<DogInfo> collectedDogData;
}

[System.Serializable]
public class DogInfo
{
    public string DogName;
    public string DogInformation;
    public string DogImageName;
} // 도감 강아지 json

[System.Serializable]
public class DogStat
{
    public List<DogData> dogs;
}

[System.Serializable]
public class DogData
{
    public int Num;
    public string Name;
    public string Image;
    public string Look;
    public string BackStory;
    public int Energy;
    public int Sociality;
    public int Deliberation;
    public int Curiosoty;
    public int Love;
} // 강아지 json