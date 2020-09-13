using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;
    public float rotationSpeed;
    public GameObject tankGun;
    public float rotateSpeed;
    public GameObject floor;

    private Vector3 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        //get tankGun
        tankGun = this.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //get horizontal movement from a and d keys
        float horizInput = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(horizInput, 0, 0) * moveSpeed * Time.deltaTime);

        //get forward movement from s and w keys
        float forwardInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(0, 0, forwardInput) * moveSpeed * Time.deltaTime);

        //figure out the direction the mouse is pointing
        mouseToWorldPosition();
        Vector3 directionToFace = mousePosition - transform.position;
        Debug.DrawRay(transform.position, directionToFace, Color.green);
        Quaternion targetRotation = Quaternion.LookRotation(directionToFace);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

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
