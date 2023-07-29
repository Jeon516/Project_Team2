using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Linq;

public class CollectedDog : MonoBehaviour
{
    private int CollectEnergy; 
    private int CollectSociality;
    private int CollectDeliberation;
    private int CollectCuriosoty;
    private int CollectLove;
    //Collect Stat

    private DogStat collectDogDataList;
    private string SelectedDog;

    private void Awake()
    {
        CollectEnergy = -4;//PlayerPrefs.GetInt("Energy");
        CollectSociality = 4;//PlayerPrefs.GetInt("Sociality");
        CollectDeliberation = -4;//PlayerPrefs.GetInt("Deliberation");
        CollectCuriosoty = -4;//PlayerPrefs.GetInt("Curiosoty");
        CollectLove = 4;//PlayerPrefs.GetInt("Love");
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadDogStat();    
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CalculateProbability();
            Debug.Log(SelectedDog);
        }
    }

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
        int selectedDogIndex = -1;
        for (int i = 0; i < collectDogDataList.dogs.Count; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randomValue <= cumulativeProbability)
            {
                selectedDogIndex = i;
                break;
            }
        }

        SelectedDog = collectDogDataList.dogs[selectedDogIndex].Name;
    }
}

[System.Serializable]
public class DogInformation
{
    public List<InteractionData> DogInformationData;
}

[System.Serializable]
public class DogInfo
{
    public string Value;
    public string Interaction;
    public string Conversation;
    public int Favor;
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
    public string Look;
    public string BackStory;
    public int Energy;
    public int Sociality;
    public int Deliberation;
    public int Curiosoty;
    public int Love;
} // 강아지 json