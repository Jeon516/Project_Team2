using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InputDog : MonoBehaviour
{
    public InputField DogInputField; // 강아지 이름 받기

    private string DogName;

    private void Start()
    {
        DogInputField.onEndEdit.AddListener(OnPlayerNameEndEdit);
    }

    private void OnPlayerNameEndEdit(string input)
    {
        DogName = input;
    } 
}
