using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 10;
    public Transform target;
    public float targetDst = 2;
    public Vector2 YMinMax = new Vector2(-40, 85);

    public float smoothTime = 0.12f;
    Vector3 smoothVelocity;
    Vector3 currentRotation;

    float mouseX;
    float mouseY;

    void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        mouseY = Mathf.Clamp(mouseY, YMinMax.x, YMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(mouseY, mouseX), ref smoothVelocity, smoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * targetDst;
    }


}