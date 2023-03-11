// The codes in this .cs file is written by me
// The comments are written by me and showing my understanding of the codes in this .cs file

// This script was written to create a functioning UI

using UnityEngine;
using UnityEngine.UI; // the namespace of UI

public class TimeEnergyBar : MonoBehaviour
{
    // to set the values on the Time-Energy Bar (Tachyon Bar)
    [SerializeField] Slider slider;

    public void maxPlayerTEnergy(int energy)
    {
        slider.maxValue = energy;
        slider.value = energy;
    }

    public void SetEnergy(int energy)
    {
        slider.value = energy;
    }


}
