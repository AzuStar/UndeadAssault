using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudStatTrackerSingletonGroup : MonoBehaviour
{
    public static HudStatTrackerSingletonGroup instance;

    public HudStatTracker healthTracker;
    public HudStatTracker experienceTracker;

    void Awake()
    {
        instance = this;
    }
}
