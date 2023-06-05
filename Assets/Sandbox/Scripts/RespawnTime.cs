using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RespawnTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Respawn), 30, 30);
    }

    private void Respawn()
    {
        Destroy(this.gameObject);

        GameObject instance = Instantiate(Resources.Load("Destructables", typeof(GameObject))) as GameObject;
    }
}
