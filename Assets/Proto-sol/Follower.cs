using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : PhysicsObject
{
    [SerializeField] Transform target;
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        maxSpeed = Random.Range(1f, 4f);
        jumpTakeOffSpeed = Random.Range(3f, 9f);
        delay = Random.Range(0.1f, 0.5f);
        transform.localScale = Vector3.one * Random.Range(2f, 6f);
    }

    public void LockMove(Vector2 _move)
    {
        target = null;
        move = _move;
    }
    Vector2 move = Vector2.zero;
    float delay = 0.3f;
    protected override void ComputeVelocity()
    {
        if (target != null)
        {
            float dx = Mathf.Sign(target.position.x - this.transform.position.x);//xInput.GetAxis("Horizontal");
            if (Mathf.Abs(dx) < 1f)
            {
                move.x = Mathf.Sign(dx) * 1f;
            }
            move.x = (1 - delay) * move.x + delay * dx;

            if (Input.GetButtonDown("Jump") && grounded)
            {
                velocity.y = jumpTakeOffSpeed;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }

            bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
            if (flipSprite)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }

        //animator.SetBool("grounded", grounded);
        //animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerPlatformerController player = collision.gameObject.GetComponent<PlayerPlatformerController>();
        if(player != null)
        {
            this.target = player.transform;
            this.gameObject.layer = 9;
            player.StartFollowing(this);
        }
    }
}
