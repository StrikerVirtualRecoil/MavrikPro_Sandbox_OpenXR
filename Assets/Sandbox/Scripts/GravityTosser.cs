using System.Collections;
using System.Collections.Generic;
using StrikerLink.Unity.Runtime.HapticEngine;
using StrikerLink.Unity.Runtime.Core;
using UnityEngine;

public class GravityTosser : MonoBehaviour
{
    public StrikerDevice mavrik;

    public Transform firePoint;

    public Rigidbody currentRB;
    public GameObject currentObj;
    public GameObject lastObject;


    public RaycastHit grapplePoint;

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
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            lastObject = gameObject;
        }
    }

    void ClearHighlighted()
    {
        if (lastObject != null || currentRB == null || !currentObj.CompareTag("Gravitable"))
        {
            
            lastObject.transform.GetChild(0).gameObject.SetActive(false);
            currentObj = null;
            lastObject = null;
        }

        if (currentObj != null)
        {
            currentObj.transform.GetChild(0).gameObject.SetActive(false);
        }

    }

    void HighlightObjectInRay()
    {
        // Check if we hit something.
        if (Physics.Raycast(firePoint.position, firePoint.forward, out grapplePoint))
        {

            // Get the object that was hit.

            if (grapplePoint.rigidbody != null)
            {
                currentRB = grapplePoint.rigidbody;
            }
            
           
            currentObj = currentRB.gameObject;
            


            if (currentRB.gameObject != null)
            {
                HighlightObject(currentRB.gameObject);
            }

            if (!grapplePoint.rigidbody.gameObject.CompareTag("Gravitable") || grapplePoint.rigidbody == null)
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
            if (mavrik.GetTriggerDown())
            {
                isGrappling = true;

                particles.Play();


                Vector3 grappleDirection = (transform.position - grapplePoint.point);

                if (currentRB != null)
                {
                    currentRB.isKinematic = false;
                    currentRB.velocity = grappleDirection.normalized * grappleSpeed;
                    
                    particleParent.SetActive(true);
                    mavrik.FireHaptic(gravityPull);
                    InvokeRepeating("GravityPull", 1f, 1f);
                }

            }
        

    }

    public void DoGrappling()
    {
        if (isGrappling)
        {
            //Get Vector between player and grappling point
            Vector3 grappleDirection = (grapplePoint.point - transform.position);

            // With this you can determine if you are overshooting your target
            if (currentRB != null && currentObj == lastObject && distance < grappleDirection.magnitude)
            {
                currentRB.gameObject.transform.SetParent(firePoint.transform);
            }
            else
                //Calculate distance between player and grappling point
                distance = grappleDirection.magnitude;
        }
    }


    public void GrapplingTriggerUp()
    {
        //turn grappling mode off when the button is released
        if (currentRB != null && mavrik.GetTriggerUp())
        {
            Debug.Log("this is my parent: " + currentRB.gameObject.transform.parent);

            isGrappling = false;

            particles.Stop();
            CancelInvoke("GravityPull");

            currentRB.isKinematic = false;
            currentRB.gameObject.transform.SetParent(null);
            mavrik.FireHaptic(gravityPush);
            currentRB.gameObject.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * 5000);

        }
        else if(currentRB == null)
        {
            CancelInvoke("GravityPull");
        }

        particleParent.SetActive(false);
        firePoint.transform.DetachChildren();

    }

    public void GravityPull()
    { 
        mavrik.FireHaptic(gravityPull);
    }
}
