using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ItemGenerator class
 * Abstract Factory object for creating new obstacles and power ups
 */ 
public abstract class ItemGenerator : MonoBehaviour {

    public abstract void GenerateItems(Pipe pipe);

}
