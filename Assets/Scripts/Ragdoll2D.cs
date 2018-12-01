using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll2D : MonoBehaviour {

    public  Ragdoll2DPart[] parts;

	// Use this for initialization
	void Start () {
        parts = new Ragdoll2DPart[this.transform.childCount];
        for(int c = 0; c< transform.childCount; ++c)
        {
            parts[c] = transform.GetChild(c).GetComponent<Ragdoll2DPart>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
