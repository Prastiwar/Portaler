using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All player statistics
[Serializable]
public struct Player
{
    public int money;
    public int lvlIndex;
    public int weaponIndex;
    public int itemIndex;
    public bool isSpotted;
}
public class GameState : MonoBehaviour
{
    static StateMachineManager _stateManager;
    [SerializeField] public static Player player;
    [SerializeField] GameObject comingSoonScene;
    [SerializeField] Transform firePoint;

    GameObject[] _StealItems;

    static GameObject Portal_1;
    static GameObject Portal_2;

    public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        string _PauseState = "Pause State";
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(_PauseState))
            return;
        _stateManager = StateMachineManager.Instance;
        SetLevel();
    }

    public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetPortal(Portal_1);
        }
        if (Input.GetMouseButtonDown(1))
        {
            SetPortal(Portal_2);
        }
    }

    public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
    }

    public static void GameOver(bool _isSpotted)
    {
        player.isSpotted = _isSpotted;
        _stateManager.ChangeSceneTo("Result");
    }

    public static void GetStealItem(ScriptableStealItem _stealItem)
    {
        int _Length = _stateManager.data.StealItems.Length;
        for (int i = 0; i < _Length; i++)
        {
            if (_stateManager.data.StealItems[i] = _stealItem)
            {
                player.itemIndex = i;
                break;
            }
        }
    }

    // Instantiate level by index
    void SetLevel()
    {
        //GameObject _level;
        //if (data.Levels[lvlIndex] != null)
        //    _level = Instantiate(comingSoonScene);
        //else
        //    _level = Instantiate(data.Levels[lvlIndex].levelPrefab);

        player.itemIndex = -1;
        player.isSpotted = false;
        //Player.playerTransform.position = data.Levels[lvlIndex].startWaypoint.position;
        StateMachineManager.Instance.data.Weapons[player.weaponIndex].ammo = _stateManager.data.Weapons[player.weaponIndex].maxAmmo;
        Portal_1 = Instantiate(_stateManager.data.Weapons[player.weaponIndex].portal_1);
        Portal_2 = Instantiate(_stateManager.data.Weapons[player.weaponIndex].portal_2);

    }

    // What to do when player've went into portal
    public static void OnPortalEnter(Collider2D other, GameObject portal)
    {
        GameObject _otherPortal = portal.GetComponent<CollisionManager>().isItFirstPortal ? Portal_2 : Portal_1;

        if (_otherPortal.activeSelf)
        {
            Rigidbody2D otherRigidBody = other.GetComponent<Rigidbody2D>();
            Vector3 exitVelocity = _otherPortal.transform.forward * otherRigidBody.velocity.magnitude;

            otherRigidBody.position = portal.transform.position; // get into portal feeling
            otherRigidBody.velocity = exitVelocity;
            other.transform.position = _otherPortal.transform.position + _otherPortal.transform.forward * 2; // throw away rigidbody from other portal
        }
    }

    // Active and setting portal position on wall
    public void SetPortal(GameObject portal)
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = (mouseWorld - firePoint.position).normalized;
        Debug.DrawRay(firePoint.position, direction * 100, Color.cyan, 1.3f);

        RaycastHit2D hit2D = Physics2D.Raycast(firePoint.position, direction, _stateManager.data.Weapons[player.weaponIndex].distance);
        _stateManager.data.Weapons[player.weaponIndex].ammo--;

        if (hit2D.collider != null)
        {
            if (hit2D.collider.gameObject.layer == 8)
            {
                if (!portal.activeSelf)
                    portal.SetActive(true);

                Vector3 portalPos = portal.transform.position;
                portalPos = hit2D.point;
                portalPos.z = 12;
                portal.transform.position = portalPos;

                portal.transform.rotation = Quaternion.LookRotation(hit2D.normal);
                Vector2 euler = portal.transform.eulerAngles;
                if (hit2D.normal.x < 0)
                    euler.y -= 11;
                else
                    euler.x -= 11;
                portal.transform.eulerAngles = euler;
            }
        }
    }
}
