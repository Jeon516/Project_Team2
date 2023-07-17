using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class TextLoader : MonoBehaviour
{
    public Text displayText;
    public static string textValue = "example_text_value";
    public string TextValue
    {
        get { return textValue; }
    }
    private void Start()
    {
        GameProcess.OnButtonClicked += OnGameProcessButtonClicked;
    }

    private void OnGameProcessButtonClicked()
    {
        LoadTextFromJSON();
    }

    private void LoadTextFromJSON()
    {
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
                List<SetData> matchingSets = setDataList.Sets.FindAll(setData => setData.Color == randomColor3);
                float matchingTextProbability = (float)GetComponent<GameProcess>().SE / 100f;

                float nonMatchingTextProbability = (float)GetComponent<GameProcess>().AE / 100f;

                bool useMatchingText = Random.value < matchingTextProbability;

                string textValue;
                if (useMatchingText && matchingSets.Count > 0)
                {
                    int randomIndex = Random.Range(0, matchingSets.Count);
                    textValue = matchingSets[randomIndex].Sort;
                }
                else
                {
                    List<SetData> nonMatchingSets = setDataList.Sets.FindAll(setData => setData.Color != randomColor3);
                    int randomIndex = Random.Range(0, nonMatchingSets.Count);
                    textValue = nonMatchingSets[randomIndex].Sort;
                }

                displayText.text = textValue;
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
    }
}
