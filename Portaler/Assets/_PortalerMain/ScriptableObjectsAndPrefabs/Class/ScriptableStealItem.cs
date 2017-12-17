using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/StealItem", fileName = "New StealItem")]
public class ScriptableStealItem : ScriptableItem
{
    [Header("Main Steal Item Content - Dont look up")]
    public ScriptableItem scriptableItem;
    public Sprite image;
    public string nameItem;
    public int moneyValue;
    [Multiline]
    public string description;

    public void InitializeItem()
    {
        _itemContentLayout = scriptableItem._itemContentLayout;
        icons = scriptableItem.icons;
        texts = scriptableItem.texts;
        buttons = scriptableItem.buttons;
    }
}