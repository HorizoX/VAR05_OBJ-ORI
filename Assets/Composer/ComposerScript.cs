using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComposerScript : MonoBehaviour
{
    // List to store audio sources of the notes.
    private List<AudioSource> notesInComposition = new List<AudioSource>();

    // Audio sources of each note.
    public AudioSource cAudioSource, cSharpAudioSource, dAudioSource, dSharpAudioSource, eAudioSource, fAudioSource, fSharpAudioSource, gAudioSource, gSharpAudioSource, aAudioSource, aSharpAudioSource, bAudioSource;

    // Buttons for each note.
    public GameObject CButton, CSharpButton, DButton, DSharpButton, EButton, FButton, FSharpButton, GButton, GSharpButton, AButton, ASharpButton, BButton;

    // Play, Reset, and Remove buttons.
    public GameObject playButton;
    public GameObject resetButton;
    public GameObject removeButton;

    // Dropdown to display the list of added notes.
    public TMP_Dropdown noteDropdown;

    private void Start()
    {
        // Get the audio source component for each button.
        cAudioSource = CButton.GetComponent<AudioSource>();

        cSharpAudioSource = CSharpButton.GetComponent<AudioSource>();

        dAudioSource = DButton.GetComponent<AudioSource>();

        dSharpAudioSource = DSharpButton.GetComponent<AudioSource>();

        eAudioSource = EButton.GetComponent<AudioSource>();

        fAudioSource = FButton.GetComponent<AudioSource>();

        fSharpAudioSource = FSharpButton.GetComponent<AudioSource>();

        gAudioSource = GButton.GetComponent<AudioSource>();

        gSharpAudioSource = GSharpButton.GetComponent<AudioSource>();

        aAudioSource = AButton.GetComponent<AudioSource>();

        aSharpAudioSource = ASharpButton.GetComponent<AudioSource>();

        bAudioSource = BButton.GetComponent<AudioSource>();
    }

    // Add a note to the composition when a button is pressed.
    public void AddNote(AudioSource audioSource)
    {
        
            
            notesInComposition.Add(audioSource);

            
            audioSource.Play();

            // Add the name of the audio source to the dropdown options.
            noteDropdown.options.Add(new TMP_Dropdown.OptionData(audioSource.name));

        

      
        
    }

    // Remove a note from the composition.
    public void RemoveNote()
    {
        int selectedIndex = noteDropdown.value;

      
        notesInComposition.RemoveAt(selectedIndex);

      
        noteDropdown.options.RemoveAt(selectedIndex);
    }

    // Reset the composition to an empty list.
    public void ResetComposition()
    {
        notesInComposition.Clear();
    }

    // Play the composition using an IEnumerator.
    public IEnumerator PlayComposition()
    {
        
        foreach (AudioSource audioSource in notesInComposition)
        {
            audioSource.Play();

            
            yield return new WaitForSeconds(audioSource.clip.length);
        }
    }

    // This method adds an audio source to the 'notesInComposition' list.
    public void OnNoteButtonClick(AudioSource audioSource)
    {
        AddNote(audioSource);
    }




    
    //The following 12 methods adds their respective Audiosources to the 'notesInComposition' list.
    public void OnCButtonClick()
    {
        OnNoteButtonClick(cAudioSource);
    }

    public void OnCSharpButtonClick()
    {
        OnNoteButtonClick(cSharpAudioSource);
    }
    
    public void OnDButtonClick()
    {
        OnNoteButtonClick(dAudioSource);
    }
    
    public void OnDSharpButtonClick()
    {
        OnNoteButtonClick(dSharpAudioSource);
    }

    public void OnEButtonClick()
    {
        OnNoteButtonClick(eAudioSource);
    }

    public void OnFButtonClick()
    {
        OnNoteButtonClick(fAudioSource);
    }

    public void OnFSharpButtonClick()
    {
        OnNoteButtonClick(fSharpAudioSource);
    }

    public void OnGButtonClick()
    {
        OnNoteButtonClick(gAudioSource);
    }

    public void OnGSharpButtonClick()
    {
        OnNoteButtonClick(gSharpAudioSource);

    }

    public void OnAButtonClick()
    {
        OnNoteButtonClick(aAudioSource);
    }

    public void OnASharpButtonClick()
    { 
        OnNoteButtonClick(aSharpAudioSource);
    }

    public void OnBButtonClick()
    {
        OnNoteButtonClick(bAudioSource);
    }






    // This method resets the 'notesInComposition' list and clears the 'noteDropdown' options.
    public void OnResetButtonClick()
    {
        ResetComposition();
        noteDropdown.ClearOptions();

    }

  
    public void OnPlayButtonClick()
    {
        StartCoroutine(PlayComposition());
    }





}