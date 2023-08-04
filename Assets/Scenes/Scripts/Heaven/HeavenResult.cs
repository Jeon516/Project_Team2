using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeavenResult : MonoBehaviour
{
    public int Gold;
    public Text GoldText;
    public Text SuccessText;
    private void Awake()
    {
        Gold = PlayerPrefs.GetInt("Gold", 100000);
        PlayerPrefs.SetInt("Gold", Gold);
    }
    void Start()
    {
        AudioManager.Instance.PlaySFX("GameOver");
        GoldText.text = "È¹µæÇÑ ±¸¸§ Á¶°¢ : "+GameProcess.Instance.ingamegold.ToString();
        SuccessText.text = "¼º°ø È½¼ö : " + GameProcess.Instance.correct.ToString();
        PlayerPrefs.SetInt("Gold", Gold + GameProcess.Instance.ingamegold);
    }
}
