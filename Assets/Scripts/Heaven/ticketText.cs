using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ticketText : MonoBehaviour
{
    private class TicketData
    {
        public string Sort;
        public string Color;
        public string EmblemAssets;
    }

    public Text sortText;
    public Button[] buttons;
    private List<TicketData> ticketDataList;

    private void Start()
    {
        LoadJSON();
    }

    private void LoadJSON()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("JsonFiles/ticket");

        if (jsonFile != null)
        {
            ticketDataList = JsonUtility.FromJson<List<TicketData>>(jsonFile.text);

            if (ticketDataList != null && ticketDataList.Count > 0)
            {
                ChangeSortText();
            }
            else
            {
                Debug.LogWarning("Ticket 데이터가 비어 있거나 형식이 잘못되었습니다.");
                sortText.text = "No Data"; // 데이터가 비어 있을 경우 기본값 설정
            }
        }
        else
        {
            Debug.LogWarning("JsonFiles/ticket 파일이 존재하지 않습니다.");
            sortText.text = "File Not Found"; // 파일이 존재하지 않을 경우 기본값 설정
        }
    }

    private void ChangeSortText()
    {
        if (ticketDataList != null && ticketDataList.Count > 0)
        {
            int randomIndex = Random.Range(0, ticketDataList.Count);
            string randomSortValue = ticketDataList[randomIndex].Sort;

            sortText.text = randomSortValue;
            Debug.Log("Sort 값: " + randomSortValue);
        }
        else
        {
            Debug.LogWarning("Ticket 데이터가 비어 있거나 형식이 잘못되었습니다.");
            sortText.text = "No Sort Data"; // Sort 데이터가 없을 경우 기본값 설정
        }
    }

    public void OnButtonClick(int buttonIndex)
    {
        ChangeSortText();

        // 버튼 인덱스에 따른 추가 동작 수행
        Debug.Log("Button Index: " + buttonIndex);
    }
}
