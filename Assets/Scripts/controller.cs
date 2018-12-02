using System.Collections;
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
    public float scoreSpeed = 1f;

    private float prevVerticalPos;

    private bool previousGrounded = false;

    [Header("UI display")]
    public Text scoreDisplay;
    public Text comboDisplay;
    public Text actionDisplay;
    [Space(10)]

    /* public enum ScoreType
        {
            Hit,
            LoseLimb,
            HitObject
        };
     */
    public int[] comboPonderation;
    public string lastEvent;

    [SerializeField] float mass = 50; // TODO: get from body
    // Use this for initialization
    void Start ()
    {
        score = 0.0f;
        //rb = GetComponent<Rigidbody2D>();
        prevVerticalPos = transform.position.y;
        lastEvent = "";
    }
    
    void Update()
    {
        ComputeInput();
        computeLastEvent();
        ComputeScore();

        comboDisplay.text = getMultiplier().ToString();
        actionDisplay.text = lastEvent;
    }

    public void Stop()
    {
        AddCurrentCombo();
    }

    void ComputeInput()
    {

        if (!ragdoll.head.attached)
        { return; }
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
            ragdoll.AddVelocity(new Vector2(translation, 0));
            ragdoll.AddTorque(rotationSpeed);
        }
        else if (translation > 0)
        {
            ragdoll.AddVelocity(new Vector2(translation, 0));
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
                AddCurrentCombo();
            }
            previousGrounded = true;
        }

        if (!previousGrounded)
        {
            currentScore += scoreSpeed * Time.deltaTime;//* (int)Mathf.Abs(ragdoll.center.RB2D.angularVelocity);
        }

        // updating parameters
        prevVerticalPos = ragdoll.transform.position.y;
    }

    public int getMultiplier()
    {
        int multiplier = 0;
        foreach (ScoreType trick in ragdoll.currentComboList)
        {
            multiplier += 1 * comboPonderation[(int)trick];
        }
        return multiplier;
    }

    public void AddCurrentCombo()
    {
        //Debug.Log("Multiplier : " + getMultiplier());
        score += (int)currentScore * getMultiplier();

        if (scoreDisplay)
            scoreDisplay.text = score.ToString();

        //Debug.Log(score);
        ragdoll.currentComboList.Clear();
        currentScore = 0.0f;
        previousGrounded = true;
    }

    private void computeLastEvent()
    {
        if(ragdoll.currentComboList.Count < 1)
        {
            lastEvent = "";
            return;
        }

        //for(int i=0; i<50; i++)
        {
            ScoreType a = ragdoll.currentComboList[ragdoll.currentComboList.Count - 1];
            if (a == ScoreType.Hit)
                lastEvent = "Hit";
            else if (a == ScoreType.LoseLimb)
                lastEvent = "LoseLimb";
            else if (a == ScoreType.HitObject)
                lastEvent = "HitObject";
            else lastEvent = "";
        }
    }
}
