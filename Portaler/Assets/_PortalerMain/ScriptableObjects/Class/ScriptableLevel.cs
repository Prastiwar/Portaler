using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Level", fileName = "New Level")]
public class ScriptableLevel : ScriptableObject
{
    public bool isUnlocked;
    public GameObject levelPrefab;
    public Sprite sprite;
    public float starScoreAmount;
}