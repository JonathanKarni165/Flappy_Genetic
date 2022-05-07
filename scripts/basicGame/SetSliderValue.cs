using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// helper class to set the slider values on screen
/// </summary>
public class SetSliderValue : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Slider slider;

    void Update()
    {
        text.text = slider.value.ToString("F2");
    }
}
