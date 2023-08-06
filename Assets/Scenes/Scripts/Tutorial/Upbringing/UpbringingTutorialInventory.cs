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

    private string[] Chat = { "������ ������ �̰����� Ȯ���� �� �ֽ��ϴ�.", "��� ������ ������ ���� ���̳׿�. �� �� Ŭ���غ��ðھ��?",
    "������ ���Ŀ� ���� ������ ���⼭ Ȯ���� �� �ֽ��ϴ�.","������ �Ϸ翡 �� ������ ���� �� ������, ������ �ϳ��ۿ� ������ ���ɿ��� �� ������ �Կ����ô�.",
    "�̷��� ������ �Կ��� �� �̰����� ������ ������ ���ϴ� ���� Ȯ���� �� �ֽ��ϴ�."};

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
