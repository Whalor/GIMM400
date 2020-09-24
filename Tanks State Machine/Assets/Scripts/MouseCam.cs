using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCam : MonoBehaviour
{
    public GameObject target;
    public GameObject cannonHead;
    public float rotateSpeed = 5;
    Vector3 offset;

    void Start()
    {
        //this offsets the camera behind the tank.
        offset = target.transform.position - transform.position;
    }

    void LateUpdate()
    {
        //This find the horizontal speed
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        target.transform.Rotate(0, horizontal, 0);
        cannonHead.transform.Rotate(-vertical, 0, 0);

        //This creates a good angle for the viewport
        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * offset);

        transform.LookAt(target.transform);
    }
}
