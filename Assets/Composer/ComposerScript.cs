//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.InputSystem;
//using UnityEngine;
//using TMPro;

//public class ComposerScript : MonoBehaviour
//{
//    // Start is called before the first frame update

//    public AudioSource c4, c4Sharp, d4, d4Sharp, e4, f4, f4Sharp, g4, g4Sharp, a4, a4Sharp, b4; 

//    private List<AudioSource> notesInComposition = new List<AudioSource>();

//    public TMP_InputField note;

//    private IEnumerator PlaySong()
//    {
//        notesInComposition.Add(a4);
//        notesInComposition.Add(a4);
//        notesInComposition.Add(a4Sharp);
//        notesInComposition.Add(a4);

//        //Debug.Log(notesInComposition[0].name);

//        for (int i = 0; i < notesInComposition.Count; i++)
//        {
//            notesInComposition[i].Play();

//            // Delay until the note is done playing.
//            while (notesInComposition[i].isPlaying == true)
//            {
//                // This waits one frame IF AND ONLY IF this function is a "coroutine".
//                yield return null;
//            }

//             //Debug.Log(notesInComposition[i].name);
//        }
//    }

//        private void Start()
//    {

//        StartCoroutine(PlaySong());
//        //audiosource = GetComponent<AudioSource>();
//    }

























//    // Update is called once per frame
//    private void Update()
//    {
//        if (Keyboard.current.digit0Key.wasPressedThisFrame)
//        {
//            notesInComposition[0].Play();
//        }

//        if (Keyboard.current.digit1Key.wasPressedThisFrame)
//        {
//            // a4Sharp.Play();
//            // Grab the third "element" in the list, and play it.
//            notesInComposition[2].Play();
//        }
//    }
//}
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComposerScript : MonoBehaviour
{
    public AudioSource c4, c4Sharp, d4, d4Sharp, e4, f4, f4Sharp, g4, g4Sharp, a4, a4Sharp, b4;

    private List<AudioSource> notesInComposition = new List<AudioSource>();

    // Functions with "IEnumerator" are coroutines! They need to be started in a special way.
    private IEnumerator PlaySong()
    {
        notesInComposition.Add(a4);
        notesInComposition.Add(a4Sharp);
        notesInComposition.Add(b4);
        notesInComposition.Add(c4);
        notesInComposition.Add(c4Sharp);
        notesInComposition.Add(d4);
        notesInComposition.Add(d4Sharp);
        notesInComposition.Add(e4);
        notesInComposition.Add(f4);
        notesInComposition.Add(f4Sharp);
        notesInComposition.Add(g4);
        notesInComposition.Add(g4Sharp);



        // Debug.Log(notesInComposition[0].name);

        for (int i = 0; i < notesInComposition.Count; i++)
        {
            notesInComposition[i].Play();

            // Delay until the note is done playing.
            while (notesInComposition[i].isPlaying == true)
            {
                // This waits one frame IF AND ONLY IF this function is a "coroutine".
                yield return null;
            }

            // Debug.Log(notesInComposition[i].name);
        }
    }

    // How do we play an AudioClip through code?
    private void Start()
    {
        StartCoroutine(PlaySong());

        // Try to play a sound on in here.
        // a4Sharp.Play();       

        //Debug.Log(0);
        //Debug.Log(1);
        //Debug.Log(2);
        //Debug.Log(3);
        //Debug.Log(4);
        //Debug.Log(5);
        //Debug.Log(6);

        // i++ is the same as i += 1.
        // i-- is the same as i -= 1.

        // 2 to 6.
        //for (int i = 2; i < 7; i += 1)
        //{
        //    Debug.Log(i);
        //}

        //// 2 to 12, skipping odd numbers.
        //for (int i = 2; i < 13; i += 2)
        //{
        //    Debug.Log(i);
        //}

        //// 10 to 0, descending.
        //for (int i = 10; i >= 0; i -= 1)
        //{
        //    Debug.Log(i);
        //}

        //// 2 to 12, but DON'T print 5 or 10.
        //for (int i = 2; i < 13; i++)
        //{
        //    if (i == 5)
        //    {
        //        // continue immediately skips to the next "iteration"
        //        // of the loop, i.e., skipping any remaining code and coming
        //        // back to the top of.
        //        continue;
        //    }
        //    else if (i == 10)
        //    {
        //        continue;
        //    }
        //    else
        //    {
        //        Debug.Log(i);
        //    }
        //}


    }

    private void Update()
    {
        if (Keyboard.current.digit0Key.wasPressedThisFrame)
        {
            notesInComposition[0].Play();
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            // a4Sharp.Play();
            // Grab the third "element" in the list, and play it.
            notesInComposition[2].Play();
        }
    }
}

