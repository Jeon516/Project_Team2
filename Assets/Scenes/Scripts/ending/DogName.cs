using UnityEngine;
using UnityEngine.UI;

public class DogName : MonoBehaviour
{
    public InputField inputField;
    public Button saveButton; // 인스펙터에서 버튼을 할당하기 위한 변수
    private ending_controller endcontrol;

    private void Start()
    {
        endcontrol = FindObjectOfType<ending_controller>();
        saveButton.onClick.AddListener(OnSaveButtonClick);
        saveButton.interactable = false;

        // 입력 필드의 "On Value Changed" 이벤트에 OnInputFieldValueChanged 메서드를 연결합니다.
        inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
    }

    public void OnInputFieldValueChanged(string text)
    {
        if (text.Length >= 1)
        {
            saveButton.interactable = true;
        }
        else
        {
            saveButton.interactable = false;
        }
    }

    public void OnSaveButtonClick()
    {
        string inputValue = inputField.text;
        PlayerPrefs.SetString("DogName", inputValue);
        PlayerPrefs.Save(); // 변경사항을 저장합니다.
        endcontrol.myButton.gameObject.SetActive(true);
        endcontrol.image5.gameObject.SetActive(false);
        endcontrol.image6.gameObject.SetActive(true);
    }
}
