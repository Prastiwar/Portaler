using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Enemy", fileName = "New Enemy")]
public class ScriptableEnemy : ScriptableObject
{
    public float RangeOfView;
    public float MoveSpeed;
    public Sprite Sprite;
}
