using TMPro;
using UnityEngine;


// public - ???
// class - ???
// GuessingGame - Name of the *script*
// :
// MonoBehaviour - Indicates this file is a C# script.
public class GuessingGame : MonoBehaviour
{
    public TextMeshProUGUI outputText;
    public TMP_InputField inputField;

    public GameObject button;

    private int selectedNumber = 0;

    private void Start()
    {
        outputText.text = "Please enter an upper bound integer for the guessing game!";
    }

    // This will be called when the Submit button is pressed.
    public void OnSubmit()
    {
        // int: the TYPE
        // myVariable: a name (anything, pretty much)
        // = 5: assign its value.
        // int myVariable = 5;

        // Strong typing in C# prevents this!
        // int myOtherVariable = "Erik";

        // var myOtherVariable = Something();

        int submittedNumber = int.Parse(inputField.text);

        // If my selected number is equal to zero,
        // i.e., if I have NOT yet selected a number.
        if (selectedNumber == 0)
        {
            selectedNumber = Random.Range(1, submittedNumber + 1);

            outputText.text = $"Guess a number between 1 and {submittedNumber}!";

            Debug.Log(selectedNumber);
        }
        // Otherwise, perform the guessing.
        else
        {
            if (submittedNumber > selectedNumber)
            {
                // Too high!
                outputText.text = "Too high!";
            }
            else if (submittedNumber < selectedNumber)
            {
                // Too low!
                outputText.text = "Too low!";
            }
            else
            {
                outputText.text = "You got it!!";

                // Same as ticking the little tickmark next to the game object's name.
                button.gameObject.SetActive(false);
                inputField.gameObject.SetActive(false);
            }
        }

        inputField.text = "";
    }
}
