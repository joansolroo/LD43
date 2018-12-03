using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Public : MonoBehaviour {

    [SerializeField] Ragdoll2D ragdoll;
    [SerializeField] float ExcitementDecay = 1;
    [SerializeField] AudioClip[] peopleSounds;
    List<PublicMember> people = new List<PublicMember>();
    AudioSource source;
    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
		for(int c = 0; c < transform.childCount; ++c)
        {
            PublicMember member = transform.GetChild(c).GetComponent<PublicMember>();
            if (member != null)
            {
                people.Add(member);
            }
        }
        excitement = 0;
        combo = 0;
    }

    float excitement= 0;
    int combo = 0;
    int change = 0;
    // Update is called once per frame
    void Update () {
        if (ragdoll.currentComboList.Count != combo)
        {
            change = Mathf.Max(0, ragdoll.currentComboList.Count-combo);
            combo = ragdoll.currentComboList.Count;
            excitement += change;
        }
        if(combo == 0)
        {
            excitement-= Time.deltaTime* ExcitementDecay;
        }

        if (change > 1)
        {
            StartCoroutine(Laugh());
        }
        int c = 0;
        foreach (PublicMember p in people)
        {
            {
                p.happiness = Mathf.Min(5,Mathf.Max(0,excitement/5-c));
            }
            ++c;
        }
	}
    int laughing = 0;
    bool justLaugh = false;
    int lastOne = -1;
    IEnumerator Laugh()
    {
        if (!justLaugh && laughing <3)
        {
            laughing++;
            justLaugh = true;
            int idx = Random.Range(0, peopleSounds.Length - 1);
            if (idx == lastOne) idx = (++idx % peopleSounds.Length);
            AudioClip clip = peopleSounds[idx];
            lastOne = idx;
            
            source.PlayOneShot(clip, Random.Range(0.2f, 0.5f));
            yield return new WaitForSeconds(0.25f);
            justLaugh = false;
            yield return new WaitForSeconds(clip.length-0.25f);
            laughing--;
        }
        
    }
}
