using System.Collections;
using System.Collections.Generic;
using StrikerLink.Unity.Runtime.HapticEngine;
using StrikerLink.Unity.Runtime.Core;
using UnityEngine;

public class GravityTosser : MonoBehaviour
{
    public StrikerDevice mavrik;

    public Transform firePoint;

    private Rigidbody currentRB;
    private GameObject currentObj;
    private GameObject lastObject;

    private RaycastHit grapplePoint;

    bool isGrappling = false;

    float distance;

    public float grappleSpeed = 5f;

    public ParticleSystem particles;
    public GameObject particleParent;

    public HapticEffectAsset gravityPull, gravityPush;

    public int particleSpeed = 10;
    public bool flip;

    void Awake()
    {
        particles.Stop();
        flip = false;
    }

    void Update()
    {
        HighlightObjectInRay();
        GrapplingTriggerDown();
        GrapplingTriggerUp();
        DoGrappling();
    }

    void HighlightObject(GameObject gameObject)
    {
        if (lastObject != gameObject)
        {
            ClearHighlighted();
            if (gameObject != null)
            {
                Transform child = gameObject.transform.GetChild(0);
                if (child != null) child.gameObject.SetActive(true);
                lastObject = gameObject;
            }
        }
    }

    void ClearHighlighted()
    {
        if (lastObject != null)
        {
            Transform child = lastObject.transform.GetChild(0);
            if (child != null) child.gameObject.SetActive(false);
        }
        lastObject = null;
        currentObj = null;
    }

    void HighlightObjectInRay()
    {
        if (Physics.Raycast(firePoint.position, firePoint.forward, out grapplePoint))
        {
            Rigidbody hitRB = grapplePoint.rigidbody;

            if (hitRB != null && hitRB.gameObject.CompareTag("Gravitable"))
            {
                currentRB = hitRB;
                currentObj = hitRB.gameObject;
                HighlightObject(currentObj);
            }
            else
            {
                ClearHighlighted();
            }
        }
        else
        {
            ClearHighlighted();
        }
    }

    public void GrapplingTriggerDown()
    {
        if (mavrik.GetTriggerDown() && currentRB != null)
        {
            isGrappling = true;
            particles.Play();

            Vector3 grappleDirection = (transform.position - grapplePoint.point);
            currentRB.isKinematic = false;
            currentRB.velocity = grappleDirection.normalized * grappleSpeed;

            particleParent.SetActive(true);
            mavrik.FireHaptic(gravityPull);
            InvokeRepeating("GravityPull", 1f, 1f);
        }
    }

    public void DoGrappling()
    {
        if (isGrappling && currentRB != null)
        {
            Vector3 grappleDirection = (grapplePoint.point - transform.position);
            float currentDistance = grappleDirection.magnitude;

            if (currentDistance < distance)
            {
                currentRB.gameObject.transform.SetParent(firePoint.transform);
            }
            distance = currentDistance;
        }
    }

    public void GrapplingTriggerUp()
    {
        if (mavrik.GetTriggerUp())
        {
            if (currentRB != null)
            {
                Debug.Log("this is my parent: " + currentRB.gameObject.transform.parent);

                isGrappling = false;
                particles.Stop();
                CancelInvoke("GravityPull");

                currentRB.isKinematic = false;
                currentRB.gameObject.transform.SetParent(null);
                mavrik.FireHaptic(gravityPush);
                currentRB.AddForce(firePoint.transform.forward * 5000);
            }

            particleParent.SetActive(false);
            firePoint.transform.DetachChildren();
        }
    }

    public void GravityPull()
    {
        if (currentRB != null)
        {
            mavrik.FireHaptic(gravityPull);
        }
    }
}

