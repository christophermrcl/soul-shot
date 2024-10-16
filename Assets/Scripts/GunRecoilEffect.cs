using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRecoilEffect : MonoBehaviour
{
    public bool isActive = false;
    public Quaternion originalRotation;

    public float vibrationRange = 5f; // The range of vibration for the z-axis
    public float vibrationSpeed = 5f; // Speed of the vibration

    private float currentZVibration = 0f; // Track the current Z vibration

    void Start()
    {
        // Store the original rotation of the object
        originalRotation = transform.localRotation; // Using localRotation for relative rotation
    }

    void Update()
    {
        if (isActive)
        {
            // Calculate the new vibration effect on the z-axis
            float vibration = Mathf.Sin(Time.time * vibrationSpeed) * vibrationRange;

            // Apply the vibration effect relative to the current rotation
            // Remove the old vibration effect, apply the new one
            transform.localRotation = originalRotation * Quaternion.Euler(vibration - currentZVibration, 0, 0);

            // Update the currentZVibration tracker to the new value
            currentZVibration = vibration;
        }
        else
        {
            // Reset to the original rotation without additional vibration
            transform.localRotation = originalRotation;
            currentZVibration = 0f; // Reset the vibration tracker
        }
    }
}
