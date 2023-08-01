using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopGacha : MonoBehaviour
{
    public int WaitSeconds = 3;
    public GameObject Close;
    public GameObject Open;
    public GameObject Gacha;
    public GameObject GachaResult;

    public static ShopGacha Instance { get; private set; } = null;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    public void GachaStart()
    {
        Gacha.SetActive(true);
        Close.SetActive(true);
        Open.SetActive(false);
        GachaResult.SetActive(false);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(WaitSeconds);
        AudioManager.Instance.PlaySFX("GachaOpen");
        Close.SetActive(false);
        Open.SetActive(true);
        yield return new WaitForSeconds(WaitSeconds - 2);
        Open.SetActive(false);
        Gacha.SetActive(false);
        GachaResult.SetActive(true);
    }
}
