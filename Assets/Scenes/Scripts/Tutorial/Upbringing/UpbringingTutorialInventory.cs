using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpbringingTutorialInventory : MonoBehaviour
{
    private int order = 0;

    public Text ChatText;
    public Button[] InventoryButton;
    public GameObject ChatSet;
    public GameObject EatIcon;
    public GameObject Inventory;
    public GameObject Stat;
    public GameObject Interaction;
    public RectTransform ChatTransform;

    private string[] Chat = { "구매한 음식은 이곳에서 확인할 수 있습니다.", "방금 구매한 음식이 저기 보이네요. 한 번 클릭해보시겠어요?",
    "선택한 음식에 대한 정보는 여기서 확인할 수 있습니다.","음식은 하루에 한 번씩만 먹일 수 있지만, 지금은 하나밖에 없으니 유령에게 이 음식을 먹여봅시다.",
    "이렇게 음식을 먹였을 때 이곳에서 유령의 성향이 변하는 것을 확인할 수 있습니다."};

    private void Start()
    {
        ChatSet.SetActive(true);
        ChatDisplay();
        EatIcon.SetActive(true);

        for(int i=0;i<3;i++)
        {
            InventoryButton[i].interactable = false;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && order == 1)
        {
            ChatSet.SetActive(false);
        }
        else if (Input.GetMouseButtonDown(0) && order == 2)
        {
            ChatSet.SetActive(false);
        }
        else if (Input.GetMouseButtonDown(0) && order == 3)
        {
            InventoryButton[1].interactable = true;
            ChatDisplay();
        }
        else if (Input.GetMouseButtonDown(0) && order == 4)
        {
            ChatSet.SetActive(false);
        }
        else if (Input.GetMouseButtonDown(0) && order == Chat.Length)
        {
            Stat.SetActive(false);
            Interaction.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void Onclick_EatButton()
    {
        EatIcon.SetActive(false);
        ChatSet.SetActive(true);
        ChatTransform.anchoredPosition = new Vector2(0, 285);
        Inventory.SetActive(true);
        ChatDisplay();
    }

    public void Onclick_FoodIcon()
    {
        ChatSet.SetActive(true);
        ChatDisplay();
        ChatTransform.anchoredPosition = new Vector2(0, -220);
    }

    public void Onclick_Eat()
    {
        Stat.SetActive(true);
        StartCoroutine(Wait());
    }

    void ChatDisplay()
    {
        ChatText.text = Chat[order];
        order++;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        ChatSet.SetActive(true);
        ChatDisplay();
    }
}
