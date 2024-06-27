using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Image image14;
    public Image image15;
    public Image image16;
    public Image image17;
    public Image image18;
    public Image image19;
    public Image image20;
    public Image image21;
    public Image image22;
    public Image image23;
    public Image image24;
    public Image image25;
    public Image image26;
    public Image image27;
    public Image image28;
    public RectTransform EndingCredit;
    public GameObject Credit;

    public Button myButton;
    public GameObject book1Prefab;
    public GameObject book2Prefab;
    public GameObject prev1;
    public GameObject prev2;
    public GameObject next1;
    public GameObject next2;

    public int clickCount = 0;
    private bool IsCredit=false;

    void Start()
    {
        AudioManager.Instance.PlayBGM("Ending");
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
            //image8 fade in
            image8.gameObject.SetActive(true);
        }
        else if (clickCount == 8)
        {
            //fadeout
            image8.gameObject.SetActive(false);
            image9.gameObject.SetActive(true);
        }
        else if (clickCount == 9)
        {
            image9.gameObject.SetActive(false);
            image10.gameObject.SetActive(true);
        }
        else if (clickCount == 10)
        {
            image10.gameObject.SetActive(false);
            image11.gameObject.SetActive(true);
        }
        else if (clickCount == 11)
        {
            image11.gameObject.SetActive(false);
            image12.gameObject.SetActive(true);
        }
        else if (clickCount == 12)
        {
            image12.gameObject.SetActive(false);
            image13.gameObject.SetActive(true);
        }
        else if (clickCount == 13)
        {
            image13.gameObject.SetActive(false);
            image14.gameObject.SetActive(true);
        }
        else if (clickCount == 14)
        {
            image14.gameObject.SetActive(false);
            image15.gameObject.SetActive(true);
        }
        else if (clickCount == 15)
        {
            image15.gameObject.SetActive(false);
            image16.gameObject.SetActive(true);
        }
        else if (clickCount == 16)
        {
            image16.gameObject.SetActive(false);
            image17.gameObject.SetActive(true);
        }
        else if (clickCount == 17)
        {
            image17.gameObject.SetActive(false);
            image18.gameObject.SetActive(true);
        }
        else if (clickCount == 18)
        {
            image18.gameObject.SetActive(false);
            image19.gameObject.SetActive(true);
        }
        else if (clickCount == 19)
        {
            //image19 fadeout
            image19.gameObject.SetActive(false);
            image20.gameObject.SetActive(true);
        }
        else if (clickCount == 20)
        {
            image20.gameObject.SetActive(false);
            image21.gameObject.SetActive(true);
        }
        else if (clickCount == 21)
        {
            image21.gameObject.SetActive(false);
            image22.gameObject.SetActive(true);
        }
        else if (clickCount == 22)
        {
            image22.gameObject.SetActive(false);
            image23.gameObject.SetActive(true);
        }
        else if (clickCount == 23)
        {
            image23.gameObject.SetActive(false);
            image24.gameObject.SetActive(true);
        }
        else if (clickCount == 24)
        {
            image24.gameObject.SetActive(false);
            image25.gameObject.SetActive(true);
        }
        else if (clickCount == 25)
        {
            image25.gameObject.SetActive(false);
            image26.gameObject.SetActive(true);
        }
        else if (clickCount == 26)
        {
            image26.gameObject.SetActive(false);
            image27.gameObject.SetActive(true);
        }
        else if (clickCount == 27)
        {
            image27.gameObject.SetActive(false);
            image28.gameObject.SetActive(true);
        }
        else if (clickCount == 28)
        {
            /*
            image28 뿌옇게 처리
            endingcredit
            */
            AudioManager.Instance.PlayBGM("Outro");
            Credit.SetActive(true);
            IsCredit = true;
        }
    }

    private void Update()
    {
        int End= PlayerPrefs.GetInt("End", 0);

        if (End == 1 && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadSceneAsync("Loading");
        }
        if (IsCredit)
        {
            if(EndingCredit.anchoredPosition.y>=1500)
            {
                IsCredit = false;
                PlayerPrefs.SetInt("End", 1);
                return;
            }
            float CreditPosition = EndingCredit.anchoredPosition.y+Time.deltaTime*65;
            EndingCredit.anchoredPosition = new Vector2(0, CreditPosition);
            Debug.Log(EndingCredit.anchoredPosition);
        }
    }
}

