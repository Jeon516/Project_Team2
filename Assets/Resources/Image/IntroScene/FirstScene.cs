using UnityEngine;
using UnityEngine.UI;
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
    public float blurStep = 1.0f;
    public float transitionTimeIndex1 = 5.0f;

    private int currentImageIndex = 0;
    private bool isTransitioning = false;
    private float currentBlurValue = 255.0f;

    private Coroutine transitionCoroutine;

    void Start()
    {
        ShowImage(currentImageIndex);

        blurMaterial = blurTargetImage.material;

        if (nextButton != null)
        {
            nextButton.onClick.AddListener(ShowNextImage);
            nextButton.interactable = true;
        }
    }

    private IEnumerator TransitionBlurEffect(float targetBlurValue, float transitionTime)
    {
        isTransitioning = true;
        float elapsedTime = 0.0f;
        float startBlurValue = currentBlurValue;

        while (elapsedTime < transitionTime)
        {
            currentBlurValue = Mathf.Lerp(startBlurValue, targetBlurValue, elapsedTime / transitionTime);
            blurMaterial.SetFloat("_Radius", currentBlurValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentBlurValue = targetBlurValue;
        blurMaterial.SetFloat("_Radius", currentBlurValue);
        isTransitioning = false;
    }

    private void ShowImage(int index)
    {
        for (int i = 0; i < imagesToDisplay.Length; i++)
        {
            imagesToDisplay[i].image.gameObject.SetActive(i == index);
        }
    }

    public void ShowNextImage()
    {
        if (isTransitioning || !imagesToDisplay[currentImageIndex].useInTransition)
        {
            return; // 이미지 전환 중이거나 전환에 사용되지 않는 이미지라면 무시
        }

        float targetBlurValue = 1.0f;
        float transitionTime = transitionTimeIndex1;

        if (currentImageIndex == 1 || currentImageIndex == 2)
        {
            targetBlurValue = 1.0f;
        }

        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
        }

        transitionCoroutine = StartCoroutine(TransitionBlurEffect(targetBlurValue, transitionTime));

        currentImageIndex = (currentImageIndex + 1) % imagesToDisplay.Length;
        ShowImage(currentImageIndex);
    }
}
