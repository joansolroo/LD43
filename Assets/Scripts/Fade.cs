using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class Fade : MonoBehaviour
{

    [SerializeField] Image FadeGround;
    [SerializeField] float fadeInTime = 1;
    [SerializeField] float fadeBlackTime = 1;
    [SerializeField] float fadeWhiteTime = 1;

    public bool faded = false;
    public bool fade = false;
    public bool white = false;
    private void Update()
    {/*
        if (!fading &&faded != fade)
        {
            if (fade) {
                if (white) FadeToWhite(fadeWhiteTime); else FadeToBlack(fadeBlackTime);
            }
            else FadeIn(fadeInTime);
            
        }*/
    }
    public float FadeIn(float fadeInTime)
    {
        Color color = FadeGround.color;
        color.a = 0;
        StartCoroutine(DoFade(color, fadeInTime));
        fade = false;
        faded = fade;
        return fadeInTime;
    }

    public float FadeToBlack(float fadeBlackTime)
    {
        Color color = new Color(0, 0, 0, 1);
        StartCoroutine(DoFade(color, fadeBlackTime));
        fade = true;
        faded = fade;
        return fadeBlackTime;
    }
    public float FadeToWhite(float fadeWhiteTime)
    {
        Color color = new Color(1, 1, 1, 1);
        StartCoroutine(DoFade(color, fadeWhiteTime));
        fade = true;
        faded = fade;
        return fadeWhiteTime;
    }
    bool fading = false;
    IEnumerator DoFade(Color fadeColor, float fadeTime)
    {
        if (!fading)
        {
            fading = true;
            float t = 0;
            while (t < fadeTime)
            {
                FadeGround.color = Color.Lerp(FadeGround.color, fadeColor, t / fadeTime);
                t += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            FadeGround.color = fadeColor;
            fading = false;
        }
    }
    IEnumerator DoBlink(Color fadeColor, float fadeTime)
    {
        if (!fading)
        {
            fading = true;
            float t = 0;
            while (t < fadeTime)
            {
                FadeGround.color = Color.Lerp(FadeGround.color, fadeColor, t / fadeTime);
                t += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            fading = false;
            FadeIn(fadeTime);
        }
    }
}
