using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class ComposerScript : MonoBehaviour
{
   private List<AudioSource> notesInComposition = new List<AudioSource>();
    public AudioSource cAudioSource, cSharpAudioSource, dAudioSource, dSharpAudioSource, eAudioSource, fAudioSource, fSharpAudioSource, gAudioSource, gSharpAudioSource, aAudioSource, aSharpAudioSource, bAudioSource;
    public GameObject CButton, CSharpButton, DButton, DSharpButton, EButton, FButton, FSharpButton, GButton, GSharpButton, AButton, ASharpButton, BButton;
    public GameObject playButton;
    public GameObject resetButton;

    private void Start()
    {
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
    // Add a note to the composition when a button is pressed
    public void AddNote(AudioSource audioSource)
    {
        if (audioSource != null)
        {

            notesInComposition.Add(audioSource);
            audioSource.Play();

        }

        else
        {
            Debug.LogError("Audio source is null");
        }
        
    }

    // Play the composition using an IEnumerator
    public IEnumerator PlayComposition()
    {
        foreach (AudioSource audioSource in notesInComposition)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
        }
    }

    public void OnNoteButtonClick(AudioSource audioSource)
    {
        AddNote(audioSource);
    }
    
    
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


    public void OnPlayButtonClick()
    {
        StartCoroutine(PlayComposition());
    }





}