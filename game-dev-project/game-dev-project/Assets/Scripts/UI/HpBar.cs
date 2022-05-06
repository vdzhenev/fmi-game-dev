using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int val)
    {
        slider.maxValue = val;
        slider.value = val;
    }

    public void SetHealth(int val)
    {
        slider.value = val;
    }
}
