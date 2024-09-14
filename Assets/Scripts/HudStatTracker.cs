using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudStatTracker : MonoBehaviour
{
    public Slider statSlider;
    public TextMeshProUGUI statText;

    public void SetStatText(float current, float max)
    {
        // no decimal points
        statText.text = current.ToString("0") + "/" + max.ToString("0");
        statSlider.value = current / max;
    }
}
