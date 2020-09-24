using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    public float curFuel = 0;
    public float maxFuel = 100;

    public FuelBar fuelBar;

    // Start is called before the first frame update
    void Start()
    {
        curFuel = maxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            RemoveFuel(10);
        }
    }

    public void RemoveFuel(float fuel)
    {
        curFuel -= fuel;

        fuelBar.SetFuel(curFuel);
    }
}
