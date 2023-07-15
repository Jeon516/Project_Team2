using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class EmblemLoader : MonoBehaviour
{
    public Image emblemImage;

    private void Start()
    {
        GameProcess.OnButtonClicked += OnGameProcessButtonClicked;
    }

    private void OnGameProcessButtonClicked()
    {
        LoadEmblemImageFromJSON();
    }

    private void LoadEmblemImageFromJSON()
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

                float matchingEmblemProbability = (float)GetComponent<GameProcess>().SN / 100f;

                float nonMatchingEmblemProbability = (float)GetComponent<GameProcess>().AN / 100f;

                bool useMatchingEmblem = Random.value < matchingEmblemProbability;

                string emblemAssets;
                if (useMatchingEmblem && matchingSets.Count > 0)
                {
                    int randomIndex = Random.Range(0, matchingSets.Count);
                    emblemAssets = matchingSets[randomIndex].emblemAssets;
                }
                else
                {
                    List<SetData> nonMatchingSets = setDataList.Sets.FindAll(setData => setData.Color != randomColor3);
                    int randomIndex = Random.Range(0, nonMatchingSets.Count);
                    emblemAssets = nonMatchingSets[randomIndex].emblemAssets;
                }

                LoadEmblemImage(emblemAssets);
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

    private void LoadEmblemImage(string emblemAssets)
    {
        string imagePath = Path.Combine("Assets/Download Assets/Fantasy Emblem3(living) Set Pack/FantasyEmblem3_128_W", emblemAssets + ".png");

        Texture2D texture = LoadTextureFromFile(imagePath);

        if (texture != null)
        {

            Sprite emblemSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            emblemImage.sprite = emblemSprite;
        }
        else
        {
            Debug.LogError("해당 주소에 대한 엠블럼 이미지를 찾을 수 없습니다: " + emblemAssets);
        }
    }

    private Texture2D LoadTextureFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            return texture;
        }
        else
        {
            Debug.LogError("파일을 찾을 수 없습니다: " + filePath);
            return null;
        }
    }
}
