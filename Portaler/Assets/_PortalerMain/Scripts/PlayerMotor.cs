using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor : MonoBehaviour
{
    public static PlayerMotor instance;
    [HideInInspector] public Transform player;

    // Forces
    public float moveSpeed = 1200;
    public float jumpForce = 250;
    float moveHorizontal;

    // Weapon
    [SerializeField] Transform weaponPos;
    SpriteRenderer weaponSr;

    // Rigid and Sprite
    [HideInInspector] public Rigidbody2D rb2D;
    Vector2 _rb2dVelocity;
    SpriteRenderer sr;

    // Animation
    Animator anim;
    int animWalk = Animator.StringToHash("Walk");
    int animJump = Animator.StringToHash("Jump");

    // Ground
    [SerializeField] LayerMask ground;
    public Transform groundPos;
    public bool isGrounded = true;
    bool canMove = true;

    [SerializeField] ParticleSystem _walkParticles;
    [SerializeField] AudioClip[] audioClips;

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

        player = this.transform;
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        weaponSr = weaponPos.GetComponent<SpriteRenderer>();
        rb2D.bodyType = RigidbodyType2D.Static;
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
        if(rb2D.bodyType == RigidbodyType2D.Dynamic)
            Move();
    }

    public void OnWalkLeft()
    {
        if (isGrounded)
        {
            SoundManager.Instance.PlaySound(audioClips[0], 1, true);
            if (!_walkParticles.isPlaying)
                _walkParticles.Play();
        }
        moveHorizontal = -(moveSpeed * Time.fixedDeltaTime); // left
    }
    public void OnWalkRight()
    {
        if (isGrounded)
        {
            SoundManager.Instance.PlaySound(audioClips[0], 1, true);
            if (!_walkParticles.isPlaying)
                _walkParticles.Play();
        }
        moveHorizontal = (moveSpeed * Time.fixedDeltaTime); // right
    }
    public void OnStop()
    {
        moveHorizontal = 0;
        SoundManager.Instance.PlaySound(audioClips[0], 1, false);
    }
    public void OnJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            SoundManager.Instance.PlaySound(audioClips[1], 1);
            _rb2dVelocity += jumpForce * Vector2.up;
            isGrounded = false;
            rb2D.velocity = _rb2dVelocity;
            anim.SetBool(animJump, true);
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
            if (Physics2D.OverlapCircle(groundPos.position, 0.055f, ground))
            {
                if (!isGrounded)
                {
                    isGrounded = true;
                    anim.SetBool(animJump, false);
                }
            }

            // temporary pc jump
            OnJump();
            
            _rb2dVelocity.x = moveHorizontal;
            rb2D.velocity = _rb2dVelocity;

            // Change between Idle and Walk animation
            if (moveHorizontal > 0 || moveHorizontal < 0)
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
            }
            // Face to direction
            Vector2 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            diff.Normalize();
            if ((moveHorizontal > 0 || diff.x > 0) && sr.flipX) // if going right, set "flip" to false - (to right direction)
            {
                sr.flipX = false;
                weaponSr.flipY = false;
            }
            else if ((moveHorizontal < 0 || diff.x < 0) && !sr.flipX) // if going left, set "flip" to true - (to left direction)
            {
                sr.flipX = true;
                weaponSr.flipY = true;
            }
            if (transform.position.y < -10)
                GameState.GameOver(true);
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