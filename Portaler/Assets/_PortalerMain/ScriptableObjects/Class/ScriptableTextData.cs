using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/TextData", fileName = "New TextData")]
public class ScriptableTextData : ScriptableObject
{
    public string[] WinText;
    public string[] LoseText;
    public string[] sellDoneText;
    public string[] sellFailureText;
}
