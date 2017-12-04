using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform lookAt;
    public float distance, sensitivity, yAngleMin, yAngleMax, yPosOffset;

    private float currentX, currentY, sensitivityX, sensitivityY;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;

        sensitivityX = sensitivity;
        sensitivityY = sensitivity;
    }

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY -= Input.GetAxis("Mouse Y");

        currentY = Mathf.Clamp(currentY, yAngleMin, yAngleMax);
    }

    private void LateUpdate()
    {
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = lookAt.position + rotation * direction;
        transform.LookAt(lookAt.position);
        transform.position = new Vector3(transform.position.x, transform.position.y + yPosOffset, transform.position.z);
    }
}
