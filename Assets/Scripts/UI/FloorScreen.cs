using System.Collections;
using TMPro;
using UndeadAssault;
using UnityEngine;
using UnityEngine.UI;

public class FloorScreen : MonoBehaviour
{
    public static FloorScreen instance;
    public float transitionTime = 0.5f;
    bool inverted = false;

    // public Image background;
    public TMP_Text text;

    void Awake()
    {
        instance = this;
    }

    private IEnumerator TransitionCoroutine()
    {
        float currentTime = 0.0f;
        while (currentTime < transitionTime)
        {
            currentTime += Time.deltaTime;
            float progress = inverted ? 1.0f - Mathf.Clamp01(currentTime / transitionTime) : Mathf.Clamp01(currentTime / transitionTime);
            // background.color = new Color(0, 0, 0, progress);
            text.color = new Color(1, 1, 1, progress);
            yield return null;
        }
    }

    void Start()
    {
        // background.color = new Color(0, 0, 0, 0);
        text.color = new Color(1, 1, 1, 0);
    }

    public void FadeIn()
    {
        inverted = false;
        // background.color = new Color(0, 0, 0, 0);
        text.color = new Color(1, 1, 1, 0);
        text.text = "Floor " + Gamemode.instance.floor;
        StartCoroutine(TransitionCoroutine());
    }

    public void FadeOut()
    {
        inverted = true;
        StartCoroutine(TransitionCoroutine());
    }
}
