using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstGameEnd : MonoBehaviour
{
    public Text NameText;
    public Text ChatText;
    public GameObject GhostImage;
    private int order;
    private int Day;
    private string PlayerName;
    private Color PlayerNameColor;
    private Color PlayerChatColor;
    private Color JayNameColor;
    private Color JayChatColor;

    private string[] Chat = {  "수고하셨습니다! \n 적응이 빠르시네요!",
        "(관리소를 나오니 작은 유령이 \n나를 빼꼼 훔쳐보고 있다.)",
    "제이. 아까부터 궁금한 게 있는데, \n저 유령들도 강아지야?",
        "네 맞아요. 생전의 기억이 없으면 \n저렇게 유령으로 오게 돼요.",
        "기억을 찾으면 모습도 찾을 수 있지만.. \n인간의 출입이 금지되어서 거의 불가능해요.",
        "강아지들의 기억은.. \n대부분 사람과의 기억이니까요",
        "기억을 찾아줄 방법이 있을까? \n있다면 내가 도와주고 싶어.",
    "가능할 진 모르지만.. \n돌보는 법 정도는 알려드릴게요.",
    "(제이가 숨어있던 유령을 데려온다)",
    };

    private void Awake()
    {
        Day = PlayerPrefs.GetInt("Day");
        PlayerPrefs.SetInt("Day", Day);
        PlayerName = PlayerPrefs.GetString("Player");

        PlayerNameColor = new Color32(47, 110, 255, 255);
        PlayerChatColor = new Color32(50, 50, 50, 255);
        JayNameColor = new Color32(84, 84, 84, 255);
        JayChatColor = new Color32(123, 123, 123, 255);
    }

    private void Start()
    {
        if (Day%20 == 1 && Day/20==0)
        {
            NameText.text = "제이";
            gameObject.SetActive(true);
            ChatText.text = Chat[0];
            order = 1;
        }
        else
            gameObject.SetActive(false);
    }

    void Update()
    {
        if (order == Chat.Length && Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("Upbringing");
        }
        else if (Input.GetMouseButtonDown(0) && (order == 3 || order == 7))
        {
            GhostImage.SetActive(true);
            NameText.text = "제이";
            ChatText.text = Chat[order];
            NameText.color = JayNameColor;
            ChatText.color = JayChatColor;
            order++;
        }
        else if (Input.GetMouseButtonDown(0) && (order == 2||order==6))
        {
            GhostImage.SetActive(true);
            NameText.text = PlayerName;
            ChatText.text = Chat[order];
            NameText.color = PlayerNameColor;
            ChatText.color = PlayerChatColor;
            order++;
        }
        else if (Input.GetMouseButtonDown(0) && (order==1|| order == 8))
        {
            GhostImage.SetActive(true);
            NameText.text = "";
            ChatText.text = Chat[order];
            order++;
        }
        else if (Input.GetMouseButtonDown(0) && Chat.Length > order)
        {
            ChatText.text = Chat[order];
            order++;
        }
    }
}
