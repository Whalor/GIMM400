using UnityEngine;

// A very simplistic car driving on the x-z plane.

public class Drive : MonoBehaviour {
    // Tank speed
    public float speed = 10.0f;
    // Tank rotation speed
    public float rotationSpeed = 100.0f;
    // Public GameObject to store the fuel in
    public GameObject fuel;

    // Bool to turn AutoPilot On/Off
    bool autoPilot = false;

    void Start() {

    }

    // Calculate the vector to the fuel
    void CalculateAngle() {

        // Tanks foward facing vector
        Vector3 tF = this.transform.up;
        // Vector to the fuel
        Vector3 fD = fuel.transform.position - this.transform.position;

        // Calculate the dot product
        float dot = tF.x * fD.x + tF.y * fD.y;
        float angle = Mathf.Acos(dot / (tF.magnitude * fD.magnitude));

        // Output the angle to the console
        Debug.Log("Angle: " + angle * Mathf.Rad2Deg);
        // Output Unitys angle
        Debug.Log("Unity Angle: " + Vector3.Angle(tF, fD));

        // Draw a ray showing the tanks forward facing vector
        Debug.DrawRay(this.transform.position, tF * 10.0f, Color.green, 2.0f);
        // Draw a ray showing the vector to the fuel
        Debug.DrawRay(this.transform.position, fD, Color.red, 2.0f);

        int clockwise = 1;

        // Check the z value of the crossproduct and negate the direction if less than 0
        if (Cross(tF, fD).z < 0.0f)
            clockwise = -1;

        // Use Unity to work out the angle for you
        // float unityAngle = Vector3.SignedAngle(tF, fD, this.transform.forward);

        // Get the tank to face the fuel
        // this.transform.Rotate(0.0f, 0.0f, unityAngle);

        // Use our rotation
        this.transform.Rotate(0.0f, 0.0f, (angle * clockwise * Mathf.Rad2Deg) * 0.02f);
    }

    // Calculate the Cross Product
    Vector3 Cross(Vector3 v, Vector3 w) {

        float xMult = v.y * w.z - v.z * w.y;
        float yMult = v.z * w.x - v.x * w.z;
        float zMult = v.x * w.y - v.y * w.x;

        Vector3 crossProd = new Vector3(xMult, yMult, zMult);
        return crossProd;
    }

    // Calculate the distance from the tank to the fuel
    float CalculateDistance() {

        // Tank position
        Vector3 tP = this.transform.position;
        // Fuel position
        Vector3 fP = fuel.transform.position;

        // Calculate the distance using pythagoras
        float distance = Mathf.Sqrt(Mathf.Pow(tP.x - fP.x, 2.0f) +
                         Mathf.Pow(tP.y - fP.y, 2.0f) +
                         Mathf.Pow(tP.z - fP.z, 2.0f));

        // Calculate the distance using Unitys vector distance function
        float unityDistance = Vector3.Distance(tP, fP);

        // Print out the two results to the console
        Debug.Log("Distance: " + distance);
        Debug.Log("Unity Distance: " + unityDistance);

        // Return the calculated distance
        return distance;
    }

    float autoSpeed = 0.1f;
    void AutoPilot() {

        // Face the fuel tank
        CalculateAngle();
        // Move the tank in it's forward direction
        this.transform.Translate(this.transform.up * autoSpeed, Space.World);
    }

    void Update() {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, translation, 0);

        // Rotate around our y-axis
        transform.Rotate(0, 0, -rotation);

        // Check for the spacebar being pressed
        if (Input.GetKeyDown(KeyCode.Space)) {

            // If pressed then cal CalculateDistance method
            CalculateDistance();
            // Call CalculateAngle method
            CalculateAngle();
        }

        // Check if the T key has been pressed
        if (Input.GetKeyDown(KeyCode.T)) {

            // Flip the value of autoPilot
            autoPilot = !autoPilot;
        }

        // Check if autoPilot is enabled
        if (autoPilot) {

            // If yes and distance is greater than 5 then execute AutoPilot method
            if (CalculateDistance() > 5.0f) {

                AutoPilot();
            }
        }

    }
}