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

    // Start is called before the first frame update
    void Start()
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
        Close.SetActive(false);
        Open.SetActive(true);
        yield return new WaitForSeconds(WaitSeconds - 2);
        Open.SetActive(false);
        Gacha.SetActive(false);
        GachaResult.SetActive(true);
    }
}
