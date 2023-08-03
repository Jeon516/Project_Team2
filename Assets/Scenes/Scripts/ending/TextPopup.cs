using UnityEngine;
using UnityEngine.UI;

public class TextPopup : MonoBehaviour
{
    public Text displayText;

    private void Start()
    {
        string savedValue = PlayerPrefs.GetString("DogName");
        displayText.text = savedValue + "......?";
    }
}