using UnityEngine;

public class VerificationScript : MonoBehaviour
{
    private void Start()
    {
        // Getting values from EmblemLoader and TextLoader
        string emblemValue = GetComponent<EmblemLoader>().emblemImage.sprite.name;
        string textValue = GetComponent<TextLoader>().displayText.text;
        string randomColor3 = GetComponent<GameProcess>().GetRandomColor3();

        // Check if all three values form a valid set
        bool isValidSet = CheckValidSet(emblemValue, textValue, randomColor3);

        // Log the result
        if (isValidSet)
        {
            Debug.Log("Correct set!");
        }
        else
        {
            Debug.Log("Not a valid set");
        }
    }

    private bool CheckValidSet(string emblemValue, string textValue, string randomColor3)
    {
        // Implement the logic to check if the three values form a valid set here
        // For example, you can compare emblemValue, textValue, and randomColor3 to certain conditions
        // and return true if they are valid, otherwise return false.

        // For demonstration purposes, let's assume that the set is valid if all three values are non-empty strings.
        return !string.IsNullOrEmpty(emblemValue) && !string.IsNullOrEmpty(textValue) && !string.IsNullOrEmpty(randomColor3);
    }
}
