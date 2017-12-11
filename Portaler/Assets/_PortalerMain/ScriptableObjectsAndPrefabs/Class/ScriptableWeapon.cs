﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Weapon", fileName = "New Weapon")]
public class ScriptableWeapon : ScriptableObject
{
    public bool isPurchased;

    public Sprite sprite;
    public int price;
    public int maxAmmo;
    public int ammo;
    public float distance;

    public GameObject portal_1;
    public GameObject portal_2;

}