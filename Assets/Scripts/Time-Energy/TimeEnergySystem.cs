// The codes in this .cs file is written by me
// The comments are written by me and showing my understanding of the codes in this .cs file

// See Development Diary for more details
// https://www.notion.so/Week-9-Implementations-and-Iterations-079156f850db4f85a0209b081075d009

using System.Collections;
using UnityEngine;

public class TimeEnergySystem : MonoBehaviour
{
    public int maxPlayerTEnergy = 100; // the maximum value of Tachyons
    public int minPlayerTEnergy = 0; // the minimum value of Tachyons
    public int marginPlayerTEnergy; // a margin is used to limit the number of times the Player can use the various Time abilities
    public int currentPlayerTEnergy; // the current value of Tachyons

    [SerializeField] TimeEnergyBar tachyonBar; // to reference the Time-Energy Bar (Tachyon Bar)

    void Start()
    {
        marginPlayerTEnergy = 49; // the margin is 49
        currentPlayerTEnergy = maxPlayerTEnergy; // the Player starts with the maximum value of Tachyons
        tachyonBar.SetEnergy(currentPlayerTEnergy); // the Tachyon Bar is set to the current value of Tachyons
    }
}
