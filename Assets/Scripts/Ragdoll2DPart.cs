using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll2DPart : MonoBehaviour
{

    public Ragdoll2D ragdoll;
    public Rigidbody2D RB2D;
    HashSet<Rigidbody2D> hits = new HashSet<Rigidbody2D>();
    HingeJoint2D stickPoint;

    public bool attached = true;
    public bool sticky = false;

    [SerializeField] float health = 100;
    [SerializeField] float armor = 30;
    private void Start()
    {
        ragdoll = transform.parent.gameObject.GetComponent<Ragdoll2D>();
        RB2D = GetComponent<Rigidbody2D>();
        ragdoll.AddLimb(this);
        stickPoint = gameObject.AddComponent<HingeJoint2D>();
        stickPoint.enabled = false;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.parent != this.transform.parent)
        {
            

            //Debug.Log("hit:" + this.name + " to " + collision.gameObject.name);
            hits.Add(collision.rigidbody);
            ragdoll.CollisionEnter(this, collision);

            // try to get interactor if it's a interactive object for score purpose
            Interactor interactorComponent = collision.gameObject.GetComponent<Interactor>();
            if (interactorComponent)
            {
                interactorComponent.Collide(this);
            }

            Damaged(collision.relativeVelocity);
            if (sticky)
            {
                Stick(collision.transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.parent != this.transform.parent)
        {
            //Debug.Log("hit out:" + this.name + " to " + collision.gameObject.name);
            hits.Remove(collision.rigidbody);
            //ragdoll.CollisionExit(this, collision);
        }
    }

    public void SetSticky(bool _sticky)
    {
        sticky = _sticky;
        if (sticky)
        {
            if (hits.Count > 0)
            {
                Rigidbody2D rb = hits.GetEnumerator().Current;
                if (rb != null)
                {
                    Stick(hits.GetEnumerator().Current.transform);
                }
            }
        }
        else
        {
            Unstick();
        }
    }

    public bool IsColliding()
    {
        return hits.Count == 0;
    }

    void Stick(Transform t)
    {
        Rigidbody2D rb = t.gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = t.gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static;
            rb.mass = float.MaxValue;
        }
        stickPoint.enabled = true;
        stickPoint.connectedBody = rb;
        
    }

    void Unstick()
    {
        stickPoint.enabled = false;
        stickPoint.connectedBody = null;
    }

    void Damaged(Vector2 relativeVelocity)
    {
        float dmg = relativeVelocity.magnitude * 0.3f;
        dmg = Mathf.Max(0, dmg - armor);
        if (dmg > 0)
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

