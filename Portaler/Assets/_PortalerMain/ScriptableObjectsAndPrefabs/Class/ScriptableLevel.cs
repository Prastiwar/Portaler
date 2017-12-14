using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Level", fileName = "New Level")]
public class ScriptableLevel : ScriptableItem, ISerializable
{
    [Header("Main Level Content - Dont look up")]
    public ScriptableItem scriptableItem;
    public bool isUnlocked;
    public GameObject levelPrefab;
    public Transform startWaypoint;

    public Sprite sprite;
    public Sprite lockedSprite;
    public float starScoreAmount;

    public void InitializeItem()
    {
        _itemContentLayout = scriptableItem._itemContentLayout;
        icons = scriptableItem.icons;
        texts = scriptableItem.texts;
        buttons = scriptableItem.buttons;
    }

    public object GetSerializers()
    {
        List<System.Object> objects = new List<System.Object>
        {
            isUnlocked,
            starScoreAmount
        };
        return objects;
    }

    public void SetSerializers(List<System.Object> objects, int i)
    {
        List<System.Object> objectsDeserialized = objects[i] as List<System.Object>;

        isUnlocked = (bool)objectsDeserialized[0];
        starScoreAmount = (float)objectsDeserialized[1];
    }
}