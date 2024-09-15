using System.Collections;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;

    public float transitionTime = 1.0f;
    public Material material;
    bool inverted = false;

    const string ProgressProperty = "_Progress";

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
            float progress = Mathf.Clamp01(currentTime / transitionTime);
            material.SetFloat(ProgressProperty, inverted ? 1.0f - progress : progress);
            yield return null;
        }
    }

    public void FadeIn()
    {
        inverted = false;
        material.SetFloat(ProgressProperty, 0.0f);
        StartCoroutine(TransitionCoroutine());
    }

    public void FadeOut()
    {
        inverted = true;
        material.SetFloat(ProgressProperty, 1.0f);
        StartCoroutine(TransitionCoroutine());
    }
}
