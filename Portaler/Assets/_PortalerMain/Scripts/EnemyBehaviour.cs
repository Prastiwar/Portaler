using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] ScriptableEnemy scriptableEnemy;
    [SerializeField] Transform wallPoint1;
    [SerializeField] Transform wallPoint2;
    [SerializeField] Transform GroundPoint;

    Animator animator;
    SpriteRenderer sr;
    Rigidbody2D rb2D;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = scriptableEnemy.animator;
    }

    void LateUpdate()
    {
        if (GameState.player.isSpotted)
            return;

        EnemySigh();
        EnemyMove();
    }

    void EnemyMove()
    {
        bool isGrounded = Physics2D.Linecast(transform.position, GroundPoint.position, scriptableEnemy.enemyMask);
        bool isBlocked = Physics2D.Linecast(wallPoint1.position, wallPoint2.position, scriptableEnemy.enemyMask);

        //If theres no ground, turn around. Or if I hit a wall, turn around
        if (!isGrounded || isBlocked)
        {
            Vector3 currRot = transform.eulerAngles;
            currRot.y += 180;
            transform.eulerAngles = currRot;
        }

        //Always move forward
        Vector2 myVel = rb2D.velocity;
        myVel.x = transform.right.x * scriptableEnemy.moveSpeed;
        rb2D.velocity = myVel;
        //rb2D.velocity.Set(transform.right.x * scriptableEnemy.moveSpeed, 0);
    }

    void EnemySigh()
    {
        Vector3 direction = sr.flipX ? -transform.right : transform.right;
        Debug.DrawRay(transform.position, direction * scriptableEnemy.rangeSigh, Color.red);
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, direction, scriptableEnemy.rangeSigh);

        if (hit2D.collider != null)
        {
            if (hit2D.collider.gameObject.layer == 10)
            {
                GameState.GameOver(true);
            }
        }
    }
}