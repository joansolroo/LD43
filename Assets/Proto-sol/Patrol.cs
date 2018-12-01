using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : PhysicsObject
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        maxSpeed += Random.Range(-1f, 1f);
        //jumpTakeOffSpeed = Random.Range(3f, 9f);
        //delay = Random.Range(0.1f, 0.5f);
        //transform.localScale = Vector3.one * Random.Range(2f, 6f);
    }

    Vector2 move = Vector2.zero;
    float delay =0.7f;
    protected override void ComputeVelocity()
    {

        float dx = Mathf.Sign(Mathf.Sin(Time.time*360/(50+Random.Range(-5f, 5f))));
        if (Mathf.Abs(dx) < 1f)
        {
            move.x = Mathf.Sign(dx) * 1f;
        }
        move.x = (1 - delay) * move.x + delay * dx;
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        //animator.SetBool("grounded", grounded);
        //animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }
}
