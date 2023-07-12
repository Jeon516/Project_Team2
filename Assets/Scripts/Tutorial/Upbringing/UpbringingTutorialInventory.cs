using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpbringingTutorialInventory : MonoBehaviour
{
    private int order = 0;
    private int Day;
    private int TutorialDay;
    private bool IsClick = false;

    public Text ChatText;
    public GameObject ChatSet;
    public GameObject TutorialEat;
    public RectTransform ChatTransform;

    private string[] Chat = { "구매한 음식은 이곳에서 확인할 수 있습니다.", "방금 구매한 음식이 저기 보이네요. 한 번 확인해보시겠어요?",
    "선택한 음식에 대한 정보는 여기서 확인할 수 있습니다.","음식은 하루에 한 번씩만 먹일 수 있지만, 지금은 하나밖에 없으니 유령에게 이 음식을 먹여봅시다.",
    "이렇게 음식을 먹였을 때 이곳에서 유령의 성향이 변하는 것을 확인할 수 있습니다."};

    private void Awake()
    {
        Day = PlayerPrefs.GetInt("Day");
        PlayerPrefs.SetInt("Day", Day);
        TutorialDay = PlayerPrefs.GetInt("TutorialDay", 0);
        PlayerPrefs.SetInt("TutorialDay", TutorialDay);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && order == 2 && IsClick)
        {
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order == 3 && IsClick)
        {
            ChatDisplay();
        }
    }

    public void Onclick_Cancel()
    {
        TutorialEat.SetActive(true);
        ChatSet.SetActive(true);
        IsClick = true;
        ChatDisplay();
    }

    public void Onclick_TutorialInventory()
    {
        TutorialEat.SetActive(false);
        order++;
        ChatDisplay();
        IsClick = true;
    }

    void ChatDisplay()
    {
        ChatText.text = Chat[order];
        order++;
    }
}
