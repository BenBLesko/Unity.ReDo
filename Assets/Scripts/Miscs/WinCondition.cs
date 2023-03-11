// The codes in this .cs file is written by me
// The comments are written by me and showing my understanding of the codes in this .cs file

// This script gives the protoype a Win Condition

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    [SerializeField] GameObject P1; // to reference the Player

    [SerializeField] PickUpSystem pickUpSystem; // to reference the PickUpSystem.cs

    [SerializeField] Text won01; // to reference the Win Condition Prompt Texts
    [SerializeField] Text won02; // to reference the Win Condition Prompt Texts
    [SerializeField] Text lose; // to reference the Win Condition Prompt Texts

    // if the Player collected all 18 coins, the scene restarts as the prototype itself is a timeloop
    // otherwise the Player stuck in the current loop
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == P1 && other.gameObject == pickUpSystem.allCoinsCollected)
        {
            StartCoroutine(winScreen());

            IEnumerator winScreen()
            {
                won01.gameObject.SetActive(true);
                P1.GetComponent<CharacterController2D>().enabled = false;
                P1.GetComponent<PlayerAttributes>().enabled = false;
                P1.GetComponent<RewindTime>().enabled = false;
                P1.GetComponent<Animator>().Play("Player_Idle"); // bug fix

                yield return new WaitForSeconds(3f);

                won01.gameObject.SetActive(false);
                won02.gameObject.SetActive(true);

                yield return new WaitForSeconds(3f);

                won02.gameObject.SetActive(false);

                SceneManager.LoadScene("Prototype_Level");
            }
        }
        else if (other.gameObject == P1 && !other.gameObject == pickUpSystem.allCoinsCollected)
        {
            StartCoroutine(notWinScreen());

            IEnumerator notWinScreen()
            {
                lose.gameObject.SetActive(true);

                yield return new WaitForSeconds(3f);

                lose.gameObject.SetActive(false);
            }
        }
    }
}
