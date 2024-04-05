using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdPersonRotation : MonoBehaviour
{

    public Transform roatationTrans;
    public float rotationSpeed = 5.0f;

    void Update()
    {
        // Get horizontal input to rotate the player
        float horizontalInput = Input.GetAxis("Mouse X");

        // Calculate rotation amount based on input and rotation speed
        float horizontalRotation = horizontalInput * rotationSpeed * Time.deltaTime;

        // Get vertical input to rotate the player
        float verticalInput = Input.GetAxis("Mouse Y");

        // Calculate rotation amount based on input and rotation speed
        float verticalRotation = -verticalInput * rotationSpeed * Time.deltaTime; // Negative to invert vertical rotation

        // Rotate the player around the Y-axis (horizontal rotation)
        // Rotate the player around the X-axis (vertical rotation)
        roatationTrans.Rotate(verticalRotation, 0, horizontalRotation, Space.World); // Use Space.World to apply global rotation
    }

}
