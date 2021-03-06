﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScoreType
{
    Hit,
    LoseLimb,
    HitObject
};


public class Ragdoll2D : MonoBehaviour
{

    [SerializeField] float pushFactor = 20;
    public Ragdoll2DPart head;
    public Ragdoll2DPart center;
    public List<Ragdoll2DPart> parts = new List<Ragdoll2DPart>();
    public Ragdoll2DPart[] hands = new Ragdoll2DPart[2];
    public Ragdoll2DPart[] feet = new Ragdoll2DPart[2];

    public GameObject hitParticle;
    public GameObject ouchParticle;

    public List<ScoreType> currentComboList = new List<ScoreType>();

    public float magnitudeThreshold = 5f;

    [SerializeField] AudioSource sound;

    public static List<Ragdoll2D> ragdolls = new List<Ragdoll2D>();
    private void OnEnable()
    {
        ragdolls.Add(this);
    }
    private void OnDisable()
    {
        ragdolls[0].transform.rotation = this.transform.rotation;
        ragdolls.Remove(this);
    }
    public void CollisionEnter(Ragdoll2DPart part, Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > magnitudeThreshold)
        {
            /*
            if (!sound.isPlaying)
            {
                sound.pitch = Random.Range(1.1f, 1.4f);
                sound.Play();
            }
            */
            currentComboList.Add(ScoreType.Hit);
            Instantiate(hitParticle, collision.contacts[0].point, Quaternion.AngleAxis(Random.Range(-40, 40), new Vector3(0, 0, 1)));
            //Debug.Log("Combo (Aie!) : " + part.gameObject.name + "Combo Size : " + currentComboList.Count);
        }
    }
    public void CollisionExit(Ragdoll2DPart part, Collision2D collision)
    {
        //TODO: handle hits
    }

    void Start()
    {
        
    }

    bool grabbing = false;
    public bool down = false;

    bool init = true;
    private void Update()
    {
        if (init)
        {
            if (ragdolls.Count > 1)
            {
                this.transform.rotation = ragdolls[ragdolls.Count - 2].transform.rotation;
            }
            AddTorque(-Random.Range(100f, 200f));
            init = false;
        }
        float pushForce = pushFactor * Mass;
        if (grabbing)
        {
            foreach (Ragdoll2DPart hand in hands)
            {
                if (hand.attached)
                {
                    if (!hand.IsSticked())
                    {
                        hand.RB2D.AddForce(Vector3.up * pushForce);
                    }
                }
            }
        }
        if (down)
        {
            Vector3 feetCenter = Vector3.zero;
            int c = 0;
            foreach (Ragdoll2DPart foot in feet)
            {

                if (foot.attached)
                {
                    ++c;
                    foot.RB2D.AddForce(Vector3.down * pushForce);
                    feetCenter += foot.transform.position;
                }
            }
            feetCenter = feetCenter / 2;
            if (c > 0)
            {
                center.RB2D.AddForce((feetCenter+(Vector3.up*Vector3.Distance(feetCenter,center.transform.position))*pushForce*0.35f*c));
            }
        }
    }

    public void AddLimb(Ragdoll2DPart part)
    {
        parts.Add(part);
    }
    public void RemoveLimb(Ragdoll2DPart part)
    {
        if (part.attached)
        {
            parts.Remove(part);
            currentComboList.Add(ScoreType.LoseLimb);
            //Debug.Log("Combo (LosingPart) : " + part.gameObject.name + "Combo Size : " + currentComboList.Count);
            if (part == head)
            {
                //over
            }
            Instantiate(ouchParticle, part.gameObject.transform.position, Quaternion.AngleAxis(Random.Range(-40, 40), new Vector3(0, 0, 1)));
        }
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

    static float velocityEpsilon = 0.1f;
    public bool isGrounded()
    {
        return Mathf.Abs(center.RB2D.velocity.y) > velocityEpsilon;
    }

    public bool IsColliding()
    {
        foreach (Ragdoll2DPart part in parts)
        {
            if (part.IsColliding()) return true;
        }
        return false;
    }
    public bool IsAlive()
    {
        return head.attached;
    }
    public void AddVelocity(Vector2 velocity)
    {
        center.RB2D.velocity+=(velocity);
    }
    public void ResetVelocty()
    {
        center.RB2D.velocity = Vector2.zero;
    }
    public void AddForceCenter(Vector2 force)
    {
        center.RB2D.AddForce(force * Mass);
    }
    public void AddForceHomogenous(Vector2 force)
    {
        /*foreach (Ragdoll2DPart part in parts)
        {
            part.RB2D.AddForce(force);
        }*/
        foreach (Ragdoll2DPart hand in hands)
        {
            if (hand.attached)
            {
                hand.RB2D.AddForce(force);
            }
        }

        foreach (Ragdoll2DPart foot in feet)
        {

            if (foot.attached)
            {
                foot.RB2D.AddForce(force);
            }
        }

    }

    public void AddTorque(float torque)
    {
        center.RB2D.AddTorque(torque * Mass);
        foreach (Ragdoll2DPart hand in hands)
        {
            if (hand.attached)
            {
                hand.RB2D.AddTorque(torque);
            }
        }

        foreach (Ragdoll2DPart foot in feet)
        {

            if (foot.attached)
            {
                foot.RB2D.AddTorque(torque);
            }
        }
    }

    public void Grab()
    {
        grabbing = true;
        foreach (Ragdoll2DPart hand in hands)
        {
            hand.SetSticky(true);
        }
    }
    public void Release()
    {
        grabbing = false;
        foreach (Ragdoll2DPart hand in hands)
        {
            hand.SetSticky(false);
        }
    }

    bool simulated = true;
    public bool Simulated
    {
        get
        {
            return simulated;
        }
        set
        {
            foreach(Ragdoll2DPart part in parts)
            {
                if (part.attached)
                {
                    part.RB2D.simulated = value;
                }
            }
            simulated = value;
        }
    }
}
