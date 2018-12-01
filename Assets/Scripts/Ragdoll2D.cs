using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll2D : MonoBehaviour {

    public Ragdoll2DPart center;
    public  List<Ragdoll2DPart> parts = new List<Ragdoll2DPart>();

    public void CollisionEnter(Ragdoll2DPart part, Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 2) ;
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

    public void AddLimb(Ragdoll2DPart part) {
        parts.Add(part);
    }
    public void RemoveLimb(Ragdoll2DPart part)
    {
        parts.Remove(part);
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

    public float Mass
    {
        get
        {
            float v = 0;
            foreach (Ragdoll2DPart part in parts)
            {
                v += part.RB2D.mass;
            }
            return v;
        }
    }

    static float velocityEpsilon = 0.01f;
    public bool isGrounded()
    {
        return Mathf.Abs(center.RB2D.velocity.y) > velocityEpsilon;
    }

    public void AddForceCenter(Vector2 force)
    {
            center.RB2D.AddForce(force * Mass);
    }
    public void AddForceHomogenous(Vector2 force)
    {
        foreach (Ragdoll2DPart part in parts)
        {
            part.RB2D.AddForce(force);
        }
    }
    public void AddTorque(float torque)
    {
        center.RB2D.AddTorque(torque * Mass);
    }
}
