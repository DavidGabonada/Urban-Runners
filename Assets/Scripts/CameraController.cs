// using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player; // Assign this via Inspector
    [SerializeField] private float offsetX = 3.0f;
    [SerializeField] private float offsetY = 3.0f;
    [SerializeField] private float followSpeed = 10.0f; // Speed for smooth movement
    [SerializeField] private float cameraSize = 5.0f; // Camera size

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        if (cam != null)
        {
            cam.orthographicSize = cameraSize;
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate the desired position with offsets
            Vector3 desiredPosition = new Vector3(player.position.x + offsetX, player.position.y + offsetY, transform.position.z);

            // Smoothly move the camera towards the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        }

        // Update camera size dynamically if changed in the Inspector
        if (cam != null)
        {
            cam.orthographicSize = cameraSize;
        }
    }
}
