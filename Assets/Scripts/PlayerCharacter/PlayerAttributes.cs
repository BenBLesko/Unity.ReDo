// This script is partly from "2D Movement in Unity (Tutorial)" by Brackeys
// https://www.youtube.com/watch?v=dwcT-Dch0bA&ab_channel=Brackeys

// I heavily modified and expanded the code as I didn't need it Crouch function but to implement the Pause Time and the Switch the Timeline mechanics; see Development Diary for more details
// https://www.notion.so/Week-5-a419d652000a4873b4a2d9b33c08f1b5

// The comments are written by me and showing my understanding of the codes in this .cs file

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour
{
    public CharacterController2D controller; // to access the CharacterController2D script

    [SerializeField] float movementSpeed = 40f; // the setting of the speed of the movement interaction
    private float horizontalMove = 0f; // the initial value for horizontal movements
    [SerializeField] float timeJump = 750; // how high the Player Character can jump with Time Jump (same as "normal" jump)
    private bool jump = false; // at Start, the Player Character is NOT jumping
    private bool isTimeJumping = false; // bugfixing the Time Jump

    private bool isPaused = false; // time is NOT paused on default
    GameObject[] spring; // an array for GameObjects
    GameObject[] autumn; // an array for GameObjects
    public bool isSpring; // a boolean to determine which "season" the Player Character in
    public bool isAutumn; // a boolean to determine which "season" the Player Character in

    [SerializeField] TimeEnergySystem timeEnergySystem; // to reference the Time-Energy System
    [SerializeField] TimeEnergyBar tachyonBar; // to reference the Time-Energy Bar (Tachyon Bar)
    [SerializeField] Image ui; // to reference the Pause UI Icon

    [SerializeField] AudioSource aud1; // to reference the Audio Source
    [SerializeField] AudioSource aud2; // to reference the Audio Source

    Rigidbody2D r2D; // to reference the Rigidbody

    public Animator animator; // to access the Animator

    void Start()
    {
        spring = GameObject.FindGameObjectsWithTag("Spring"); // this array contains GameObjects tagged with "Spring"
        autumn = GameObject.FindGameObjectsWithTag("Autumn"); // this array contains GameObjects tagged with "Autumn"
        isSpring = true; // at Start, the current "season" is "Spring"
        isAutumn = false; // at Start, the current "season" is NOT "Autumn"
        foreach (GameObject spring in spring)
        {
            spring.GetComponent<SpriteRenderer>().enabled = true;
            spring.GetComponent<BoxCollider2D>().enabled = true;
        }
        foreach (GameObject autumn in autumn)
        {
            autumn.GetComponent<SpriteRenderer>().enabled = false;
            autumn.GetComponent<BoxCollider2D>().enabled = false;
        }

        r2D = GetComponent<Rigidbody2D>(); // to get the Rigidbody Component
    }

    void Update()
    {
        // to exit the prototype
        if (Input.GetButtonDown("Exit"))
        {
            Application.Quit();
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * movementSpeed; // how the "horzintalMove" will be calculated
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove)); // to play the Run animation from Idle and vice versa

        // how "Jump" will be called upon
        if (Input.GetButtonDown("Jump") && !isPaused)
        {
            jump = true;
            animator.SetBool("isJumping", true); // to trigger the Jump animation
        }

        // how "Time Jump" will be called upon
        if (Input.GetButtonDown("Jump") && isPaused)
        {
            if (isTimeJumping == true)
            {

            }
            else
            {
                r2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                r2D.AddForce(new Vector2(0f, timeJump));
                isTimeJumping = true;
                animator.SetBool("isJumping", true); // to trigger the Jump animation
                aud2.Play(); // to play the Jump audio
            }

        }

        // "Q" pauses Time but only if the Player has enough Time-Energy (Tachyons)
        if (Input.GetButtonDown("Fire2") && timeEnergySystem.currentPlayerTEnergy > timeEnergySystem.marginPlayerTEnergy)
        {
            if (isPaused)
            {

            }
            else
            {
                TakeTachyonDamage(50);
                RegenerateTachyons();
                Pause();
            }

        }

        // how to "Time-Shift"
        if (Input.GetButtonDown("Fire3") && isSpring && timeEnergySystem.currentPlayerTEnergy > timeEnergySystem.marginPlayerTEnergy)
        {
            TakeTachyonDamage(50);
            RegenerateTachyons();

            {
                StartCoroutine(ShiftToSpringTime());
            }
            IEnumerator ShiftToSpringTime()
            {
                // if the boolean "isSpring" is true, the engine turns off the renders of the GameObjects (Sprites) that had been tagged with "Spring"
                foreach (GameObject spring in spring)
                {
                    spring.GetComponent<SpriteRenderer>().enabled = false;
                    spring.GetComponent<BoxCollider2D>().enabled = false;
                }
                // if the boolean "isSpring" is true, the engine turns on the renders of the GameObjects (Sprites) that had been tagged with "Autumn"
                foreach (GameObject autumn in autumn)
                {
                    autumn.GetComponent<SpriteRenderer>().enabled = true;
                    autumn.GetComponent<BoxCollider2D>().enabled = true;
                }

                // to set the Camera Background colour to match the Background
                Camera.main.GetComponent<Camera>().backgroundColor = new Color32(206, 211, 112, 0);

                yield return new WaitForSeconds(0.25f);

                // to set the two "season" booleans the opposite values
                isSpring = false;
                isAutumn = true;
            }

        }
        else if (Input.GetButtonDown("Fire3") && !isSpring && timeEnergySystem.currentPlayerTEnergy > timeEnergySystem.marginPlayerTEnergy)
        {
            TakeTachyonDamage(50);
            RegenerateTachyons();

            {
                StartCoroutine(ShiftToAutumnTime());
            }
            IEnumerator ShiftToAutumnTime()
            {
                // if the boolean "isSpring" is false, the engine turns on the renders of the GameObjects (Sprites) that had been tagged with "Spring"
                foreach (GameObject spring in spring)
                {
                    spring.GetComponent<SpriteRenderer>().enabled = true;
                    spring.GetComponent<BoxCollider2D>().enabled = true;
                }
                // if the boolean "isSpring" is false, the engine turns off the renders of the GameObjects (Sprites) that had been tagged with "Autumn"
                foreach (GameObject autumn in autumn)
                {
                    autumn.GetComponent<SpriteRenderer>().enabled = false;
                    autumn.GetComponent<BoxCollider2D>().enabled = false;
                }

                // to set the Camera Background colour to match the Background
                Camera.main.GetComponent<Camera>().backgroundColor = new Color32(112, 211, 112, 0);

                yield return new WaitForSeconds(0.25f);

                // to set the two "season" booleans the opposite values
                isSpring = true;
                isAutumn = false;
            }
        }
    }

    void FixedUpdate()
    {
        // the player movement and jump implementations
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
    
    // to stop jumping when the Player hits any ground
    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    // what happens when the player pauses Time
    void Pause()
    {
        {
            StartCoroutine(PauseTime());
        }

        IEnumerator PauseTime()
        {
            isPaused = true; // bugfixing
            r2D.constraints = RigidbodyConstraints2D.FreezeAll; // to freeze the Player Character on X and Y
            animator.speed = 0; // to freeze animations
            ui.gameObject.SetActive(true); // to set the Pause UI Icon on
            aud1.mute = true;

            yield return new WaitForSeconds(0.75f);

            isPaused = false; // bugfixing
            isTimeJumping = false; // a much needed bug fix that prevents the Player to infinitly Jump while the Time is paused
            r2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation; // to unfreeze the Player Character on X and Y, freeze its Z rotation
            animator.speed = 1; // to freeze animations
            ui.gameObject.SetActive(false); // to set the Pause UI Icon off
            aud1.mute = false;
        }
    }

    // every usage of a Time related ability costs 50 Tachyons
    public void TakeTachyonDamage(int tachyonDamage)
    {
        timeEnergySystem.currentPlayerTEnergy -= tachyonDamage;
        tachyonBar.SetEnergy(timeEnergySystem.currentPlayerTEnergy);

        if (timeEnergySystem.currentPlayerTEnergy < timeEnergySystem.minPlayerTEnergy)
        {
            timeEnergySystem.currentPlayerTEnergy = 0;
            tachyonBar.SetEnergy(timeEnergySystem.currentPlayerTEnergy);
        }
    }

    // after 3 seconds the engine sets the current Player's Time Energy to its maximum value
    void RegenerateTachyons()
    {
        StartCoroutine(addTachyons());

        IEnumerator addTachyons()
        {
            while (true)
                if (timeEnergySystem.currentPlayerTEnergy <= 50)
                {
                    yield return new WaitForSeconds(3);
                    timeEnergySystem.currentPlayerTEnergy = timeEnergySystem.maxPlayerTEnergy;
                    tachyonBar.SetEnergy(timeEnergySystem.currentPlayerTEnergy);
                }
                else
                {
                    yield return null;
                }
        }
    }
}
