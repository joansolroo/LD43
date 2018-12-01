using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    private Rigidbody2D rb;

    public float score = 0.0f;
    public float currentScore = 0.0f;
    public float scoreSpeed = 0.1f;


    private bool previousGrounded = false;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        //  Movement
        float translation = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        transform.Translate(translation, 0, 0);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(0, 30, 0);
        }

        if (Input.GetButton("Fire2"))
        {
            transform.Rotate(0, 0,  rotationSpeed * Time.deltaTime);
        }
        if (Input.GetButton("Fire1"))
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }


        if (previousGrounded && Mathf.Abs(rb.velocity.y) > 0.0001f)
        {
            previousGrounded = false;
        }

        //  score
        if(Mathf.Abs(rb.velocity.y) < 0.0001f)
        {
            if(!previousGrounded)
            {
                if(Mathf.Abs(transform.localRotation.z) < 0.3f)
                {
                    score += 2.0f * currentScore;
                    Debug.Log("combo !");
                }
                else
                    score += currentScore;
                currentScore = 0.0f;
            }
            previousGrounded = true;
        }

        if (!previousGrounded)
        {
            currentScore += scoreSpeed * Mathf.Abs(rb.angularVelocity);
        }
    }
}
