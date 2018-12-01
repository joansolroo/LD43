using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll2DPart : MonoBehaviour {

    HashSet<Transform> hits = new HashSet<Transform>();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.parent!=this.transform.parent)
        {
            Debug.Log("hit:" + this.name+" to "+collision.gameObject.name);
            hits.Add(collision.transform);
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.parent != this.transform.parent)
        {
            Debug.Log("hit out:" + this.name + " to " + collision.gameObject.name);
            hits.Remove(collision.transform);
        }
    }

}

