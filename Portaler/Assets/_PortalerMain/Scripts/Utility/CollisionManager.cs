using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public enum ColliderItemType
    {
        Portal,
        EndingDoor,
        StealItem
    }
    public ColliderItemType colItemType;
    [HideInInspector] public ScriptableStealItem stealItem;
    [HideInInspector] public bool isItFirstPortal;

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (colItemType)
        {
            case ColliderItemType.Portal:
                GameState.OnPortalEnter(other, gameObject);
                break;

            case ColliderItemType.EndingDoor:
                if (other.gameObject.layer == 10)
                    GameState.GameOver(false);
                break;

            case ColliderItemType.StealItem:
                if (other.gameObject.layer == 10)
                {
                    GameState.GetStealItem(stealItem);
                    gameObject.SetActive(false);
                }
                break;

            default:
                break;
        }
    }
}
