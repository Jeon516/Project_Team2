using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[System.Serializable]
public class ImageData
{
    public Image image;
    public bool useInTransition = true;
}

public class FirstScene : MonoBehaviour
{
    public ImageData[] imagesToDisplay;
    public Button nextButton;
    public float transitionTimeIndex1 = 2.0f;
    public float pauseTimeAfterIndex1 = 3.0f;
    public float transitionTimeIndex3 = 5.0f;
    public Image loadImage;

    private int currentImageIndex = 0;
    private bool isTransitioning = false;
    private bool isSceneTransitionAllowed = true;
    private Coroutine transitionCoroutine;
    private Coroutine colorTransitionCoroutine;
    private bool isFade = false;
    private float fadeTime = 0f;


    void Start()
    {
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlaySFX("CarCrash");

        StartCoroutine(ShowLoading());
        ShowImage(currentImageIndex);

        if (nextButton != null)
        {
            nextButton.onClick.AddListener(ShowNextImage);
            nextButton.interactable = false;
        }
    }

    private void Update()
    {
        if(isFade)
        {
            fadeTime += Time.deltaTime;
            if(fadeTime >= 2)
            {
                imagesToDisplay[1].image.color = new Color(imagesToDisplay[1].image.color.r, imagesToDisplay[1].image.color.g,
                imagesToDisplay[1].image.color.b, 1 - ((fadeTime-2) / 2f));
                if (fadeTime >= 4)
                {
                    imagesToDisplay[1].image.color = new Color(imagesToDisplay[1].image.color.r, imagesToDisplay[1].image.color.g,
                        imagesToDisplay[1].image.color.b, 0f);
                    isFade = false;
                }
            }
        }
    }

    private IEnumerator ShowLoading()
    {
        loadImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        nextButton.interactable = true;
    }

    private IEnumerator PauseAfterIndex1(float pauseTime)
    {
        isSceneTransitionAllowed = false; // Disable scene transition during the pause
        nextButton.interactable = false; // Disable the nextButton during the pause

        yield return new WaitForSeconds(pauseTime);

        isSceneTransitionAllowed = true; // Re-enable scene transition after the pause
        nextButton.interactable = true; // Re-enable the nextButton after the pause

        StartTransitionToMaxBlur();
    }

    private void ShowImage(int index)
    {
        for (int i = 0; i < imagesToDisplay.Length; i++)
        {
            bool isActive = i == index;
            imagesToDisplay[i].image.gameObject.SetActive(isActive);

            if (isActive)
            {
                if (index == 1)
                {
                    isFade = true;
                    StartCoroutine(ShowLoading());
                    if (transitionCoroutine != null)
                    {
                        StopCoroutine(transitionCoroutine);
                    }
                    StartCoroutine(PauseAfterIndex1(transitionTimeIndex1 + pauseTimeAfterIndex1));
                    nextButton.interactable = true;
                }
                else if (index == 3)
                {
                    if (transitionCoroutine != null)
                    {
                        StopCoroutine(transitionCoroutine);
                    }

                    if (colorTransitionCoroutine != null)
                    {
                        StopCoroutine(colorTransitionCoroutine);
                    }

                    colorTransitionCoroutine = StartCoroutine(TransitionColorEffect(Color.black, Color.white, transitionTimeIndex3));
                }
                else
                {
                    if(index==4)
                    {
                        AudioManager.Instance.PlayBGM("Intro");
                    }

                    if (transitionCoroutine != null)
                    {
                        StopCoroutine(transitionCoroutine);
                    }

                }
            }
        }
    }

    private void StartTransitionToMaxBlur()
    {
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
        }
    }

    private IEnumerator TransitionColorEffect(Color startColor, Color targetColor, float transitionTime)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < transitionTime)
        {
            Color currentColor = Color.Lerp(startColor, targetColor, elapsedTime / transitionTime);
            imagesToDisplay[3].image.color = currentColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imagesToDisplay[3].image.color = targetColor;
    }

    public void ShowNextImage()
    {
        if (!isSceneTransitionAllowed || isTransitioning || !imagesToDisplay[currentImageIndex].useInTransition)
        {
            return;
        }

        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
        }

        if (colorTransitionCoroutine != null)
        {
            StopCoroutine(colorTransitionCoroutine);
        }
        
        if(currentImageIndex==imagesToDisplay.Length-1)
        {
            SceneManager.LoadScene("Tutorial");
        }
        currentImageIndex = (currentImageIndex + 1) % imagesToDisplay.Length;
        ShowImage(currentImageIndex);
    }
}
