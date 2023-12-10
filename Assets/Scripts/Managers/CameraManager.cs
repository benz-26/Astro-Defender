using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target; // Reference to the character's transform

    public Vector2 minPosition; // Minimum camera position
    public Vector2 maxPosition; // Maximum camera position

    public float smoothTime = 0.3f; // Smoothing time for camera movement
    private Vector3 velocity = Vector3.zero; // Velocity for smoothing

    private void LateUpdate()
    {
        if (target != null)
        {
            // Get the target's position
            Vector3 targetPosition = target.position;

            // Ensure the target stays within the defined bounds
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            // Calculate the desired camera position
            Vector3 desiredPosition = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);

            // Smoothly move the camera towards the desired position
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        }
    }
}
