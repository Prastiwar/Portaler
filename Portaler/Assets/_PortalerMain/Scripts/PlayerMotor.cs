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
    public Transform weaponPos;
    float moveHorizontal;

    Rigidbody2D rb2D;
    Vector2 _rb2dVelocity;
    SpriteRenderer sr;

    Animator anim;
    int animWalk = Animator.StringToHash("Walk");

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

    void LateUpdate()
    {
        // Gun is looking at mouse position
        Vector2 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - weaponPos.position;
        diff.Normalize();
        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        weaponPos.rotation = Quaternion.Euler(0f, 0f, rotZ);
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
            {
                sr.flipX = false;
                //weaponPos.transform.position = new Vector3(weaponPos.transform.position.y + 1, weaponPos.transform.position.y, weaponPos.transform.position.z);
            }
            else if (moveHorizontal < 0 && !sr.flipX) // if going left, set "flip" to true - (to left direction)
            {
                sr.flipX = true;
                //weaponPos.transform.position = new Vector3(weaponPos.transform.position.y - 1, weaponPos.transform.position.y, weaponPos.transform.position.z);
            }
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