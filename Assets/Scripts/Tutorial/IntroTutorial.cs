using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroTutorial : MonoBehaviour
{
    public GameObject NameQuestion;
    public Text ChatText;
    private int order;
    private string Name;
    private bool IsClick=true;
    private int Day;

    private string[] Chat = {  "??? : 마침 7번 관리소가 자리가 비어서 이곳을 담당하게 되실거에요.","??? : 아 참, 서로 통성명도 안했네요. 혹시 성함이 어떻게 되실까요?",
    "??? : 아하…확인했습니다.", "제이 : 저는 ‘제이’라고 합니다.7번 관리소 업무에 대해서 제가 알려드릴게요.",
        "제이 : 앞으로 해야 할 일에 대해 알려드릴 테니,잘 보시고 따라하시면 됩니다."};

    private void Awake()
    {
        Day = PlayerPrefs.GetInt("Day", 0);
        PlayerPrefs.SetInt("Day", Day);
    }

    private void Start()
    {
        if (Day >= 1)
            gameObject.SetActive(false);
        else if (Day / 20 == 0 && Day % 20 == 0)
        {
            gameObject.SetActive(true);
            ChatText.text = Chat[0];
            order = 1;
        }
    }
    void Update()
    {

        if (order == Chat.Length && Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
            Day++;
            PlayerPrefs.SetInt("Day", Day);
            SceneManager.LoadScene("Heaven");
        }
        if (Input.GetMouseButtonDown(0) && Chat.Length>order && IsClick)
        {
            ChatText.text = Chat[order];
            if (order == 2)
            {
                NameCreate();
                ChatText.text = "";
                order++;
            }
            order++;
        }
    }

    void NameCreate()
    {
        NameQuestion.SetActive(true);
        IsClick = false;
    }

    public void OnClick_Check()
    {
        NameQuestion.SetActive(false);
        IsClick = true;
        ChatText.text = Chat[3];
    }
}
