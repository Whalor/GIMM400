using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    public float rotateSpeed = 5;
    Vector3 offset;

    private void Start()
    {
        offset = player.transform.position - transform.position;
    }

    void LateUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        player.transform.Rotate(0, horizontal, 0);

        float desiredAngle = player.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = player.transform.position - (rotation * offset);

        transform.LookAt(player.transform);
    }
}