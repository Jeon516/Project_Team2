using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroTutorial : MonoBehaviour
{
    public Text ChatText;
    private int order;

    private string[] Chat = { "��, �̹��� 7�� �����Ҹ� ����ϰ� �� ���̽Ű���?","Ȥ�� ������ ��� �ǽǱ��?", 
    "����... Ȯ���߽��ϴ�", "�ݰ����ϴ�! ���� 7�� �������� �μ��ΰ踦 �ð� �� '����'��� �մϴ�.", 
        "������ �ؾ� �� �Ͽ� ���� �˷��帱 �״�,�� ���ð� �����Ͻø� �˴ϴ�."};

    private void Update()
    {
        ChatText.text = Chat[order];

        if(Input.GetMouseButtonDown(0) && Chat.Length>order)
        {
            order++;
        }
    }


}
