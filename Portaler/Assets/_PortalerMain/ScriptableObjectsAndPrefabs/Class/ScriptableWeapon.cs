using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Weapon", fileName = "New Weapon")]
public class ScriptableWeapon : ScriptableItem, ISerializable
{
    [Header("Main Weapon Content - Dont look up")]
    public ScriptableItem scriptableItem;
    public bool isPurchased;

    public Sprite sprite;
    public int price;
    public int maxAmmo;
    public int ammo;
    public float distance;

    public GameObject portal_1;
    public GameObject portal_2;

    public void InitializeItem()
    {
        _itemContentLayout = scriptableItem._itemContentLayout;
        icons = scriptableItem.icons;
        texts = scriptableItem.texts;
        buttons = scriptableItem.buttons;
    }

    public object GetSerializers()
    {
        return isPurchased;
    }

    public void SetSerializers(List<System.Object> objects, int i)
    {
        isPurchased = (bool)objects[i];
    }

}