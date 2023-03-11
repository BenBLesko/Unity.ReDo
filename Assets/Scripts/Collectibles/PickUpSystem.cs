// This is a script from "Collect & Count Items | Build a 2D Platformer Game in Unity #7" by Coding in Flow
// https://www.youtube.com/watch?v=pXn9icmreXE&ab_channel=CodinginFlow

// The comments are written by me as I had to heavily modify the original code to accomodate my needs

// See Development Diary for more details
// https://www.notion.so/Week-9-Implementations-and-Iterations-079156f850db4f85a0209b081075d009

using UnityEngine;
using UnityEngine.UI;

public class PickUpSystem : MonoBehaviour
{
    public int collectedCoins = 0; // the value of coins that have been collected; at the Start, it is 0
    public bool allCoinsCollected; // boolean for the WinCondition

    [SerializeField] private Text collectedCoinsText; // to reference the Coins UI

    [SerializeField] private AudioSource sfxPickUp; // to reference the Audio Source

    void OnTriggerEnter2D(Collider2D collision)
    {
        // if the Player collides with a Coin while its Sprite Renderer is true AND the Player IS NOT rewinding time, that Coin's Sprite Renderer turns off and the Engine adds +1 to the UI and plays an SFX
        if (collision.gameObject.CompareTag("Coins") && collision.gameObject.GetComponent<SpriteRenderer>().enabled == true && !gameObject.GetComponent<RewindTime>().isRewinding)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collectedCoins++;
            collectedCoinsText.text = "x  " + collectedCoins;
            sfxPickUp.Play();
        }

        // if the Player collides with an "invisible" Coin while he/she rewinds time, that Coin's Sprite Renderer turns on and the Engine adds -1 to the UI
        if (collision.gameObject.CompareTag("Coins") && collision.gameObject.GetComponent<SpriteRenderer>().enabled == false && gameObject.GetComponent<RewindTime>().isRewinding)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            collectedCoins--;
            collectedCoinsText.text = "x  " + collectedCoins;
        }
    }

    // if all 18 coins have been collected, the allCoinsCollected boolean is true, otherwise it is false
    void Update()
    {
        if (collectedCoins == 18)
        {
            allCoinsCollected = true;
        }
        else
        {
            allCoinsCollected = false;
        }
    }
}
