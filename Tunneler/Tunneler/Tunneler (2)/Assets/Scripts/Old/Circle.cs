using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle {

    public Vector3 pos;
    public int radius;
    public int segments;
    public int depth;

    private List<Transform> segment_list; 

    // Constructor
    public Circle (Vector3 position_, Vector3 next_position, int radius_, int segments_, int depth_, Transform segment)
    {

        pos = position_;
        radius = radius_;
        segments = segments_;
        depth = depth_;

        segment_list = new List<Transform>();

        // Create GameObject to store segments
        GameObject circle = new GameObject
        {
            name = "Circle"
        };

        // Angle step
        float dTheta = (2f * Mathf.PI / segments);
        // Inside angle of polygon (circles don't technically have this, but this isn't a real circle)
        float insideAngle = ((180 - (180 * (segments - 2)) / segments) / 2) * Mathf.Deg2Rad;
        // Current position of segment, and position of the next segment. Used for calculating transform's size
        Vector3 nextPos;
        Vector3 thisPos;
        // Length of segment (distance between this position and next position)
        float length;

        // Generate x segments
        for (int i = 0; i < segments; ++i)
        {

            nextPos = new Vector3(radius * Mathf.Cos(dTheta * (i + 1)), radius * Mathf.Sin(dTheta * (i + 1)), 1);
            thisPos = new Vector3(radius * Mathf.Cos(dTheta * (i)), radius * Mathf.Sin(dTheta * (i)), 1);

            var rec = Object.Instantiate(segment);
            rec.name = i.ToString();
            rec.transform.SetParent(circle.transform);
            rec.transform.localPosition = thisPos;

            var dir = Vector3.zero - rec.transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rec.transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector3 thisPosShift = thisPos.normalized * (radius + rec.transform.localScale.x / 2);
            Vector3 nextPosShift = nextPos.normalized * (radius + rec.transform.localScale.x / 2);

            length = 2 * ((nextPosShift - thisPosShift).magnitude * Mathf.Sin(insideAngle)) / (Mathf.Sin((180 * (segments - 2) / segments) * Mathf.Deg2Rad));

            rec.transform.localScale = new Vector3(rec.transform.localScale.x, length, depth);

            // Add segments to list
            segment_list.Add(rec.transform);

        }

        circle.transform.position = pos;
        circle.transform.LookAt(next_position);

    }

    // Delete Circle
    public void Remove ()
    {

        // Iterate over list

        // Delete transform object

        // Clear list

        // Delete circle GameObject

    }
/*
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        float dTheta = (0.5f * Mathf.PI) / segments;
        float theta = (Mathf.PI / 4); // starting angle is pi/4

        Vector3 oldPos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f); // first point

        for (int i = 0; i < segments + 1; i++)
        {
            Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
            Gizmos.DrawLine(oldPos, transform.position + pos);

            oldPos = transform.position + pos;

            theta = theta + dTheta;
        }
    }
#endif
*/
}




