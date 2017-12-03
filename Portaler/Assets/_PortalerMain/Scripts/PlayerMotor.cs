using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor : MonoBehaviour
{
    public static PlayerMotor instance;

    public float moveSpeed = 1200;
    public float jumpForce = 250;
    Rigidbody2D rb2D;
    Vector2 _rb2dVelocity;
    SpriteRenderer sr;
    Animator anim;
    int animWalk = Animator.StringToHash("Walk");
    float moveHorizontal;
    [SerializeField] LayerMask ground;
    [SerializeField] Transform groundPos;
    bool canMove = true;
    public bool isGrounded = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void OnWalkLeft()
    {
        moveHorizontal = -(moveSpeed * Time.fixedDeltaTime); // left
    }
    public void OnWalkRight()
    {
          moveHorizontal = (moveSpeed * Time.fixedDeltaTime); // right
    }
    public void OnStop()
    {
        moveHorizontal = 0;
    }
    public void OnJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _rb2dVelocity += jumpForce * Vector2.up;
            isGrounded = false;
            rb2D.velocity = _rb2dVelocity;
        }
    }

    void Move()
    {
        // temporary pc left right movement
        float moveHorizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        _rb2dVelocity = rb2D.velocity;

        if (canMove)
        {
            // Check grounded
            if (Physics2D.OverlapCircle(groundPos.position, 0.04f, ground))
            {
                if (!isGrounded)
                    isGrounded = true;
            }

            // temporary pc jump
            OnJump();
            
            _rb2dVelocity.x = moveHorizontal;
            rb2D.velocity = _rb2dVelocity;

            // Change between Idle and Walk animation
            /*if (moveHorizontal > 0 || moveHorizontal < 0)
            {
                if (!anim.GetBool(animWalk))
                {
                    ChangeRunState();
                }
            }
            else
            {
                if (anim.GetBool(animWalk))
                {
                    ChangeRunState();
                }
            }*/

            // Face to direction
            if (moveHorizontal > 0 && sr.flipX) // if going right, set "flip" to false - (to right direction)
                sr.flipX = false;
            else if (moveHorizontal < 0 && !sr.flipX) // if going left, set "flip" to true - (to left direction)
                sr.flipX = true;
        }
    }

    void ChangeRunState()
    {
        // If player is idle - start running
        // If player is running - start idle
        anim.StopPlayback();
        anim.SetBool(animWalk, !anim.GetBool(animWalk));
    }
}