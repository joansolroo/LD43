using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class controller : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 450.0f;
    private Rigidbody2D rb;

    public float score;
    public float currentScore = 0.0f;
    public float scoreSpeed = 0.1f;

    private float prevVerticalPos;
    public float velocityEpsilon = 0.01f;

    private bool previousGrounded = false;
    public Text scoreDisplay;

    // Use this for initialization
    void Start ()
    {
        score = 0.0f;
        rb = GetComponent<Rigidbody2D>();
        prevVerticalPos = transform.position.y;
    }
    
    void Update()
    {
        //  Movement
        float translation = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        rb.AddForce(new Vector3(translation * 1000, 0, 0));

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector3(0, 30000, 0));
        }

        if (Input.GetButton("Fire2"))
        {
            rb.AddTorque(rotationSpeed);
            //transform.parent.Rotate(0, 0,  rotationSpeed * Time.deltaTime);
        }
        if (Input.GetButton("Fire1"))
        {
            rb.AddTorque(-rotationSpeed);
            //transform.parent.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
        
        if (previousGrounded && Mathf.Abs(rb.velocity.y) > velocityEpsilon)
        {
            previousGrounded = false;
        }



        //  score
        //Debug.Log(transform.position.y - prevVerticalPos);
        if(Mathf.Abs(rb.velocity.y) < velocityEpsilon && transform.position.y - prevVerticalPos < 0.0f)
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

                scoreDisplay.text = score.ToString();

                Debug.Log(score);
                currentScore = 0.0f;
                previousGrounded = true;
            }
            previousGrounded = true;
        }

        if (!previousGrounded)
        {
            currentScore += scoreSpeed * Mathf.Abs(rb.angularVelocity);
        }

        // updating parameters
        prevVerticalPos = transform.position.y;
    }
}
