using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudSkillTracker : MonoBehaviour
{
    public Image cooldownRadialImage;
    public TextMeshProUGUI textMeshProUGUI;

    public void SetCooldown(float timeout, float cooldown)
    {
        cooldownRadialImage.fillAmount = timeout / cooldown;
        if (timeout <= 0)
            textMeshProUGUI.text = "";
        else
        {
            textMeshProUGUI.text = timeout.ToString("0.0");
        }
    }
}
