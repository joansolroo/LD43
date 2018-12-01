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
	

    public void CollisionEnter(Ragdoll2DPart part, Collision2D collision)
    {
        //TODO: handle hits
    }
    public void CollisionExit(Ragdoll2DPart part, Collision2D collision)
    {
        //TODO: handle hits
    }

    public bool IsColliding()
    {
        foreach(Ragdoll2DPart part in parts)
        {
            if (part.IsColliding()) return true;
        }
        return false;
    }
    public Ragdoll2DPart[] GetCollisions()
    {
        if (!IsColliding())
        {
            return null;
        }
        List<Ragdoll2DPart> hits = new List<Ragdoll2DPart>();
        foreach (Ragdoll2DPart part in parts)
        {
            if (part.IsColliding()) hits.Add(part);
        }
        return hits.ToArray();
    }
}
