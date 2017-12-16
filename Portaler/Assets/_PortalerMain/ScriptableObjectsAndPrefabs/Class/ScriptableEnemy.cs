using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Enemy", fileName = "New Enemy")]
public class ScriptableEnemy : ScriptableObject
{
    public float rangeSigh;
    public float moveSpeed;
    public RuntimeAnimatorController animator;
    public LayerMask enemyMask;
}
