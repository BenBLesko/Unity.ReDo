// This script is from "How to Create the Time Rewind in Braid?" by Fadrik
// https://www.youtube.com/watch?v=LVM2SD0Rne0

// The comments are written by me and showing my understanding of the codes in this .cs file

// See Development Diary for more details
// https://www.notion.so/Implementation-Rewind-Week-4-f2ecbd5364b6451a938657f790635c28

using UnityEngine;

public class TimePoint : MonoBehaviour
{

    // variables that need to be recorded
    public Vector3 position;
    public Vector3 velocity;

    // the constructer
    public TimePoint(Vector3 position, Vector3 velocity)
    {
        this.position = position;
        this.velocity = velocity;
    }
}