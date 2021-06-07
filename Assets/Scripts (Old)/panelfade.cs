using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panelfade : MonoBehaviour
{

    public CanvasGroup fadescreen;
    public float fadeinspeed = 2;
    public float fadeoutspeed = 1;


    void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadingIn());
    }

    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadingOut());
    }

    IEnumerator FadingIn()
    {
        fadescreen.alpha = 1;
        for (int fs = 0; fs < (1000 * fadeinspeed); fs++)
        {
            fadescreen.alpha = 1f-(float)fs/(fadeinspeed * 50f);
            yield return new WaitForSecondsRealtime(0.0001f);
        }
    }

    IEnumerator FadingOut()
    {
        fadescreen.alpha = 0;
        for (int fs = 0; fs < (1000 * fadeoutspeed); fs++)
        {
            fadescreen.alpha = (float)fs / (fadeoutspeed * 50f);
            yield return new WaitForSecondsRealtime(0.0001f);
        }
    }
}
