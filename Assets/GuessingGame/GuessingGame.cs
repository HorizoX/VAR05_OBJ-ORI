using TMPro;
using UnityEngine;


// public - ???
// class - ???
// GuessingGame - Name of the *script*
// :
// MonoBehaviour - Indicates this file is a C# script.
public class GuessingGame : MonoBehaviour
{
    // A reference to the Text object
    public TextMeshProUGUI textGameObject;
    public TMP_InputField inputUpperBound;
    public int upperint = Int32.Parse(inputUpperBound.Text);
    public TMP_InputField inputLowerBound;
    public int lowerint - Int32.Parse(inputLowerBound.Text);
    public TMP_InputField inputAnswer;
    public int answerint = Int32.Parse(inputAnswer.Text);
    public int guessNumber = rnd.Next(lowerint, upperint);

    // public - indicates Unity can refer to this function in the editor,
    // such as to trigger when a button is pressed.
    public void MyFunction()
    {
        textGameObject.text = "Hello!\nPlease input a lower bound and upper bound number below. Once finished please press the button to start the Game!";
        if (inputAnswer.text != guessNumber)
        {
            if (answerint > guessNumber)
            {

                textGameObject.text = "The number you have chosen is too high. Please guess again!";



            } 
            
            else
            {

                textGameObject.text = "The number you have chosen is too low. Please guess again!";

            }


        }
        else
        {

            textGameObject.text = "Congratulations! You have gussed the correct number! You Win!";

        }
        // Function call to print to the console.
        // Debug.Log(inputFieldGameObject.text);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
