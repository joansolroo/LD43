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
    {
        if (!fading &&faded != fade)
        {
            if (fade) {
                if (white) FadeToWhite(); else FadeToBlack();
            }
            else FadeIn();
        }
    }
    public void FadeIn()
    {
        Color color = FadeGround.color;
        color.a = 0;
        StartCoroutine(DoFade(color, fadeInTime));
        faded = fade = false;
    }

    public void FadeToBlack()
    {
        Color color = new Color(0, 0, 0, 1);
        StartCoroutine(DoFade(color, fadeBlackTime));
        faded = fade = true;
    }
    public void FadeToWhite()
    {
        Color color = new Color(1, 1, 1, 1);
        StartCoroutine(DoFade(color, fadeWhiteTime));
        faded = fade = true;
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
            FadeIn();
        }
    }
}
