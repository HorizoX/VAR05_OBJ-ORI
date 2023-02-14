using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartRace : MonoBehaviour
{
     // Public variables that allow us to assign UI elements to these variables in the Unity Inspector.
    public Button playerButton; 
    public Button AIButton;
    public TMP_Text welcomeText;
    public GameObject panel;
    public GameObject canvas;
    public GameObject mainCamera;
    // Private variable that holds the RaceScript component of the parent object.
    private RaceScript raceScript;

    void Start()
    {
        // Get the RaceScript component of the parent object.
        raceScript = GetComponentInParent<RaceScript>();
    }

    // Called when the player chooses to play as a human.
    public void ChoosePlayer()
    {
        // Hide the UI elements and activate the race script to start the race.
        canvas.SetActive(false);
        mainCamera.SetActive(true);
        welcomeText.gameObject.SetActive(false);
        playerButton.gameObject.SetActive(false);
        AIButton.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
        raceScript.StartRace(true);
    }

    // Called when the player chooses to play as an AI.
    public void ChooseAI()
    {
        // Hide the UI elements and activate the race script to start the race.
        canvas.SetActive(false);
        mainCamera.SetActive(true);
        welcomeText.gameObject.SetActive(false);
        playerButton.gameObject.SetActive(false);
        AIButton.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
        raceScript.StartRace(true);
    }
}



