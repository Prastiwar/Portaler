using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    enum ColliderItemType
    {
        Portal,
        EndingDoor,
        StealItem
    }
    [SerializeField] ColliderItemType colItemType;
    [SerializeField] ScriptableStealItem stealItem;
    public bool isItFirstPortal;

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
