using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Data", fileName = "New Data")]
public class ScriptableData : ScriptableObject
{
    public ScriptableWeapon[] Weapons;
    public ScriptableEnemy[] Enemies;
    public ScriptableLevel[] Levels;
}
