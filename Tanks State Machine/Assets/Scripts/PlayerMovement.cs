using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;
    public float rotationSpeed;
    public float rotateSpeed;
    public GameObject floor;
    public float fuelBurnRate;

    private Vector3 mousePosition;
    public Fuel playerFuel;

    private void Start()
    {
        playerFuel = this.GetComponent<Fuel>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerFuel.curFuel <= 0)
        {
            Debug.Log("Out of Fuel!");
        } else
        {
            //get horizontal movement from a and d keys
            if (Input.GetAxis("Horizontal") != 0)
            {
                float horizInput = Input.GetAxis("Horizontal");
                transform.Translate(new Vector3(horizInput, 0, 0) * moveSpeed * Time.deltaTime);
                playerFuel.RemoveFuel(fuelBurnRate * Time.deltaTime);
            }

            //get forward movement from s and w keys
            if (Input.GetAxis("Vertical") != 0)
            {
                float forwardInput = Input.GetAxis("Vertical");
                transform.Translate(new Vector3(0, 0, forwardInput) * moveSpeed * Time.deltaTime);
                playerFuel.RemoveFuel(fuelBurnRate * Time.deltaTime);
            }

        }

        //figure out the direction the mouse is pointing
        //mouseToWorldPosition();
        //Vector3 directionToFace = mousePosition - transform.position;
        //Debug.DrawRay(transform.position, directionToFace, Color.green);
        //Quaternion targetRotation = Quaternion.LookRotation(directionToFace);

        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

    }

        //id mouseToWorldPosition()
        //
        //  RaycastHit hit;
        //  Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //  if (Physics.Raycast(ray, out hit))
        //  {
        //      mousePosition = hit.point;
        //  }
        //
}
