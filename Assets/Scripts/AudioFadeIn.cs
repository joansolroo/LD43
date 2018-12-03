using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioFadeIn : MonoBehaviour {
    AudioSource source;
    [SerializeField] float delay;
    [SerializeField] float time;
    [SerializeField] AnimationCurve fade;
    // Use this for initialization
    void Start () {

        source = GetComponent<AudioSource>();
        source.volume = 0;
        StartCoroutine(DoFadeIn(time,delay));
    }
	
    IEnumerator DoFadeIn(float time, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        source.Play();
        float t = 0;
        while (t < time)
        {

            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            source.volume = fade.Evaluate(t / time);
        }
    }
}
