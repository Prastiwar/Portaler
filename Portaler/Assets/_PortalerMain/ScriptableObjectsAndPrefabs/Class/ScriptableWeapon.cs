using System.Collections;
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

    public void Save(int i)
    {
        UEncryptPrefs.SetInt("weapon" + i, isPurchased ? 1:0);
    }
    public void Load(int i)
    {
        isPurchased = UEncryptPrefs.GetInt("weapon" + i) == 1 ? true : false;
    }

}