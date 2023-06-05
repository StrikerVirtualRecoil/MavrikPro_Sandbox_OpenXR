using System.Collections;
using System.Collections.Generic;
using StrikerLink.Unity.Runtime.HapticEngine;
using StrikerLink.Unity.Runtime.Core;
using UnityEngine;

public class BallDestroy : MonoBehaviour
{
    public float TimeToDestroy;
    public float tTime = 0;
    
    void Update()
    {
        tTime += Time.deltaTime;
        if(tTime > TimeToDestroy)
        {
            Debug.Log("Destroy"); 
            Destroy(gameObject);
        }
    }
}
