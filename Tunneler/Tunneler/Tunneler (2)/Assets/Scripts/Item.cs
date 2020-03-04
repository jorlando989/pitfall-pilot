using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Item class
 * Holds information on physcial GameObject, and is used to set the GameObject's position and rotation properties
 * Position(Pipe, float, float) places the item along the specified Pipe at a specified position and rotation
 */ 
public class Item : MonoBehaviour {

    private Transform rotater;

    private void Awake()
    {
        rotater = transform.GetChild(0);
    }

    public void Position (Pipe pipe, float curveRotation, float ringRotation)
    {
        transform.SetParent(pipe.transform, false);
        transform.localRotation = Quaternion.Euler(0f, 0f, -curveRotation);
        rotater.localPosition = new Vector3(0f, pipe.CurveRadius, 0f);
        rotater.localRotation = Quaternion.Euler(ringRotation, 0f, 0f);
    }
}
