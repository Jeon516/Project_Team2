using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpbringingInformation : MonoBehaviour
{
    public Text ChatText;
    public RectTransform ChatTransform;
    public GameObject TutorialInformationButton;
    public GameObject TutorialInformation;
    public GameObject NextDay;
    public GameObject Tutorial;

    private int order = 0;
    private bool IsClick = false;
    private string[] Chat = { "���������� �̰͸� �˷��帮�� \n������ ��ġ���� �ϰڽ��ϴ�.","���� ������ ���̽���? \n�� �� ����ðھ��?"
            ,"���⼭ ���ݱ��� Ȯ���� ���İ� \n�������� ������ Ȯ���� �� �ֽ��ϴ�.","��� ��ϵǴ����� ���ؼ� ���񿡰� �˷��� �ٴ� ������...��, �� ���� ��ü�� Ư���ϴϱ��. �ǿܷ� ���� �� �ϼ��� ����.",
    "�� ������ ��������Դϴ�."," �� ��, �� ���� ��� ���ƴٸ� �ֹ��ð� ���� ����Ͻø� �˴ϴ�. �׷� ������ �˰ڽ��ϴ�!"};

    // Start is called before the first frame update
    void Start()
    {
        ChatDisplay();
        ChatTransform.anchoredPosition = new Vector2(0, -296);
        IsClick = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && order == 1 && IsClick)
        {
            TutorialInformationButton.SetActive(true);
            ChatDisplay();
            IsClick = false;
        }
        else if (Input.GetMouseButtonDown(0) && order == 4 && IsClick)
        {
            TutorialInformation.SetActive(false);
            ChatTransform.anchoredPosition = new Vector2(0, -296);
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order == 5 && IsClick)
        {
            NextDay.SetActive(true);
            ChatTransform.anchoredPosition = new Vector2(-220, -296);
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order < Chat.Length && IsClick)
        {
            ChatDisplay();
        }
    }

    void ChatDisplay()
    {
        ChatText.text = Chat[order];
        order++;
    }

    public void Onclick_Information()
    {
        TutorialInformationButton.SetActive(false);
        IsClick = true;
        TutorialInformation.SetActive(true);
        ChatTransform.anchoredPosition = new Vector2(0, -225);
        ChatDisplay(); 
    }

    public void Onclick_Nextday()
    {
        Tutorial.SetActive(false);
    }
}
