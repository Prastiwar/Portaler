using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateBehaviour : MonoBehaviour
{
    [SerializeField] ScriptableEnemy scriptableEnemy;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        sr.sprite = scriptableEnemy.sprite;
    }
    void LateUpdate()
    {
        if (GameState.isSpotted)
            return;

        EnemySigh();
        EnemyMove();
    }

    void EnemyMove()
    {
        
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
