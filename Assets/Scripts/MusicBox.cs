using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicBox : MonoBehaviour
{
    ParticleSystem system;
    AudioSource source;
    [SerializeField] AudioClip[] notes;
    [SerializeField] float speed = 60;
    [SerializeField] float volume = 0.5f;
    [SerializeField] int emissionRate = 1;
    // Use this for initialization
    void Start()
    {
        system = GetComponent<ParticleSystem>();
        source = GetComponent<AudioSource>();
        t = 0;
        note = 0;
    }

    float t = 0;
    int note = 0;
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * Random.Range(0.98f, 1.1f);
        if (t > 3 && ! playing)
        {
            StartCoroutine(Play());
        }
    }
    public bool playing = false;
    IEnumerator Play()
    {
        playing = true;
        while (playing)
        {
            note++;

            source.PlayOneShot(notes[Random.Range(0, notes.Length)], volume);
            
            system.Emit(emissionRate);
            yield return new WaitForSeconds(1f/speed*60);
        }
    }
}
