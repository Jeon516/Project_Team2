using UnityEngine;
using UnityEngine.UI;

public class RandomImageUpbringing : MonoBehaviour
{
    public string resourcePath = "Image/BackGround/Upbringing";
    public float alpha = 1.0f;

    private Sprite[] backgrounds;
    private Image imageComponent;

    private void Awake()
    {
        imageComponent = GetComponent<Image>();
        backgrounds = Resources.LoadAll<Sprite>(resourcePath);
    }

    private void OnEnable()
    {
        SetRandomBackground();
    }

    public void SetRandomBackground()
    {
        if (backgrounds.Length > 0 && imageComponent != null)
        {
            int randomIndex = Random.Range(0, backgrounds.Length);
            imageComponent.sprite = backgrounds[randomIndex];

            Color imageColor = imageComponent.color;
            imageColor.a = alpha;
            imageComponent.color = imageColor;
        }
    }
}
