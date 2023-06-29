using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InputDog : MonoBehaviour
{
    public InputField DogInputField; // ������ �̸� �ޱ�

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
