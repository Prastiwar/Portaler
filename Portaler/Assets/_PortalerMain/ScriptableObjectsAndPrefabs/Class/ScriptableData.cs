using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Data", fileName = "New Data")]
public class ScriptableData : ScriptableObject
{
    public ScriptableWeapon[] Weapons;
    public ScriptableEnemy[] Enemies;
    public ScriptableLevel[] Levels;
    public ScriptableStealItem[] StealItems;

    public void Save()
    {
        int _Length = Weapons.Length;
        for (int i = 0; i < _Length; i++)
        {
            Weapons[i].Save(i);
        }
    }
    public void Load()
    {
        int _Length = Weapons.Length;
        for (int i = 0; i < _Length; i++)
        {
            Weapons[i].Load(i);
        }
    }
}
