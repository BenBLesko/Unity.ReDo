// The codes in this .cs file is written by me
// The comments are written by me and showing my understanding of the codes in this .cs file

// This script is for showing On-screen Tutorials

// See Development Diary for more details
// https://www.notion.so/Week-10-Implementations-5e6372624629409ca8f77ea16d01818a

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPrompts : MonoBehaviour
{
    [SerializeField] GameObject P1; // to reference the Player
    public Text prompt; // to reference the Tutorial Prompt Texts

    // if the Player collides with the invisible wall, the tutorial prompt shows for 5 seconds then it gets destroyed
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == P1)
        {
            StartCoroutine(tutorialScreen());

            IEnumerator tutorialScreen()
            {
                prompt.gameObject.SetActive(true);

                yield return new WaitForSeconds(3.75f);

                prompt.gameObject.SetActive(false);
                Destroy(this);
            }
        }
    }
}
