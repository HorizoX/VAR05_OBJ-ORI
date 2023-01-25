using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composer : MonoBehaviour
{
    public AudioSource a4, a4Sharp;

    private List<AudioSource> notesInComposition = new List<AudioClip>();
    // Start is called before the first frame update
    private void Start()
    {
        notesInComposition.Add(a4);
        notesInComposition.Add(a4);
        notesInComposition.Add(a4Sharp);
        notesInComposition.Add(a4);
    }

    // Update is called once per frame
    private void Update()
    {
        if(Keyboard.current.digit0Key.wasPressedThisframe)
        {
            notesInComposition[0].Play();
        }

        if (Keyboard.current.digit1Key.wasPressedThisframe)
        {
            notesInComposition[2].Play();
        }

        if (Keyboard.current.aKey.wasPressedThisFrame)

            a4.Play();
    }
}
