using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatMove : MonoBehaviour
{
    public GameObject[] State;

    Dictionary<int, string> StatOrder = new Dictionary<int, string>(); // Ω∫≈» µÒº≈≥ ∏Æ

    private void Awake()
    {
        StatOrder.Add(0, "Energy");
        StatOrder.Add(1, "Sociality");
        StatOrder.Add(2, "Deliberation");
        StatOrder.Add(3, "Curiosoty");
        StatOrder.Add(4, "Love");
    } // µÒº≈≥ ∏Æ √ ±‚»≠

    private void Update()
    {
        float EnergyX = PlayerPrefs.GetFloat("EnergyX");
        float SocialityX = PlayerPrefs.GetFloat("SocialityX");
        float DeliberationX = PlayerPrefs.GetFloat("DeliberationX");
        float CuriosotyX = PlayerPrefs.GetFloat("CuriosotyX");
        float LoveX = PlayerPrefs.GetFloat("LoveX");

        /*for (int i = 0; i < 5; i++)
        {
            RectTransform rectTransform = State[i].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(PlayerPrefs.GetFloat(StatOrder[i] + "X")*99/111, PlayerPrefs.GetFloat(StatOrder[i] + "Y"));
        }*/

        RectTransform rectTransform1 = State[0].GetComponent<RectTransform>();
        rectTransform1.anchoredPosition = new Vector2(EnergyX * 99 / 111, PlayerPrefs.GetFloat(StatOrder[0] + "Y"));

        RectTransform rectTransform2 = State[1].GetComponent<RectTransform>();
        rectTransform2.anchoredPosition = new Vector2(SocialityX * 99 / 111, PlayerPrefs.GetFloat(StatOrder[1] + "Y"));

        RectTransform rectTransform3 = State[2].GetComponent<RectTransform>();
        rectTransform3.anchoredPosition = new Vector2(DeliberationX * 99 / 111, PlayerPrefs.GetFloat(StatOrder[2] + "Y"));

        RectTransform rectTransform4 = State[3].GetComponent<RectTransform>();
        rectTransform4.anchoredPosition = new Vector2(CuriosotyX * 99 / 111, PlayerPrefs.GetFloat(StatOrder[3] + "Y"));

        RectTransform rectTransform5 = State[4].GetComponent<RectTransform>();
        rectTransform5.anchoredPosition = new Vector2(LoveX * 99 / 111, PlayerPrefs.GetFloat(StatOrder[4] + "Y"));
    } // Ω∫≈» øÚ¡˜¿” π›øµ
}
