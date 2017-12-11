using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Scriptable
{
    weapons,
    levels
}
[CreateAssetMenu(menuName = "Scriptable/Data", fileName = "New Data")]
public class ScriptableData : ScriptableObject
{
    public ScriptableWeapon[] Weapons;
    public ScriptableEnemy[] Enemies;
    public ScriptableLevel[] Levels;
    public ScriptableStealItem[] StealItems;

    int GetLength(Scriptable scriptable)
    {
        switch (scriptable)
        {
            case Scriptable.weapons:
                return Weapons.Length;
            case Scriptable.levels:
                return Levels.Length;
            default:
                break;
        }
        return 0;
    }
    public List<System.Object> Get(Scriptable scriptable)
    {
        List<System.Object> objects = new List<System.Object>();

        int length = GetLength(scriptable);
        for (int i = 0; i < length; i++)
        {
            GetSerializers(scriptable, objects, i);
        }
        return objects;
    }
    public void Set(Scriptable scriptable, List<System.Object> objects)
    {
        int length = GetLength(scriptable);
        for (int i = 0; i < length; i++)
        {
            SetSerializers(scriptable, objects, i);
        }
    }
    void GetSerializers(Scriptable scriptable, List<System.Object> objects, int i)
    {
        switch (scriptable)
        {
            case Scriptable.weapons:
                objects.Add(Weapons[i].GetSerializers());
                break;
            case Scriptable.levels:
                objects.Add(Levels[i].GetSerializers());
                break;
            default:
                break;
        }
    }
    void SetSerializers(Scriptable scriptable, List<System.Object> objects, int i)
    {
        switch (scriptable)
        {
            case Scriptable.weapons:
                Weapons[i].SetSerializers(objects, i);
                break;
            case Scriptable.levels:
                Levels[i].SetSerializers(objects, i);
                break;
            default:
                break;
        }
    }

    // Test getters setters
    
    //public List<System.Object> GetWeapons()
    //{
    //    List<System.Object> objects = new List<System.Object>();

    //    for (int i = 0; i < Weapons.Length; i++)
    //    {
    //        objects.Add(Weapons[i].GetSerializers());
    //    }
    //    return objects;
    //}
    //public List<System.Object> GetLevels()
    //{
    //    List<System.Object> objects = new List<System.Object>();

    //    for (int i = 0; i < Levels.Length; i++)
    //    {
    //        objects.Add(Levels[i].GetSerializers());
    //    }
    //    return objects;
    //}
    //public void SetLevels(List<System.Object> objects)
    //{
    //    for (int i = 0; i < Levels.Length; i++)
    //    {
    //        Debug.Log(i + " level " + i);
    //        Levels[i].SetSerializers(objects, i);
    //    }
    //}
    //public void SetWeapons(List<System.Object> objects)
    //{
    //    for (int i = 0; i < Weapons.Length; i++)
    //    {
    //        Debug.Log(i + " weapon " + i);
    //        Weapons[i].SetSerializers(objects, i);
    //    }
    //}
}
