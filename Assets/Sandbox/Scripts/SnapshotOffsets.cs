using System.Collections;
using System.Collections.Generic;
using StrikerLink.Unity.Runtime.Core;
using UnityEngine;

public class SnapshotOffsets : MonoBehaviour
{
    private StrikerDevice strikerDevice;
    private OffsetAdjuster offsets;

    private bool isPositionFrozen = false;
    private Vector3 worldPositionWhenFrozen;
    private Quaternion worldRotationWhenFrozen;
    private MeshRenderer mesh;

    public Transform blasterTransform; 
    public Transform anchorTransform;
    public Transform muzzleTransform;

    public GameObject targetObject; 

    public Material mat1;
    public Material mat2;

    [HideInInspector]
    public Vector3 localPositionOffset;
    [HideInInspector]
    public Quaternion localRotationOffset;


    void Start()
    {
        localPositionOffset = new(0, 0, 0);
        localRotationOffset = new(0, 0, 0, 0);
        strikerDevice = gameObject.GetComponent<StrikerDevice>();
        offsets = gameObject.GetComponent<OffsetAdjuster>();

        mesh = targetObject.GetComponent<MeshRenderer>();
        mat1 = Resources.Load<Material>("Charcoal");
        mat2 = Resources.Load<Material>("Striker_Logo");
    }

    void Update()
    {
        if (!isPositionFrozen)
        {
            // Cast a ray forward from the muzzle
            if (Physics.Raycast(muzzleTransform.position, muzzleTransform.forward, out RaycastHit hit))
            {
                // Highlight the target object when hit by the raycast
                if (hit.collider.gameObject == targetObject)
                {
                    mesh.material = mat2; // Change to highlighted material

                    // Check if the ray hits the target object and the trigger is pulled
                    if (strikerDevice.GetTriggerDown())
                    {
                        StorePosition();
                    }
                }
                else
                {
                    mesh.material = mat1; // Revert to default material
                }
            }
        }
        else
        {
            // While position is frozen, keep blaster in the same world position
            blasterTransform.position = worldPositionWhenFrozen;
            blasterTransform.rotation = worldRotationWhenFrozen;

            // Wait for the user to pull the trigger again to lock the new position
            if (strikerDevice.GetTriggerDown())
            {
                LockPosition();
            }
        }
    }

    void StorePosition()
    {
        isPositionFrozen = true;
        // Store the world position and rotation to keep the blaster frozen in place
        worldPositionWhenFrozen = blasterTransform.position;
        worldRotationWhenFrozen = blasterTransform.rotation;
    }

    void LockPosition()
    {
        isPositionFrozen = false;
        // Calculate the local offset based on the world position when frozen
        // Convert the world position to the local position relative to the anchor (parent object)
        Vector3 localPositionOffset = anchorTransform.InverseTransformPoint(worldPositionWhenFrozen);
        Quaternion localRotationOffset = Quaternion.Inverse(anchorTransform.rotation) * worldRotationWhenFrozen;

        // Apply the local offsets to make the adjustments relative to the parent object
        blasterTransform.localPosition = localPositionOffset;
        blasterTransform.localRotation = localRotationOffset;
        offsets.SaveOffsets();
    }
}



