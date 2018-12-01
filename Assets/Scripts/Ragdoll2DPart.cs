using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll2DPart : MonoBehaviour {

    Ragdoll2D ragdoll;
    HashSet<Transform> hits = new HashSet<Transform>();

    private void Start()
    {
        ragdoll = transform.parent.gameObject.GetComponent<Ragdoll2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.parent!=this.transform.parent)
        {
            Debug.Log("hit:" + this.name+" to "+collision.gameObject.name);
            hits.Add(collision.transform);
            ragdoll.CollisionEnter(this, collision);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.parent != this.transform.parent)
        {
            Debug.Log("hit out:" + this.name + " to " + collision.gameObject.name);
            hits.Remove(collision.transform);
            ragdoll.CollisionExit(this, collision);
        }
    }

    public bool IsColliding()
    {
        return hits.Count == 0;
    }

}

