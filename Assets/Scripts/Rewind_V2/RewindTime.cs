// This script is from "How to Create the Time Rewind in Braid?" by Fadrik
// https://www.youtube.com/watch?v=LVM2SD0Rne0

// I heavily modified and expanded the code

// The comments are written by me and showing my understanding of the codes in this .cs file

// See Development Diary for more details
// https://www.notion.so/Implementation-Rewind-Week-4-f2ecbd5364b6451a938657f790635c28

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewindTime : MonoBehaviour
{
    public bool isRewinding = false;
    List<TimePoint> TimePoints = new List<TimePoint>(); // a list that contains all saved points
    int TimePointsCount = 500; // maximum number of frames to be saved

    Rigidbody2D r2D; // to reference the Rigidbody

    Animator anim; // to access the Animator
    CharacterController2D cc2d; // to acces the Character Controller 2D

    [SerializeField] Image ui; // to reference the Rewind UI Icon

    [SerializeField] AudioSource aud; // to reference the Audio Source

    void Awake()
    {
        r2D = GetComponent<Rigidbody2D>(); // to get the Rigidbody Component
        anim = GetComponent<Animator>(); // to get the Animator Component
        cc2d = GetComponent<CharacterController2D>(); // to get the Character Controller 2D
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // to start Rewinding
            RewindController();

        if (Input.GetButtonUp("Fire1")) // to stop Rewinding
            StopRewinding();

        // to rewind player's Jump animation
        if (isRewinding == true && !cc2d.m_Grounded)
        {
            anim.SetBool("isJumping", true);
        }
    }

    // recording point as frames
    void FixedUpdate()
    {
        if (isRewinding)
            RewindTimePoints();
        else
            RecordTimePoints();
    }

    // recording points
    void RecordTimePoints()
    {
        if (TimePoints.Count >= TimePointsCount) // bugfix, so the player can't rewind time before frames were started to be stored
            TimePoints.RemoveAt(0);

        TimePoint timePoint = new TimePoint(transform.position, r2D.velocity); // to create a point
        TimePoints.Add(timePoint); // to add it to the list
    }

    // rewinding time
    void RewindTimePoints()
    {
        if (TimePoints.Count > 0) // bugfix, so the player can't rewind time before frames were started to be stored
        {
            SetTimePoint(TimePoints[TimePoints.Count - 1]); // to set the point to the Player GameObject
            TimePoints.RemoveAt(TimePoints.Count - 1);
        }
        else
            StopRewinding(); // when TimePoints is empty, stop Rewinding
    }

    // the function can be overloaded
    void SetTimePoint(TimePoint timePoint)
    {
        this.transform.position = timePoint.position;
        r2D.velocity = timePoint.velocity;
    }

    void RewindController()
    {
        isRewinding = true;
        ui.gameObject.SetActive(true); // to set the Pause UI Icon on
        aud.pitch = -1; // to rewind the BG music

        // to rewind Player animations
        if (isRewinding == true)
        {
            anim.SetBool("isRewinding", true);
        }
    }

    void StopRewinding()
    {
        isRewinding = false;
        ui.gameObject.SetActive(false); // to set the Pause UI Icon off

        // to stop rewinding Player animations
        if (isRewinding == false)
        {
            anim.SetBool("isRewinding", false);
            anim.Play("Player_Idle"); // bug fix
            aud.pitch = 1;
        }
    }
}