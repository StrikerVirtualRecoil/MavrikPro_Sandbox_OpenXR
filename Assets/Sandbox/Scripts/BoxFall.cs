using System.Collections;
using System.Collections.Generic;
using StrikerLink.Unity.Runtime.Core;
using StrikerLink.Unity.Runtime.HapticEngine;
using UnityEngine;

public class BoxFall : MonoBehaviour
{
    public AudioSource impact;
    public AudioSource fall;
    public HapticEffectAsset groundRumble;

    public StrikerDevice mavrik;
    public GameObject breakable;

    private int hit;

    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;
    private MeshRenderer mesh;

    public float hoverVal;

    public bool strikerCube;


    void Awake()
    {
        impact = gameObject.transform.GetChild(0).GetComponent<AudioSource>();
        fall = gameObject.transform.GetChild(1).GetComponent<AudioSource>();
        hit = 0;
    }

    void Start()
    {
        if (strikerCube)
        {
            mat1 = Resources.Load<Material>("Striker_Logo");
        }else
        {
            mat1 = Resources.Load<Material>("Scuffed_metal_blue_dall_e");
        }
        
        mat2 = Resources.Load<Material>("Scuffed_metal_green_dall_e");
        mat3 = Resources.Load<Material>("Scuffed_metal_yellow_dall_e");
        mat4 = Resources.Load<Material>("Scuffed_metal_red_dall_e");
        mesh = gameObject.GetComponent<MeshRenderer>();

        hoverVal = Random.Range(0.01f, 0.0f);
    }

    void Update()
    {
        gameObject.transform.position +=  Vector3.up * hoverVal * Mathf.Cos(Time.time);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Tennis") || collision.gameObject.CompareTag("UFO") || collision.gameObject.CompareTag("Cow"))
        {
            hit++;
            if (hit == 1)
            {
                mesh.material = mat2;
            }
            else if(hit == 2)
            {
                mesh.material = mat3;
            }
            else if (hit == 3)
            {
                mesh.material = mat4;
            }
            if (hit == 4)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                fall.Play();
            }
        }
        else if (collision.gameObject.tag == "Plunger")
        {
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
        }
        else 
        {
            GameObject breakableCube = Instantiate(breakable, transform.position, new Quaternion(0, 0, 0, 0));
            gameObject.transform.SetParent(null);
            //Destroy(gameObject);
            gameObject.SetActive(false);
            
            mavrik.FireHaptic(groundRumble);
            impact.Play();

        }



        
    }
}
