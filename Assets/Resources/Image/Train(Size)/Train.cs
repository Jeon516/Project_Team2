using UnityEngine;
using UnityEngine.UI;

public class Train : MonoBehaviour
{
    public GameObject Left_Object;
    public GameObject Right_Object;

    public Image Left_Image;
    public Image Right_Image;

    private void Start()
    {
        // 이미지를 오브젝트에 할당
        AssignImageToObject(Left_Object, Left_Image, "Image/Train(Size)/LeftSide");
        AssignImageToObject(Right_Object, Right_Image, "Image/Train(Size)/RightSide");
    }

    private void AssignImageToObject(GameObject obj, Image uiImage, string folderPath)
    {
        // 상대 경로로 폴더의 경로 구성
        string path = folderPath;

        // 폴더에 있는 모든 이미지 파일 로드
        Object[] loadedImages = Resources.LoadAll(path, typeof(Texture2D));

        if (loadedImages.Length == 0)
        {
            Debug.LogError("No images found in folder: " + folderPath);
            return;
        }

        // 랜덤한 이미지 선택
        int randomIndex = Random.Range(0, loadedImages.Length);
        Texture2D selectedImage = (Texture2D)loadedImages[randomIndex];

        // 이미지를 Sprite로 변환
        Sprite imageSprite = Sprite.Create(selectedImage, new Rect(0, 0, selectedImage.width, selectedImage.height), Vector2.one * 0.5f);

        // 이미지를 UI 요소에 할당
        if (uiImage != null)
        {
            uiImage.sprite = imageSprite;
        }
        else
        {
            Debug.LogError("UI Image component not found.");
        }

        // 이미지를 오브젝트의 Sprite Renderer에 할당
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = imageSprite;
        }
        else
        {
            Debug.LogError("Sprite Renderer component not found on object: " + obj.name);
        }
    }
}
