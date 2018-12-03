using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Public : MonoBehaviour {

    [SerializeField] Ragdoll2D ragdoll;
    [SerializeField] float ExcitementDecay = 1;
    [SerializeField] AudioClip[] peopleSounds;
    [SerializeField] float maxDistance = -1;
    List<PublicMember> people = new List<PublicMember>();
    AudioSource source;

        /// <summary>
        /// Shuffles the element order of the specified list.
        /// </summary>
        public static void Shuffle<T>(List<T> ts)
        {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var r = UnityEngine.Random.Range(i, count);
                var tmp = ts[i];
                ts[i] = ts[r];
                ts[r] = tmp;
            }
        }


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
       Shuffle(people);
        excitement = 0;
        combo = 0;
    }

    public float excitement= 0;
    public int combo = 0;
    public int change = 0;
    // Update is called once per frame
    void Update () {

        Vector3 targetPos = ragdoll.center.transform.position;
        targetPos.y = this.transform.position.y;
        if (maxDistance>=0 && Vector3.Distance(targetPos, this.transform.position) > maxDistance)
        {
            this.transform.position = targetPos;
        }
        change = 0;
        if (ragdoll.currentComboList.Count != combo)
        {
            change = Mathf.Max(0, ragdoll.currentComboList.Count-combo);
            combo = ragdoll.currentComboList.Count;
            excitement += change;
        }
        
        if(change == 0)
        {
            excitement= Mathf.Max(0, excitement-Time.deltaTime* ExcitementDecay);
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
