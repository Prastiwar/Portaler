using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectFindWithLayer : MonoBehaviour
{
    static GameObject[] AllGameObjects;

    public static GameObject Find(int layer)
    {
        if (AllGameObjects == null)
            AllGameObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];

        for (var i = 0; i < AllGameObjects.Length; i++)
        {
            if (AllGameObjects[i] != null)
            {
                if (AllGameObjects[i].layer == layer)
                {
                    Debug.Log(AllGameObjects[i]);
                    return AllGameObjects[i];
                }
            }
        }
        return null;
    }
}
