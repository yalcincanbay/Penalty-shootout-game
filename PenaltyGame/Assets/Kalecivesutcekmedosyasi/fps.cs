using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps : MonoBehaviour
{
    public float speed = 2.0f;
    public float mousesensitivity = 200.0f;
    private float xrotation = 0.0f;

    void Update()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(x, 0, z);

        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mousesensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mousesensitivity;
        xrotation -= mouseY;
        xrotation = Mathf.Clamp(xrotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(xrotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

    }
}
