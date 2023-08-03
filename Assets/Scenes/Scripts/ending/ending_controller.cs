using UnityEngine;
using UnityEngine.UI;

public class ending_controller : MonoBehaviour
{
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Image image5;
    public Image image6;
    public Image image7;
    public Image image8;
    public Image image9;
    public Image image10;
    public Image image11;
    public Image image12;
    public Image image13;

    public Button myButton;
    public GameObject book1Prefab;
    public GameObject book2Prefab;
    public GameObject prev1;
    public GameObject prev2;
    public GameObject next1;
    public GameObject next2;

    public int clickCount = 0;

    void Start()
    {
        myButton.onClick.AddListener(OnMyButtonClick);
        book1Prefab.SetActive(false);
        book2Prefab.SetActive(false);
        prev1.SetActive(false);
        prev2.SetActive(false);
        next1.SetActive(false);
        next2.SetActive(false);
    }

    private void OnMyButtonClick()
    {
        clickCount++;
        if (clickCount == 1)
        {
            image1.gameObject.SetActive(false);
            image2.gameObject.SetActive(false);
            image3.gameObject.SetActive(false);
            image4.gameObject.SetActive(false);
            myButton.gameObject.SetActive(false);
            book1Prefab.SetActive(true);
            next1.gameObject.SetActive(true);
            prev1.gameObject.SetActive(true);
        }
        else if (clickCount == 2)
        {
            book1Prefab.SetActive(false);
            image2.gameObject.SetActive(true);
            next1.gameObject.SetActive(false);
            prev1.gameObject.SetActive(false);
        }
        else if (clickCount == 3)
        {
            image2.gameObject.SetActive(false);
            image3.gameObject.SetActive(true);
        }
        else if (clickCount == 4)
        {
            image3.gameObject.SetActive(false);
            myButton.gameObject.SetActive(false);
            book2Prefab.SetActive(true);
            prev2.gameObject.SetActive(true);
            next2.gameObject.SetActive(true);
        }
        else if (clickCount == 5)
        {
            book2Prefab.SetActive(false);
            image4.gameObject.SetActive(true);
            next2.gameObject.SetActive(false);
            prev2.gameObject.SetActive(false);
        }
        else if (clickCount == 6)
        {
            image4.gameObject.SetActive(false);
            image5.gameObject.SetActive(true);
            myButton.gameObject.SetActive(false);
        }
        else if (clickCount == 7)
        {
            image6.gameObject.SetActive(false);
            image7.gameObject.SetActive(true);
        }
        else if (clickCount == 8)
        {
            image7.gameObject.SetActive(false);
            image8.gameObject.SetActive(true);
        }
        else if (clickCount == 9)
        {
            image8.gameObject.SetActive(false);
            image9.gameObject.SetActive(true);
        }
        else if (clickCount == 10)
        {
            image9.gameObject.SetActive(false);
            image10.gameObject.SetActive(true);
        }
        else if (clickCount == 11)
        {
            image10.gameObject.SetActive(false);
            image11.gameObject.SetActive(true);
        }
        else if (clickCount == 12)
        {
            image11.gameObject.SetActive(false);
            image12.gameObject.SetActive(true);
        }
        else if (clickCount == 13)
        {
            image12.gameObject.SetActive(false);
            image13.gameObject.SetActive(true);
        }
    }
}
