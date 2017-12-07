using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [SerializeField] GameObject otherPortal;
    public bool isPortalOn;

    void OnEnable()
    {
        isPortalOn = true;
    }

    void OnDisable()
    {
        isPortalOn = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 2 && otherPortal.GetComponent<PortalManager>().isPortalOn)
        {
            Rigidbody2D otherRigidBody = other.GetComponent<Rigidbody2D>();
            Vector3 exitVelocity = otherPortal.transform.forward * otherRigidBody.velocity.magnitude;
            otherRigidBody.velocity = exitVelocity;
            other.transform.position = otherPortal.transform.position + otherPortal.transform.forward * 2;
        }
    }
}
