using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpbringingInformation : MonoBehaviour
{
    public Text ChatText;
    public Button CancelButton;
    public Button Nobutton;
    public Button[] Buttons;
    public RectTransform ChatTransform;
    public GameObject TutorialInformationButton;
    public GameObject TutorialInformation;
    public GameObject NextDay;
    public GameObject Tutorial;
    public GameObject ChatSet;

    private int order = 0;
    private string[] Chat = { "���������� �̰͸� �˷��帮�� \n������ ��ġ���� �ϰڽ��ϴ�.","���� ������ ���̽���? \n�� �� ����ðھ��?"
            ,"���⼭ ���ݱ��� Ȯ���� ���İ� \n�������� ������ Ȯ���� �� �ֽ��ϴ�.","��� ��ϵǴ����� ���ؼ� ���񿡰� �˷��� �ٴ� ������...��, �� ���� ��ü�� Ư���ϴϱ��. �ǿܷ� ���� �� �ϼ��� ����.",
    "�� ������ ��������Դϴ�."," �� ��, �� ���� ��� ���ƴٸ� �ֹ��ð� ���� ����Ͻø� �˴ϴ�. �׷� ������ �˰ڽ��ϴ�!"};

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<2;i++)
        {
            Buttons[i].interactable = false;
        }
        CancelButton.interactable = false;
        ChatSet.SetActive(true);
        ChatDisplay();
        NextDay.SetActive(false);
        ChatTransform.anchoredPosition = new Vector2(0, -220);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && order == 1)
        {
            ChatSet.SetActive(true);
            TutorialInformationButton.SetActive(true);
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order == 6)
        {
            ChatSet.SetActive(true);
            NextDay.SetActive(true);
        }
        else if (Input.GetMouseButtonDown(0) && order == 3)
        {
            CancelButton.interactable = true;
            ChatSet.SetActive(true);
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order == 5)
        {
            Nobutton.interactable = false;
            NextDay.SetActive(true);
            ChatSet.SetActive(true);
            ChatDisplay();
        }
    }

    public void Onclikc_CancelButton()
    {
        ChatSet.SetActive(true);
        TutorialInformation.SetActive(false);
        ChatTransform.anchoredPosition = new Vector2(0, -220);
        ChatDisplay();
    }

    void ChatDisplay()
    {
        ChatText.text = Chat[order];
        order++;
    }

    public void Onclick_Information()
    {
        ChatSet.SetActive(true);
        TutorialInformationButton.SetActive(false);
        TutorialInformation.SetActive(true);
        ChatTransform.anchoredPosition = new Vector2(0, -160);
        ChatDisplay(); 
    }

    public void Onclick_Nextday()
    {
        Tutorial.SetActive(false);
    }
}
