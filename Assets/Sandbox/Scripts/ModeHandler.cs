using System.Collections;
using System.Collections.Generic;
using StrikerLink.Shared.Devices.DeviceFeatures;
using StrikerLink.Unity.Runtime.Core;
using StrikerLink.Unity.Runtime.HapticEngine;
using StrikerLink.Unity.Runtime.Samples;
using TMPro;
using UnityEngine;

public class ModeHandler : MonoBehaviour
{
    public StrikerDevice mavrik;

    public MavrikController mc;

    private DeviceButton sideRight, sideLeft;

    public GameObject classic, gravity, ballzooka, crossbeaux, plasma;

    public GravityTosser gravityTosser;

    public TextMeshProUGUI tmp;

    public HapticEffectAsset blaster_swap;

    

    public int fireMode;
    public int fireModeMax;


    // Start is called before the first frame update
    void Start()
    {
        fireMode = 0;
        fireModeMax = 5;

        sideRight = DeviceButton.SideRight;
        sideLeft = DeviceButton.SideLeft;

        StartCoroutine(FadeTMP());
    }

    // Update is called once per frame
    void Update()
    {
        FireMode();
    }

    void FireMode()
    {
        if (mavrik.GetButtonDown(sideRight) || Input.GetKeyDown("right"))
        {
            if (fireMode < fireModeMax)
            {
                fireMode++;
                ModeSwap(); 
            }
            else
            {
                fireMode = 0;
                ModeSwap();
            }
        }

        if (mavrik.GetButtonDown(sideLeft) || Input.GetKeyDown("left"))
        {
            if (fireMode > 0)
            {
                fireMode--;
                ModeSwap();
            }
            else
            {
                fireMode = fireModeMax;
                ModeSwap();
            }
        }

        if (fireMode == 0 || fireMode == 1)
        {
            classic.SetActive(true);
        }
        else
        {
            classic.SetActive(false);
        }


        if (fireMode == 0)
        {
            mc.autoMode = false;
            tmp.text = "Mavrik Blaster";
        }

        if (fireMode == 1)
        {
            mc.autoMode = true;
            tmp.text = "Full Auto Blaster";
        }

        if (fireMode == 2)
        {
            ballzooka.SetActive(true);
            tmp.text = "Ballzooka";
        }
        else
        {
            ballzooka.SetActive(false);
        }

        if (fireMode == 3)
        {
            crossbeaux.SetActive(true);
            tmp.text = "Explosive Crossbow";
        }
        else
        {
            crossbeaux.SetActive(false);
        }

        if (fireMode == 4)
        {
            gravity.SetActive(true);
            tmp.text = "Gravity Blaster";
        }
        else
        {
            gravity.SetActive(false);
        }

        if (fireMode == 5)
        {
            plasma.SetActive(true);
            tmp.text = "Plasma Blaster";
        }
        else
        {
             plasma.SetActive(false);
        }
    }

    private IEnumerator FadeTMP()
    {
        tmp.alpha = 255f;
        yield return new WaitForSeconds(2.0f);
        tmp.alpha = 0;
    }

    private void ModeSwap()
    {
        StopCoroutine(FadeTMP());
        StartCoroutine(FadeTMP());
        mavrik.FireHaptic(blaster_swap);
    }
}
