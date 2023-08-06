using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DefaultDialogueManager : MonoBehaviour
{
    public Text textUI;
    public string randomMessage;

    private List<string> messages;

    public static DefaultDialogueManager Instance { get; private set; } = null;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        LoadDialogue();
        DisplayRandomMessage();

        if (CollectedDog.Instance.CollectedDogDatas.collectedDogData.Count == 32)
        {
            textUI.text = "내 일기를 읽어줘";
        }
    }

    private void LoadDialogue()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("JsonFiles/Dialogue/Default_dialogue");

        if (jsonFile != null)
        {
            string jsonData = jsonFile.text;
            DialogueData dialogueData = JsonUtility.FromJson<DialogueData>(jsonData);

            messages = new List<string>();
            foreach (Dialogue dialogue in dialogueData.dialogue)
            {
                messages.Add(dialogue.message);
            }
        }
        else
        {
            Debug.LogError("Dialogue file not found at path: " + jsonFile);
        }
    }

    private void DisplayRandomMessage()
    {
        if (messages != null && messages.Count > 0)
        {
            int randomIndex = Random.Range(0, messages.Count);
            randomMessage = messages[randomIndex];
            textUI.text = randomMessage;
        }
        else
        {
            Debug.LogWarning("No messages found in dialogue.");
        }
    }
}

[System.Serializable]
public class DialogueData
{
    public Dialogue[] dialogue;
}

[System.Serializable]
public class Dialogue
{
    public int TID;
    public string message;
}
