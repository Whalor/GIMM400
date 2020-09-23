using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GunRotation : MonoBehaviour
{

    [SerializeField]
    public float rotationSpeed;
    public float rotateSpeed;
    public float camSpeed;

    public Transform Cam;

    private Vector3 mousePosition;

    // Update is called once per frame
    void Update()
    {
        //figure out the direction the mouse is pointing
        mouseToWorldPosition();
        Vector3 directionToFace = mousePosition - transform.position;
        Debug.DrawRay(transform.position, directionToFace, Color.green);
        Quaternion targetRotation = Quaternion.LookRotation(directionToFace);

        transform.localRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

    }

    void mouseToWorldPosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            mousePosition = hit.point;
        }
    }
}
