// The codes in this .cs file is written by me
// The comments are written by me and showing my understanding of the codes in this .cs file

// This script is for GameObjects that can hurt the Player

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDmg : MonoBehaviour
{
    [SerializeField] GameObject P1; // to reference the Player
    
    [SerializeField] RawImage dmg; // to referece the damage screen effect Raw Image
    [SerializeField] Text prompt; // to reference the Button Prompt Text
    [SerializeField] TutorialPrompts tutotiralPrompts; // to reference the TutorialPrompts.cs

    [SerializeField] bool isDead;

    [SerializeField] PhysicsMaterial2D Platform; // to reference a Physic Material
    [SerializeField] PhysicsMaterial2D Hazard; // to reference a Physic Material

    [SerializeField] AudioSource aud;

    void OnCollisionEnter2D(Collision2D col)
    {
        // if the Player Character dies, he can only do a Rewind action
        if (col.gameObject == P1)
        {
            isDead = true;
            aud.Play();
            P1.GetComponent<Animator>().enabled = false;
            P1.GetComponent<PlayerAttributes>().enabled = false;
            P1.GetComponent<BoxCollider2D>().sharedMaterial = Hazard;
            P1.GetComponent<CircleCollider2D>().sharedMaterial = Hazard;

            // the screen goes red when the Player dies
            StartCoroutine(dmgScreen());

            IEnumerator dmgScreen()
            {
                dmg.color = new Color(dmg.color.r, dmg.color.g, dmg.color.b, 0.125f);
                
                yield return new WaitForSeconds(0.05f);

                dmg.color = new Color(dmg.color.r, dmg.color.g, dmg.color.b, 0.25f);

                yield return new WaitForSeconds(0.05f);

                dmg.color = new Color(dmg.color.r, dmg.color.g, dmg.color.b, 0.50f);
                
                Time.timeScale = 0; // to stop time; so it doesn't store more frames to rewind upon death

            }

            // to activate the Button Prompt
            prompt.gameObject.SetActive(true);
        }
    }

    
    void Update()
    {
        // by hitting the Rewind key, time starts to flow once again
        if (Input.GetButtonDown("Fire1"))
        {
            Time.timeScale = 1;
            isDead = false;
        }

        // if the Player is dead and there is a Tutorial Prompt on Screen, the Tutorial Prompt turns off
        if (isDead && tutotiralPrompts.prompt)
        {
            tutotiralPrompts.prompt.gameObject.SetActive(false);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (P1.GetComponent<RewindTime>().isRewinding == true)
        {
            P1.GetComponent<Animator>().enabled = true;
            P1.GetComponent<PlayerAttributes>().enabled = true;
            P1.GetComponent<BoxCollider2D>().sharedMaterial = Platform;
            P1.GetComponent<CircleCollider2D>().sharedMaterial = Platform;

            // the screen goes back to normal
            StartCoroutine(dmgScreen());

            IEnumerator dmgScreen()
            {
                dmg.color = new Color(dmg.color.r, dmg.color.g, dmg.color.b, 0.50f);

                yield return new WaitForSeconds(0.05f);

                dmg.color = new Color(dmg.color.r, dmg.color.g, dmg.color.b, 0.25f);

                yield return new WaitForSeconds(0.05f);

                dmg.color = new Color(dmg.color.r, dmg.color.g, dmg.color.b, 0.125f);

                yield return new WaitForSeconds(0.05f);

                dmg.color = new Color(dmg.color.r, dmg.color.g, dmg.color.b, 0f);

            }

            // to deactivate the Button Prompt
            prompt.gameObject.SetActive(false);
        }
    }
}
