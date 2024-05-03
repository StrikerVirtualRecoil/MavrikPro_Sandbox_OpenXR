using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[Serializable]
public class BlasterOffsets
{
    public Vector3 positionOffset;
    public Vector3 rotationOffset;
}

public partial class OffsetAdjuster : MonoBehaviour
{
    public BlasterOffsets currentOffsets;
    public SnapshotOffsets offsets;
    public Slider posXSlider, posYSlider, posZSlider, rotXSlider, rotYSlider, rotZSlider;
    public TMP_InputField posXInput, posYInput, posZInput, rotXInput, rotYInput, rotZInput;
    
    void Update()
    {
        posXInput.text = offsets.blasterTransform.localPosition.x.ToString();
        posYInput.text = offsets.blasterTransform.localPosition.y.ToString();
        posZInput.text = offsets.blasterTransform.localPosition.z.ToString();
        rotXInput.text = offsets.blasterTransform.localRotation.x.ToString();
        rotYInput.text = offsets.blasterTransform.localRotation.y.ToString();
        rotZInput.text = offsets.blasterTransform.localRotation.z.ToString();
    }

    void Start()
    {
        LoadOffsets(); // Attempt to load offsets from a previous session
        ApplyOffsetsToSliders();

        // Add listeners to the sliders
        posXSlider.onValueChanged.AddListener(value => AdjustPositionOffset(0, value));
        posYSlider.onValueChanged.AddListener(value => AdjustPositionOffset(1, value));
        posZSlider.onValueChanged.AddListener(value => AdjustPositionOffset(2, value));
        rotXSlider.onValueChanged.AddListener(value => AdjustRotationOffset(0, value));
        rotYSlider.onValueChanged.AddListener(value => AdjustRotationOffset(1, value));
        rotZSlider.onValueChanged.AddListener(value => AdjustRotationOffset(2, value));
    }

    void AdjustPositionOffset(int index, float value)
    {
        currentOffsets.positionOffset[index] = value;
        ApplyOffsets();
    }

    void AdjustRotationOffset(int index, float value)
    {
        currentOffsets.rotationOffset[index] = value;
        ApplyOffsets();
    }

    void ApplyOffsets()
    {
        // Apply the offsets to your blasterTransform here
        // Remember to convert rotationOffset from Euler angles to a Quaternion
        if (!offsets.blasterTransform || !offsets.anchorTransform) return;

        // Assuming the positionOffset is in local space of the anchor (parent object)
        offsets.blasterTransform.localPosition = currentOffsets.positionOffset;

        // Assuming the rotationOffset represents Euler angles in degrees
        offsets.blasterTransform.localRotation = Quaternion.Euler(currentOffsets.rotationOffset);
    }

    void ApplyOffsetsToSliders()
    {
        posXSlider.value = currentOffsets.positionOffset.x;
        posYSlider.value = currentOffsets.positionOffset.y;
        posZSlider.value = currentOffsets.positionOffset.z;
        rotXSlider.value = currentOffsets.rotationOffset.x;
        rotYSlider.value = currentOffsets.rotationOffset.y;
        rotZSlider.value = currentOffsets.rotationOffset.z;
    }

}

