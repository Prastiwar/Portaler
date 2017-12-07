using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Player
{
    public Transform playerTransform;
    public int score;
}
public class GameState : MonoBehaviour
{
    Player _player;
    public static bool isSpotted = false;
    [SerializeField] ScriptableWeapon weapon;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject Portal_1;
    [SerializeField] GameObject Portal_2;

    public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _player.playerTransform = GetComponent<Transform>();

        // Temporary
        Portal_1.SetActive(false);
        Portal_2.SetActive(false);
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

    public void SetPortal(GameObject portal)
    {
        if (!portal.activeSelf)
            portal.SetActive(true);

        Vector3 mousePosition = Input.mousePosition;
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Debug.DrawLine(firePoint.position, mouseWorld * weapon.Distance, Color.cyan, 2f);
        RaycastHit2D hit2D = Physics2D.Raycast(firePoint.position, mouseWorld, weapon.Distance);
        

        if (hit2D.collider != null)
        {
            if (hit2D.collider.gameObject.layer == 8)
            {
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
