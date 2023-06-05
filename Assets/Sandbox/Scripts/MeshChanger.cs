using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshChanger : MonoBehaviour
{
    public Material stdMat;
    public Material tennisMat;
    public Material rainbowMat;
    private SkinnedMeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        stdMat = Resources.Load<Material>("M_MavrikGun");
        tennisMat = Resources.Load<Material>("M_MavrikGunTennis");
        rainbowMat = Resources.Load<Material>("M_MavrikGunRainbow");
        mesh = gameObject.GetComponent<SkinnedMeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StandardBlaster")
        {
            mesh.material = stdMat;
        }
        else if (other.gameObject.tag == "TennisBlaster")
        {
            mesh.material = tennisMat;
        }
        else if (other.gameObject.tag == "RainbowBlaster")
        {
            mesh.material = rainbowMat;
        }
    }
}
