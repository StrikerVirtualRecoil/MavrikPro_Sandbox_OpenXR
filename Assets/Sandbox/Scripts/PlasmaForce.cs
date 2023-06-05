using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaForce : MonoBehaviour
{
    public float radius, radius1, radius2, radius3, radius4, radius5;

    public float power, power1, power2, power3, power4, power5;

    public bool charge1, charge2, charge3, charge4, charge5;


    void Start()
    {
        ExplosionLevelSet();

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        } 
    }

    void ExplosionLevelSet()
    {
        if (charge1)
        {
            radius = radius1;
            power = power1;
        }
        else if (charge2)
        {
            radius = radius2;
            power = power2;
        }
        else if (charge3)
        {
            radius = radius3;
            power = power3;
        }
        else if (charge4)
        {
            radius = radius4;
            power = power4;
        }
        else if (charge5)
        {
            radius = radius5;
            power = power5;
        }
    }
}
