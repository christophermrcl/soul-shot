using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouse : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    public bool isFiring = false;  // Boolean to control recoil effect
    public float recoilAmount = 0.1f;  // Amount of vertical recoil increment per shot
    public float horizontalRecoilAmount = 0.5f;  // Max horizontal recoil per shot
    public float recoilIncreaseRate = 0.1f;  // Rate at which vertical recoil increases
    public float recoilResetSpeed = 0.05f;  // Speed of recoil recovery when not firing

    private float xRotation = 0f;
    private float recoilOffsetX = 0f;  // Tracks the vertical recoil offset
    private float recoilOffsetY = 0f;  // Tracks the horizontal recoil offset

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook();  // Call function for mouse look
        ApplyRecoil();      // Call function to handle recoil
    }

    void HandleMouseLook()
    {
        // Get mouse movement input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Calculate base rotation from mouse input
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply vertical and horizontal rotation to the camera (mouse + recoil)
        transform.localRotation = Quaternion.Euler(xRotation - recoilOffsetX, recoilOffsetY, 0f);  // Subtracting recoilOffsetX to make recoil go up

        // Rotate the player body horizontally
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void ApplyRecoil()
    {
        if (isFiring)
        {
            // Increase vertical recoil over time (going upwards)
            recoilOffsetX += recoilIncreaseRate;

            // Add random horizontal recoil
            recoilOffsetY += Random.Range(-horizontalRecoilAmount, horizontalRecoilAmount);
        }
        else
        {
            // Gradually reset recoil when not firing
            recoilOffsetX = Mathf.Lerp(recoilOffsetX, 0f, recoilResetSpeed);
            recoilOffsetY = Mathf.Lerp(recoilOffsetY, 0f, recoilResetSpeed);
        }
    }
}
