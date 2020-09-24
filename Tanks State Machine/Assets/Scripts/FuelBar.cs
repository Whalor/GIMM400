using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    public Slider fuelBar;
    public Fuel playerFuel;

    private void Start()
    {
        playerFuel = GameObject.FindGameObjectWithTag("Player").GetComponent<Fuel>();
        fuelBar = GetComponent<Slider>();
        fuelBar.maxValue = playerFuel.maxFuel;
        fuelBar.value = playerFuel.maxFuel;
    }

    public void SetFuel(float fp)
    {
        fuelBar.value = fp;
    }
}
