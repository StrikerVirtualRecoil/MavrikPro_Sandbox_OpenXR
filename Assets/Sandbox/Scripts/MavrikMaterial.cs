using System.Collections;
using System.Collections.Generic;
using StrikerLink.Shared.Devices.DeviceFeatures;
using StrikerLink.Unity.Runtime.Core;
using UnityEngine;

public class MavrikMaterial : MonoBehaviour
{
    private DeviceRawSensor underBar0;
    private DeviceRawSensor underBar1;
    private DeviceRawSensor underBar2;
    private DeviceRawSensor underBar3;
    private DeviceRawSensor underBar4;
    private DeviceRawSensor underBar5;

    public StrikerDevice mavrik;

    public SkinnedMeshRenderer smr;

    // Start is called before the first frame update
    void Start()
    {
        underBar0 = DeviceRawSensor.UnderTouchpadGrip00;
        underBar1 = DeviceRawSensor.UnderTouchpadGrip01;
        underBar2 = DeviceRawSensor.UnderTouchpadGrip02;
        underBar3 = DeviceRawSensor.UnderTouchpadGrip03;
        underBar4 = DeviceRawSensor.UnderTouchpadGrip04;
        underBar5 = DeviceRawSensor.UnderTouchpadGrip05;

        smr = this.gameObject.GetComponent<SkinnedMeshRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mavrik.GetRawSensor(underBar0))
        {
            smr.material = Resources.Load<Material>("Mat_Mavrik_Body_White");
        }
    
        else if (mavrik.GetRawSensor(underBar1))
        {
             smr.material = Resources.Load<Material>("Mat_Mavrik_Body_Black");
        }
        else if (mavrik.GetRawSensor(underBar2))
        {
            smr.material = Resources.Load<Material>("Mat_Mavrik_Body_Blue");
        }
        else if (mavrik.GetRawSensor(underBar3))
        {
            smr.material = Resources.Load<Material>("Mat_Mavrik_Green");
        }
        else if (mavrik.GetRawSensor(underBar4))
        {
            smr.material = Resources.Load<Material>("Mat_Mavrik_Purple");
        }
        else if (mavrik.GetRawSensor(underBar5))
        {
            smr.material = Resources.Load<Material>("Mat_Mavrik_Red");
        }
    }
}
