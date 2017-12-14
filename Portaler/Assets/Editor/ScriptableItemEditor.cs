using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

[CustomEditor(typeof(ScriptableItem), true)]
[CanEditMultipleObjects]
public class ScriptableItemEditor : Editor
{
    //bool active = true;

    //public override void OnInspectorGUI()
    //{
    //    var serializedObject = new SerializedObject(target);
    //    var layout = serializedObject.FindProperty("_itemContentLayout");
    //    var icons = serializedObject.FindProperty("icons");
    //    var texts = serializedObject.FindProperty("texts");
    //    var buttons = serializedObject.FindProperty("buttons");

    //    if (GUILayout.Button("Toggle Item Layout Visuals"))
    //    {
    //        active = !active;
    //    }
    //    if (active)
    //    {
    //        serializedObject.Update();
    //        EditorGUILayout.PropertyField(layout, true);
    //        EditorGUILayout.PropertyField(icons, true);
    //        EditorGUILayout.PropertyField(texts, true);
    //        EditorGUILayout.PropertyField(buttons, true);
    //        serializedObject.ApplyModifiedProperties();
    //    }

    //    base.DrawDefaultInspector();
    //}
}