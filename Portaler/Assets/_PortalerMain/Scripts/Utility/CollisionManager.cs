using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public bool isItFirstPortal;
    [SerializeField] bool isPortal;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isPortal)
            GameState.OnPortalEnter(other, gameObject);
        else
            GameState.GameOver(false);
    }
}
