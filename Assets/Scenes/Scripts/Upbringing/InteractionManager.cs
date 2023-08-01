using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    public string jsonFilePath;

    public Text LowInteractionText;
    public Text MiddleInteractionText;
    public Text HighInteractionText;
    public Text ConservationText;

    public string LowInteractionConservation;
    public string MiddleInteractionConservation;
    public string HighInteractionConservation;

    public int LowFlavor;
    public int MiddleFlavor;
    public int HighFlavor;

    private int Low;
    private int Middle;
    private int High;

    private List<string> ValueData= new List<string>();
    private List<string> InteractionData = new List<string>();
    public List<string> ConversationData = new List<string>();
    private List<int> FlavorData = new List<int>();

    public static InteractionManager Instance { get; private set; } = null;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadJsonFile();
        Low = Random.Range(0, 30);
        Middle = Random.Range(30, 60);
        High = Random.Range(60, 90);


        Debug.Log(InteractionData[Low]);
        LowInteractionText.text = InteractionData[Low];
        MiddleInteractionText.text = InteractionData[Middle];
        HighInteractionText.text = InteractionData[High];

        LowFlavor = FlavorData[Low];
        MiddleFlavor = FlavorData[Middle];
        HighFlavor = FlavorData[High];

        LowInteractionConservation = ConversationData[Low];
        MiddleInteractionConservation = ConversationData[Middle];
        HighInteractionConservation = ConversationData[High];
    }

    private void LoadJsonFile()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("JsonFiles/Interaction/FlavorInteraction");
        if (jsonFile != null)
        {
            string jsonData = jsonFile.text;
            FlavorInteractionData interactionDataList = JsonUtility.FromJson<FlavorInteractionData>(jsonData);

            if (interactionDataList != null && interactionDataList.FlavorInteraction.Count > 0)
            {
                foreach (InteractionData interactionData in interactionDataList.FlavorInteraction)
                {
                    ValueData.Add(interactionData.Value);
                    InteractionData.Add(interactionData.Interaction);
                    ConversationData.Add(interactionData.Conversation);
                    FlavorData.Add(interactionData.Favor);
                }
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
    }
}

[System.Serializable]
public class FlavorInteractionData
{
    public List<InteractionData> FlavorInteraction;
}

[System.Serializable]
public class InteractionData
{
    public string Value;
    public string Interaction;
    public string Conversation;
    public int Favor;
}