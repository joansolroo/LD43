using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll2DPart : MonoBehaviour {

    public Ragdoll2D ragdoll;
    public Rigidbody2D RB2D;
    HashSet<Transform> hits = new HashSet<Transform>();

    public bool attached = true;

    [SerializeField] float health = 100;
    [SerializeField] float armor = 30;
    private void Start()
    {
        ragdoll = transform.parent.gameObject.GetComponent<Ragdoll2D>();
        RB2D = GetComponent<Rigidbody2D>();
        ragdoll.AddLimb(this);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.parent!=this.transform.parent)
        {
            RB2D.AddForce(collision.relativeVelocity);

            Debug.Log("hit:" + this.name+" to "+collision.gameObject.name);
            hits.Add(collision.transform);
            ragdoll.CollisionEnter(this, collision);

            Damaged(collision.relativeVelocity);
            
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
    
    void Damaged(Vector2 relativeVelocity)
    {
        float dmg = relativeVelocity.magnitude * 0.5f;
        if (dmg > armor)
        {
            health -= dmg;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.red, Color.white, health / 100f);
            if (health <= 0)
            {
                ragdoll.RemoveLimb(this);
                HingeJoint2D joint = GetComponent<HingeJoint2D>();
                if (joint != null)
                {
                    joint.enabled = false;
                    attached = false;
                }
                
            }
        }
    }
}

