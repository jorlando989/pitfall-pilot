using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ShipSwap class
 * Handles ship model switching
 */ 
public class ShipSwap : MonoBehaviour {

    public GameObject ship;
    public GameObject alternateShip;

    public GameObject active;

    public void Start()
    {
        active = ship;
    }

    public void SwapToBasicShip()
    {
        if (!active.Equals(ship))
        {
            alternateShip.SetActive(false);
            ship.SetActive(true);

            active = ship;
        }
    }

    public void SwapToAlternateShip()
    {
        if (!active.Equals(alternateShip))
        {
            ship.SetActive(false);
            alternateShip.SetActive(true);

            active = alternateShip;
        }
    }

}
