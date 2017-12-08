using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public bool isItFirstPortal;

    void OnTriggerEnter2D(Collider2D other)
    {
        GameState.OnPortalEnter(other, gameObject);
    }
}
