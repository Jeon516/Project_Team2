using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroTutorial : MonoBehaviour
{
    public Text ChatText;
    private int order;

    private string[] Chat = { "아, 이번에 7번 관리소를 담당하게 된 분이신가요?","혹시 성함이 어떻게 되실까요?", 
    "아하... 확인했습니다", "반갑습니다! 저는 7번 관리소의 인수인계를 맡게 된 '케이'라고 합니다.", 
        "앞으로 해야 할 일에 대해 알려드릴 테니,잘 보시고 따라하시면 됩니다."};

    private void Update()
    {
        ChatText.text = Chat[order];

        if(Input.GetMouseButtonDown(0) && Chat.Length>order)
        {
            order++;
        }
    }


}
