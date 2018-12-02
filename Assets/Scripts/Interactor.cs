using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour {

    public Ragdoll2D ragdoll;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Interact()
    {
        if (ragdoll)
        {
            ragdoll.currentComboList.Add(this);
        }
    }
}
