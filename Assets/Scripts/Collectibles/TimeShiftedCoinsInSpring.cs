// The codes in this .cs file is written by me
// The comments are written by me and showing my understanding of the codes in this .cs file

// This script was written to make the PickUpSystem more sophisticated in terms of visual

using UnityEngine;

public class TimeShiftedCoinsInSpring : MonoBehaviour
{
    Animator anim; // to reference the Animator
    Collider2D col; // to reference the 2D Collider

    [SerializeField] PlayerAttributes timeShifting; // to reference the PlayerAttributes.cs
    
    [SerializeField] RuntimeAnimatorController opaque; // to reference an Animator Controller
    [SerializeField] RuntimeAnimatorController transparent; // to reference an Animator Controller

    void Awake()
    {
        anim = GetComponent<Animator>(); // to get the Animator Component
        col = GetComponent<BoxCollider2D>(); // to get the 2D Box Collider Component
    }

    // some coins are not visible and pickable in Spring time
    void Update()
    {
        if (timeShifting.isSpring == true)
        {
            anim.runtimeAnimatorController = transparent;
            col.enabled = false;
        }
        else
        {
            anim.runtimeAnimatorController = opaque;
            col.enabled = true;
        }
    }
}
