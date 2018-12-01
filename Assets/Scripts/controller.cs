﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class controller : MonoBehaviour
{
    public float movementSpeed = 10.0f;
    public float rotationSpeed = 450.0f;
    public float jumpForce = 450.0f;
    [SerializeField] Ragdoll2D ragdoll;

    public float score;
    public float currentScore = 0.0f;
    public float scoreSpeed = 0.1f;

    private float prevVerticalPos;

    private bool previousGrounded = false;
    public Text scoreDisplay;

    [SerializeField] float mass = 50; // TODO: get from body
    // Use this for initialization
    void Start ()
    {
        score = 0.0f;
        //rb = GetComponent<Rigidbody2D>();
        prevVerticalPos = transform.position.y;
    }
    
    void Update()
    {
        ComputeInput();

        ComputeScore();
    }

    void ComputeInput()
    {
        //  Movement
        float translation = Input.GetAxis("Horizontal") * movementSpeed;
        translation *= Time.deltaTime;
        //rb.AddForce();

        bool grab = Input.GetAxis("Vertical")>0;
        bool down = Input.GetAxis("Vertical") < 0;
        if (grab)
        {
            ragdoll.Grab();
        }
        else
        {
            ragdoll.Release();
        }
        ragdoll.down = down;

        if (Input.GetButtonDown("Jump"))
        {
            ragdoll.AddForceCenter(new Vector2(0, jumpForce));
        }

        if (translation < 0)
        {
            ragdoll.AddForceCenter(new Vector2(translation, 0));
            ragdoll.AddTorque(rotationSpeed);
        }
        else if (translation > 0)
        {
            ragdoll.AddForceCenter(new Vector2(translation, 0));
            ragdoll.AddTorque(-rotationSpeed);
        }

        if (previousGrounded && ragdoll.isGrounded())
        {
            previousGrounded = false;
        }
    }

    // TODO move to a dedicated script
    void ComputeScore()
    {
        //  score
        //Debug.Log(transform.position.y - prevVerticalPos);
        if (!ragdoll.isGrounded() && ragdoll.transform.position.y - prevVerticalPos < 0.0f)
        {
            if (!previousGrounded)
            {
                if (Mathf.Abs(ragdoll.transform.localRotation.z) < 0.3f)
                {
                    score += 2.0f * currentScore;
                    //Debug.Log("combo !");
                }
                else
                    score += currentScore;

                if(scoreDisplay)
                    scoreDisplay.text = score.ToString();

                //Debug.Log(score);
                currentScore = 0.0f;
                previousGrounded = true;
            }
            previousGrounded = true;
        }

        if (!previousGrounded)
        {
            currentScore += scoreSpeed * Mathf.Abs(ragdoll.center.RB2D.angularVelocity);
        }

        // updating parameters
        prevVerticalPos = ragdoll.transform.position.y;
    }
}
