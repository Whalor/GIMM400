using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;
    public GameObject floor;

    // Update is called once per frame
    void Update()
    {
        //get horizontal movement from a and d keys
        float horizInput = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(horizInput, 0, 0) * moveSpeed * Time.deltaTime);

        //get forward movement from s and w keys
        float forwardInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(0, 0, forwardInput) * moveSpeed * Time.deltaTime);
    }
}
