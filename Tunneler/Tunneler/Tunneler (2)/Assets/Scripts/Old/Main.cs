using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public Transform segment;
    public GameObject ship_object;
    public Vector3 pos = new Vector3(0, 0, 0);
    public Vector3 next_position;

    private float rx, ry;
    private int depth;

    // Use this for initialization
    void Start () {

        /*
        // Test box - testing circle generation
        Circle TunnelSection = new Circle(25, 20, 10, segment);
        */
        // Spawn and position ship
        GameObject ship = Instantiate(ship_object, new Vector3(0, 0, 0), Quaternion.identity);
        ship.name = "Ship";

        // Test tunnel generation

        depth = 2;

        rx = depth*0.1f;
        ry = depth*0.1f;

        // Init next_position
        next_position = pos + new Vector3(0.1f, 0.1f, depth-1f);

        MonoBehaviour.print(next_position);

        for (int i = 0; i < 20; ++i)
        {

            Circle TunnelSection = new Circle(pos, next_position, 25, 100, depth, segment);

            pos = next_position;
            next_position = pos + new Vector3(0.1f*i, 0.1f*i, depth - 1f);

            MonoBehaviour.print(next_position);

        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
