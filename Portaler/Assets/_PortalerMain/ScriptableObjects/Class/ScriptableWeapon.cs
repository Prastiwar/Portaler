using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Weapon", fileName = "New Weapon")]
public class ScriptableWeapon : ScriptableObject
{
    public int MaxAmmo;
    public int Ammo;
    public float Distance;
    public GameObject Portal_1;
    public GameObject Portal_2;
}
