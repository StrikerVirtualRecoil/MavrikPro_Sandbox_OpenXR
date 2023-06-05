using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeExplode : MonoBehaviour
{
    public GameObject breakable;
    public GameObject plasmafied;

    public bool prisms;

    private int radius = 5;
    private int power = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Bullet") && breakable != null)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;

            GameObject breakableCube = Instantiate(breakable, transform.position, new Quaternion(0, 0, 0, 0));

            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
            }

            Destroy(gameObject);
            Destroy(breakableCube, 5);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plasma") && plasmafied != null)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;

            GameObject plasmafiedCube = Instantiate(plasmafied, transform.position, new Quaternion(0, 0, 0, 0));

            Destroy(gameObject);
            Destroy(plasmafiedCube, 5);

        }
    }
}
