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
    // ���� ��� �� ������

    public GameObject[] Selected;
    public Image[] SelectedImage;
    public Text SelectedText;
    public Text SelectedNameText;
    //����

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
    //20���� ������ �̺�Ʈ

    public static CollectedDog Instance { get; private set; } = null;

    public void LastEvent()
    {
        BlackScreen.SetActive(true);
        ChatText.text = "(������ ������ ���� 20���� �Ǿ���.\n���� �������� �غ� �ؾ� �� �� ����.)";
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
                    ChatText.text = "(���� ������ ������ �� ���� �������� ���� ���Ѻ��� �ִ�.)";
                    Check = true;
                    break;
                }
            }

            if (!Check)
            {
                ChatText.text = "(���� ������ ó�� ���� �������� ���� ���Ѻ��� �ִ�.)";
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
            ChatText.text = "(�������� ���̿��� ������ ����.)";
            order++;
            //StartCoroutine(LoadingScene());
        }
        else if(Input.GetMouseButtonDown(0) && order == 4)
        {
            CharacterText.text = "����";
            CharacterText.color = JayNameColor;
            ChatText.text = "��? �� �������¡�\n ����! �� ������ ����� ã�ҳ� ������ ?";
            ChatText.color = JayChatColor;
            JayImage.SetActive(true);
            order++;
        } 
        else if(Input.GetMouseButtonDown(0) && order == 5)
        {
            order++;
            ChatText.text = "�������� �����ϼ̽��ϴ�!\n������ �Ͻô� ���� �� �������� ������ Ż �� �ֵ���\n�غ��ϰڽ��ϴ�.";
            if(UpbringingGameManager.Instance.Day/20!=1)
            {
                StartCoroutine(LoadingScene());
            }
        }
        else if (Input.GetMouseButtonDown(0) && order == 7)
        {
            order++;
            CharacterText.text = "";
            ChatText.text = "(�����Ҹ� ������ ���̰� ���� ���ɰ� �Բ� ���� ��ٸ��� �ִ�.)";
            ChatText.color = InformationChatColor;
            GhostImage.SetActive(true);
        }
        else if (Input.GetMouseButtonDown(0) && order == 8)
        {
            order++;
            CharacterText.text = "����";
            ChatText.text = "���ϡ���ġä�̳���? �̹��� �����ּž� �� �����Դϴ�.";
            ChatText.color = JayChatColor;
        }
        else if (Input.GetMouseButtonDown(0) && order == 9)
        {
            order++;
            ChatText.text = "������ ����� ã�� �ȴٸ� ����ó�� �����ҿ� ������ ���ø� �˴ϴ�.\n�׷�, �����ε� �� ��Ź�帳�ϴ�!";
        }
        else if(Input.GetMouseButtonDown(0) && order == 10)
        {
            StartCoroutine(LoadingScene());
        }
    }

    public IEnumerator LoadingScene()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync("Loading");

        while (!loading.isDone) //�� �ε� �Ϸ�� �ε��Ϸ�� �Ϸ�ȴ�.
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
    } //���� ���� text�� ����

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
    } // ���������� ���� Ȯ��

    private void LoadDogCollect()
    {
        string jsonFilePath = Path.Combine(Application.persistentDataPath, "collecteddog.json");
        Debug.Log("������ ��δ�"+jsonFilePath);

        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            CollectedDogDatas = JsonUtility.FromJson<CollectedDogData>(jsonData);
            Debug.Log("JSON ���� �ҷ����� ����!");
        }
        else
        {
            Debug.LogWarning("JSON ������ �������� �ʽ��ϴ�.");
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
            // max�� min�� ������ ���� ����
            if(maxDistance==minDistance)
            {
                selectedDogIndex = UnityEngine.Random.Range(0, 32);
                return;
            }
            // �Ÿ� ���� �������� ���� Ȯ���� �ο��ϵ�, �ִ밪�� �ּҰ� ���̷� ����ȭ
            double normalizedDistance = (distanceFromDesiredValues[i] - minDistance) / (maxDistance - minDistance);
            // ����ȭ�� ���� �������Ѽ� ���� ���� ���� Ȯ���� �������� ��
            double probability = 1 - normalizedDistance;
            // Ȯ�� ���� �迭�� ����
            probabilities[i] = probability;
        }

        double totalProbability = probabilities.Sum();
        float totalProbabilityFloat = (float)totalProbability;

        // ���� ���� �����Ͽ� �̱�
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
            Debug.LogError("�̹����� ã�� �� �����ϴ�");
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
    } // ���ȿ� ���� ������ ���� �� ����

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
} // ���� ������ json

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
} // ������ json