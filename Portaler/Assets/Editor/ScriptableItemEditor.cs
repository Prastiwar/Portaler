using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

[CustomEditor(typeof(CollisionManager))]
[CanEditMultipleObjects]
public class ScriptableItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var scriptableItem = target as ScriptableItem;

        if (GUILayout.Button("Toggle Item Layout Visuals"))
        {
            EditorGUILayout.ObjectField("_itemContentLayout", scriptableItem._itemContentLayout, typeof(GameObject), true);
            EditorGUILayout.ObjectField("icons", scriptableItem.icons, typeof(Image[]), true);
            EditorGUILayout.ObjectField("texts", scriptableItem.texts, typeof(TextMeshProUGUI[]), true);
            EditorGUILayout.ObjectField("buttons", scriptableItem.buttons, typeof(Button[]), true);
        }
    }

}