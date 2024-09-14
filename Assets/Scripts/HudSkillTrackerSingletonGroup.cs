using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudSkillTrackerSingletonGroup : MonoBehaviour
{
    public static HudSkillTrackerSingletonGroup instance;

    public HudSkillTracker primarySkillTracker;
    public HudSkillTracker secondarySkillTracker;
    public HudSkillTracker dashSkillTracker;

    void Awake()
    {
        instance = this;
    }
}
