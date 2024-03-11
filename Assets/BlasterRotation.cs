
using StrikerLink.Unity.Runtime.Core;
using StrikerLink.Unity.Runtime.HapticEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

namespace StrikerLink.Unity.Runtime.Samples
{
    public class BlasterRotation : MonoBehaviour
    {
        public StrikerDevice strikerDevice;
        public Transform trans;
        public float smoothingFactor;
        private float smoothedXRotationDegrees = 0f;
        private float smoothedYRotationDegrees = 0f;
        public float offsetX;
        public float offsetY;
        //private float smoothedZRotationDegrees = 0f;

        private Queue<float> inputBuffer = new Queue<float>();
        private const int bufferSize = 5; // Adjust based on desired smoothing

        // Update is called once per frame
        void Update()
        {
            int rawPitchInput = (int)strikerDevice.GetAxis(Shared.Devices.DeviceFeatures.DeviceAxis.AccelX); // Accel Y Raw input
            int rawYawInput = (int)strikerDevice.GetAxis(Shared.Devices.DeviceFeatures.DeviceAxis.AccelZ); // Accel Z Raw input
            //int rawRollInput = (int)strikerDevice.GetAxis(Shared.Devices.DeviceFeatures.DeviceAxis.AccelX); // Accel X Raw input

            float normalizedPitch = NormalizeAccelerometerInput(rawPitchInput, offsetX); // Normalize it
            float normalizedYaw = NormalizeAccelerometerInput(rawYawInput, offsetY); // Normalize it
            //float normalizedRoll = NormalizeAccelerometerInput(rawRollInput); // Normalize it

            float movingAvgPitch = CalculateMovingAverage(normalizedPitch);
            float movingAvgYaw = CalculateMovingAverage(normalizedYaw);

            // Assuming you want a full tilt (from -1 to 1) to correspond to a 90-degree rotation
            float targetXRotationDegrees = movingAvgPitch * -360; // Scale the normalized value to degrees
            float targetYRotationDegrees = movingAvgYaw * 360; // Scale the normalized value to degrees
            //float targetZRotationDegrees = 0;


            // Clamp the rotation to ensure it's within 0 to -45 degrees
            float clampedRotationX = Mathf.Clamp(targetXRotationDegrees, -45, 0);
            float clampedRotationY = Mathf.Clamp(targetYRotationDegrees, -23, 23);


            // Apply exponential smoothing
            smoothedXRotationDegrees = Mathf.Lerp(smoothedXRotationDegrees, clampedRotationX, Time.deltaTime * smoothingFactor);
            smoothedYRotationDegrees = Mathf.Lerp(smoothedYRotationDegrees, clampedRotationY, Time.deltaTime * smoothingFactor);
            //smoothedZRotationDegrees = Mathf.Lerp(smoothedZRotationDegrees, targetZRotationDegrees, Time.deltaTime * smoothingFactor);

            

            // Apply to your object's rotation
            Quaternion newRotation = Quaternion.Euler(smoothedXRotationDegrees, smoothedYRotationDegrees, 0);
            trans.rotation = newRotation;
        }

        float NormalizeAccelerometerInput(int rawInput, float offset)
        {
            // Apply the offset to the raw input before normalization
            float adjustedInput = rawInput + offset;
            // Assuming 32768 is the midpoint for 0 acceleration
            // And the range is from 0 to 65535 (or 65556 based on your description)
            return (adjustedInput - 32768f) / 32768f; // This will give a range from -1 to 1
        }



        private float CalculateMovingAverage(float newValue)
        {
            if (inputBuffer.Count >= bufferSize)
            {
                inputBuffer.Dequeue(); // Remove the oldest value if at capacity
            }

            inputBuffer.Enqueue(newValue); // Add the new value

            return inputBuffer.Average(); // Calculate and return the average of the buffer
        }
    }
}
