using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All player statistics
public struct Player
{
    public Transform playerTransform;
    public int score;
    public int money;
}

public class GameState : MonoBehaviour
{
    public static Player _player;
    public static bool isSpotted = false;
    public static int lvlIndex = 0;
    public static int weaponIndex = 0;

    [SerializeField] ScriptableWeapon[] weapon;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject[] levels;

    static GameObject Portal_1;
    static GameObject Portal_2;

    public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        string _PauseState = "Pause State";
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(_PauseState))
            return;

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

        if (isSpotted)
            GameOver();
    }

    public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }

    void GameOver()
    {
        StateMachineManager.ChangeSceneTo("Result");
    }

    // What to do when player've went into portal
    public static void OnPortalEnter(Collider2D other, GameObject portal)
    {
        GameObject _otherPortal = portal.GetComponent<PortalManager>().isItFirstPortal ? Portal_2 : Portal_1;

        if (_otherPortal.activeSelf)
        {
            Rigidbody2D otherRigidBody = other.GetComponent<Rigidbody2D>();
            Vector3 exitVelocity = _otherPortal.transform.forward * otherRigidBody.velocity.magnitude;

            otherRigidBody.position = portal.transform.position; // get into portal feeling
            otherRigidBody.velocity = exitVelocity;
            other.transform.position = _otherPortal.transform.position + _otherPortal.transform.forward * 2; // throw away rigidbody from other portal
        }
    }

    // Instantiate level by index
    void SetLevel()
    {
        //if (levels[lvlIndex] != null)
            //Debug.Log("No more levels, if u see this call developer!");
        //else
            //GameObject _level = Instantiate(levels[lvlIndex]);

        _player.playerTransform = PlayerMotor.instance.player;
        Portal_1 = Instantiate(weapon[weaponIndex].portal_1);
        Portal_2 = Instantiate(weapon[weaponIndex].portal_2);
        weapon[weaponIndex].ammo = weapon[weaponIndex].maxAmmo;

        Debug.Log("Level was instantied");
    }

    // Active and setting portal position on wall
    public void SetPortal(GameObject portal)
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = (mouseWorld - firePoint.position).normalized;
        Debug.DrawRay(firePoint.position, direction * 100, Color.cyan, 1.3f);

        RaycastHit2D hit2D = Physics2D.Raycast(firePoint.position, direction, weapon[weaponIndex].distance);
        weapon[weaponIndex].ammo--;

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
