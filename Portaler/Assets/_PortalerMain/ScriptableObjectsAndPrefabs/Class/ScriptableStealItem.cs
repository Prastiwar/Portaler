using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/StealItem", fileName = "New StealItem")]
public class ScriptableStealItem : ScriptableObject
{
    public Sprite image;
    public string nameItem;
    public int moneyValue;
    [Multiline]
    public string description;
}
