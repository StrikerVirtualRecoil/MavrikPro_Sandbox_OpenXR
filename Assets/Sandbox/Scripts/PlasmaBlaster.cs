using System.Collections;
using System.Collections.Generic;
using StrikerLink.Unity.Runtime.Core;
using StrikerLink.Unity.Runtime.HapticEngine;
using UnityEngine;

public class PlasmaBlaster : MonoBehaviour
{
    public StrikerDevice mavrik;

    
    private ParticleSystem mavrikPlasma;

    [Header("Particles")]
    //private ParticleSystem.MainModule mainPlasma;
    //public GameObject plasmaParent;
    public Transform plasmaField;

    private int plasmaSize;

    public Transform firePoint;

    [Header("Projectiles")]

    public Transform projectilePlasma1;
    public Transform projectilePlasma2;
    public Transform projectilePlasma3;
    public Transform projectilePlasma4;
    public Transform projectilePlasma5;

    [Header("Haptics")]

    public HapticEffectAsset charge1;
    public HapticEffectAsset charge2;
    public HapticEffectAsset charge3;
    public HapticEffectAsset charge4;
    public HapticEffectAsset charge5;

    public HapticEffectAsset charge_shot1;
    public HapticEffectAsset charge_shot2;
    public HapticEffectAsset charge_shot3;
    public HapticEffectAsset charge_shot4;
    public HapticEffectAsset charge_shot5;

    [Header("SFX")]

    public AudioSource plasmaCharge1;
    public AudioSource plasmaCharge2;
    public AudioSource plasmaCharge3;
    public AudioSource plasmaCharge4;
    public AudioSource plasmaCharge5;

    public AudioSource plasmaBlast1;
    public AudioSource plasmaBlast2;
    public AudioSource plasmaBlast3;
    public AudioSource plasmaBlast4;
    public AudioSource plasmaBlast5;

    // Start is called before the first frame update
    void Start()
    {
        mavrikPlasma = plasmaField.GetChild(0).GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        FiringHandler();
    }

    void Shoot()
    {
        switch (plasmaSize)
        {
            case 1:
                Instantiate(projectilePlasma1, firePoint.position, firePoint.rotation).GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 167f));
                mavrik.FireHaptic(charge_shot1);
                plasmaBlast1.Play(0);
                plasmaCharge1.Stop();
                break;

            case 2:
                Instantiate(projectilePlasma2, firePoint.position, firePoint.rotation).GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 625f));
                mavrik.FireHaptic(charge_shot2);
                plasmaBlast2.Play(0);
                plasmaCharge2.Stop();
                break;

            case 3:
                Instantiate(projectilePlasma3, firePoint.position, firePoint.rotation).GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 1250f));
                mavrik.FireHaptic(charge_shot3);
                plasmaBlast3.Play(0);
                plasmaCharge3.Stop();
                break;

            case 4:
                Instantiate(projectilePlasma4, firePoint.position, firePoint.rotation).GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 2500f));
                mavrik.FireHaptic(charge_shot4);
                plasmaBlast4.Play(0);
                plasmaCharge4.Stop();
                break;

            case 5:
                Instantiate(projectilePlasma5, firePoint.position, firePoint.rotation).GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 5000f));
                mavrik.FireHaptic(charge_shot5);
                plasmaBlast5.Play(0);
                plasmaCharge5.Stop();
                break;
        }
    }

    void FiringHandler()
    {
       

        if (mavrik.GetTriggerDown())
        {
            var plasmaVelocity = mavrikPlasma.velocityOverLifetime;
            plasmaVelocity.speedModifierMultiplier = 0.1f;

            var plasmaRadius = mavrikPlasma.shape;
            plasmaRadius.radius = .05f;
            Charging();
        }
        else if (mavrik.GetTriggerUp())
        {
            var plasmaVelocity = mavrikPlasma.velocityOverLifetime;
            plasmaVelocity.speedModifierMultiplier = 0.1f;

            var plasmaRadius = mavrikPlasma.shape;
            plasmaRadius.radius = .05f;
            Debug.Log("plasma blast size = " + plasmaSize);
            Shoot();
            CancelInvoke("Charge6");
            StopAllCoroutines();
            plasmaSize = 1;
        }
    }

    void Charging()
    {

        StartCoroutine(Charge1());
        StartCoroutine(Charge2());
        StartCoroutine(Charge3());
        StartCoroutine(Charge4());
        StartCoroutine(Charge5());

        InvokeRepeating("Charge6", 5.0f, 1.0f);
    }

    public IEnumerator Charge1()
    {
        yield return new WaitForSeconds(0.0f);
        var plasmaVelocity = mavrikPlasma.velocityOverLifetime;
        plasmaVelocity.speedModifierMultiplier = Mathf.MoveTowards(plasmaVelocity.speedModifierMultiplier, 1.0f, 1.0f);

        var plasmaShape = mavrikPlasma.shape;

        plasmaShape.radius = .05f;

        mavrik.FireHaptic(charge1);
        plasmaCharge1.Play(0);
        plasmaSize = 1;
    }

    public IEnumerator Charge2()
    {
        yield return new WaitForSeconds(1.0f);
        var plasmaVelocity = mavrikPlasma.velocityOverLifetime;
        plasmaVelocity.speedModifierMultiplier = Mathf.MoveTowards(plasmaVelocity.speedModifierMultiplier, 3.0f, 3.0f);

        var plasmaShape = mavrikPlasma.shape;

        plasmaShape.radius = .1f;

        mavrik.FireHaptic(charge2);
        plasmaCharge2.Play(0);
        plasmaSize = 2;
    }

    public IEnumerator Charge3()
    {
        yield return new WaitForSeconds(2f);
        var plasmaVelocity = mavrikPlasma.velocityOverLifetime;
        plasmaVelocity.speedModifierMultiplier = Mathf.MoveTowards(plasmaVelocity.speedModifierMultiplier, 9.0f, 9.0f);

        var plasmaShape = mavrikPlasma.shape;

        plasmaShape.radius = .15f;

        mavrik.FireHaptic(charge3);
        plasmaCharge3.Play(0);
        plasmaSize = 3;
    }

    public IEnumerator Charge4()
    {
        yield return new WaitForSeconds(3f);
        var plasmaVelocity = mavrikPlasma.velocityOverLifetime;
        plasmaVelocity.speedModifierMultiplier = Mathf.MoveTowards(plasmaVelocity.speedModifierMultiplier, 30.0f, 30.0f);

        var plasmaShape = mavrikPlasma.shape;

        plasmaShape.radius = .2f;

        mavrik.FireHaptic(charge4);
        plasmaCharge4.Play(0);
        plasmaSize = 4;
    }

    public IEnumerator Charge5()
    {
        yield return new WaitForSeconds(4f);
        var plasmaVelocity = mavrikPlasma.velocityOverLifetime;
        plasmaVelocity.speedModifierMultiplier = Mathf.MoveTowards(plasmaVelocity.speedModifierMultiplier, 70.0f, 30.0f);

        var plasmaShape = mavrikPlasma.shape;
        plasmaShape.radius = .25f;

        mavrik.FireHaptic(charge5);
        plasmaCharge5.Play(0);
        plasmaSize = 5;
    }

    void Charge6()
    {
        mavrik.FireHaptic(charge5);
        plasmaCharge5.Play(0);
    }
}
