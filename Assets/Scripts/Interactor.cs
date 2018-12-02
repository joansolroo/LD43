using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Collide(Ragdoll2DPart part)
    {
        if (part.ragdoll)
        {
            part.ragdoll.currentComboList.Add(ScoreType.HitObject);
            //Debug.Log("Combo (Object!) : " + this.gameObject.name + "Combo Size : " + part.ragdoll.currentComboList.Count);
        }
    }
}
