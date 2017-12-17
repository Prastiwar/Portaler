using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Scriptable
{
    weapons,
    levels
}
public enum Cmd
{
    Get,
    Set
}
public interface ISerializable
{
    object GetSerializers();
    void SetSerializers(List<System.Object> objects, int i);
}
[CreateAssetMenu(menuName = "Scriptable/Data", fileName = "New Data")]
public class ScriptableData : ScriptableObject
{
    public ScriptableWeapon[] Weapons;
    public ScriptableEnemy[] Enemies;
    public ScriptableLevel[] Levels;
    public ScriptableStealItem[] StealItems;

    public object SaveLoad(Scriptable scriptable, Cmd cmd, List<System.Object> objects)
    {
        int i = 0;
        switch (cmd)
        {
            case Cmd.Get:
                List<System.Object> _objects = new List<System.Object>();
                Loop(scriptable, _objects, i, () => Switcher(scriptable, _objects, i, false));
                return _objects;

            case Cmd.Set:
                Loop(scriptable, objects, i, () => Switcher(scriptable, objects, i, true));
                break;

            default:
                break;
        }
        return null;
    }

    void Loop(Scriptable scriptable, List<System.Object> objects, int i, UnityAction call)
    {
        int length = GetLength(scriptable);
        for (i = 0; i < length; i++)
        {
            call();
        }
    }
    void Switcher(Scriptable scriptable, List<System.Object> objects, int i, bool toSet)
    {
        switch (scriptable)
        {
            case Scriptable.weapons:
                if (!toSet) objects.Add(Weapons[i].GetSerializers());
                else Weapons[i].SetSerializers(objects, i);
                break;
            case Scriptable.levels:
                if (!toSet) objects.Add(Levels[i].GetSerializers());
                else Levels[i].SetSerializers(objects, i);
                break;
            default:
                break;
        }
    }

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
}