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
    public Image blurTargetImage;
    public Material blurMaterial;
    public float transitionTimeIndex1 = 2.0f;
    public float maxBlurRadius = 255.0f;
    public float minBlurRadius = 0.0f;
    public float pauseTimeAfterIndex1 = 3.0f;
    public float transitionTimeIndex3 = 5.0f;
    public Image loadImage;

    private int currentImageIndex = 0;
    private bool isTransitioning = false;
    private bool isSceneTransitionAllowed = true;
    private Coroutine transitionCoroutine;
    private Coroutine colorTransitionCoroutine;


    void Start()
    {
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlaySFX("CarCrash");

        StartCoroutine(ShowLoading());
        ShowImage(currentImageIndex);

        blurMaterial = new Material(blurMaterial);
        blurTargetImage.material = blurMaterial;

        if (nextButton != null)
        {
            nextButton.onClick.AddListener(ShowNextImage);
            nextButton.interactable = true;
        }
    }

    private IEnumerator ShowLoading()
    {
        for (int r = 0; r < 600; r++)
        {
            ResetLoading();

            loadImage.gameObject.SetActive(true);
            loadImage.canvasRenderer.SetAlpha(0f);
            loadImage.CrossFadeAlpha(1f, 1f, true);
            yield return new WaitForSeconds(3f);

            float currentFadeOutTime = 0f;

            while (currentFadeOutTime < 2f)
            {
                float deltaTime = Time.deltaTime;
                currentFadeOutTime += deltaTime;

                float fadeAmount = Mathf.Lerp(1f, 0f, currentFadeOutTime /2f);
                loadImage.canvasRenderer.SetAlpha(fadeAmount);

                yield return null;
            }

            yield return new WaitForSeconds(1f);

            loadImage.gameObject.SetActive(false);
        }
    }

    private void ResetLoading()
    {
         loadImage.canvasRenderer.SetAlpha(1f);
    }

    private IEnumerator TransitionBlurEffect(float startBlurValue, float targetBlurValue, float transitionTime)
    {
        isTransitioning = true;
        float elapsedTime = 0.0f;

        while (elapsedTime < transitionTime)
        {
            float currentBlurValue = Mathf.Lerp(startBlurValue, targetBlurValue, elapsedTime / transitionTime);
            blurMaterial.SetFloat("_Radius", currentBlurValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        blurMaterial.SetFloat("_Radius", targetBlurValue);
        isTransitioning = false;
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
                    if (transitionCoroutine != null)
                    {
                        StopCoroutine(transitionCoroutine);
                    }

                    transitionCoroutine = StartCoroutine(TransitionBlurEffect(maxBlurRadius, minBlurRadius, transitionTimeIndex1));
                    StartCoroutine(PauseAfterIndex1(transitionTimeIndex1 + pauseTimeAfterIndex1));
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

                    transitionCoroutine = StartCoroutine(TransitionBlurEffect(maxBlurRadius, minBlurRadius, transitionTimeIndex3));
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

                    blurMaterial.SetFloat("_Radius", minBlurRadius);
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

        transitionCoroutine = StartCoroutine(TransitionBlurEffect(minBlurRadius, maxBlurRadius, transitionTimeIndex1));
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
