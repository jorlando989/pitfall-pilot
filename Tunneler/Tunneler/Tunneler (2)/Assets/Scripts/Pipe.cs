using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Pipe class
 * Constructs a Pipe, or section of a PipeSystem, by creating a mesh of a segment of a torus. A torus is a donut shape, and has 2 radii. Google torus for more information and a visual.
 * On launch, the mesh is created
 * SetVertices() initializes vertices list and creates the mesh along the torus segment
 * CreateFirstQuadRing(float) creates the first inner circle along the torus segment, setting vertices for the mesh configuration
 * CreateQuadRing(float) uses assumptions from the first quad ring to iteratively create the next quad rings
 * SetTriangles() sets the triangle list used by the mesh object to calculate the mesh at runtime (sets the ordering)
 * GetPointOnTorus(float, float) returns a Vector3 of the world space coordinate of a point on the torus as defined by radius u and radius v
 * SetUV() sets the UV mapping of the mesh object. In our case, we set the UV mapping to be inside out, so the mesh is visible on the inside, but is not rendered when looking from the outside.
 * AlignWith(Pipe) repositions this Pipe object to align with the previous Pipe object
 * Generate(bool) utilizes private methods to create final Pipe object. Optional argument decides whether or not Item objects will be spawned within the Pipe object. Since Pipe objects are reused, this method also
 * removes all previously spawned Item objects before repositioning and recalculating the mesh object.
 */ 

 /*
  * Notes
  * v = angle along inner circles (quad ring)
  * u = angle along large curve (made up of smaller circles)
  * quad ring = inner circle of torus
  */ 
public class Pipe : MonoBehaviour
{

    public float pipeRadius;
    public int pipeSegmentCount;

    public float ringDistance;

    public float minCurveRadius, maxCurveRadius;
    public int minCurveSegmentCount, maxCurveSegmentCount;

    public ItemGenerator[] generators;
    private int curveSegmentCount;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uv;

    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Pipe";
    }

    private void SetVertices()
    {
        vertices = new Vector3[pipeSegmentCount * curveSegmentCount * 4];

        float uStep = ringDistance / CurveRadius;
        CurveAngle = uStep * curveSegmentCount * (360f / (2f * Mathf.PI));
        CreateFirstQuadRing(uStep);
        int iDelta = pipeSegmentCount * 4;
        for (int u = 2, i = iDelta; u <= curveSegmentCount; u++, i += iDelta)
        {
            CreateQuadRing(u * uStep, i);
        }
        mesh.vertices = vertices;
    }

    private void CreateFirstQuadRing(float u)
    {
        float vStep = (2f * Mathf.PI) / pipeSegmentCount;

        Vector3 vertexA = GetPointOnTorus(0f, 0f);
        Vector3 vertexB = GetPointOnTorus(u, 0f);
        for (int v = 1, i = 0; v <= pipeSegmentCount; v++, i += 4)
        {
            vertices[i] = vertexA;
            vertices[i + 1] = vertexA = GetPointOnTorus(0f, v * vStep);
            vertices[i + 2] = vertexB;
            vertices[i + 3] = vertexB = GetPointOnTorus(u, v * vStep);
        }
    }

    private void CreateQuadRing(float u, int i)
    {
        float vStep = (2f * Mathf.PI) / pipeSegmentCount;
        int ringOffset = pipeSegmentCount * 4;

        Vector3 vertex = GetPointOnTorus(u, 0f);
        for (int v = 1; v <= pipeSegmentCount; v++, i += 4)
        {
            vertices[i] = vertices[i - ringOffset + 2];
            vertices[i + 1] = vertices[i - ringOffset + 3];
            vertices[i + 2] = vertex;
            vertices[i + 3] = vertex = GetPointOnTorus(u, v * vStep);
        }
    }

    private void SetTriangles()
    {
        triangles = new int[pipeSegmentCount * curveSegmentCount * 6];
        for (int t = 0, i = 0; t < triangles.Length; t += 6, i += 4)
        {
            triangles[t] = i;
            triangles[t + 1] = triangles[t + 4] = i + 2;
            triangles[t + 2] = triangles[t + 3] = i + 1;
            triangles[t + 5] = i + 3;
        }
        mesh.triangles = triangles;
    }

    private Vector3 GetPointOnTorus(float u, float v)
    {
        Vector3 p;
        float r = (CurveRadius + pipeRadius * Mathf.Cos(v));
        p.x = r * Mathf.Sin(u);
        p.y = r * Mathf.Cos(u);
        p.z = pipeRadius * Mathf.Sin(v);
        return p;
    }

    private void SetUV()
    {
        uv = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i += 4)
        {
            uv[i] = Vector2.zero;
            uv[i + 1] = Vector2.right;
            uv[i + 2] = Vector2.up;
            uv[i + 3] = Vector2.one;
        }
        mesh.uv = uv;
    }

    /*
     * Public methods
     */ 

    public void AlignWith(Pipe pipe)
    {
        RelativeRotation =
            Random.Range(0, curveSegmentCount) * 360f / pipeSegmentCount;

        transform.SetParent(pipe.transform, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0f, 0f, -pipe.CurveAngle);
        transform.Translate(0f, pipe.CurveRadius, 0f);
        transform.Rotate(RelativeRotation, 0f, 0f);
        transform.Translate(0f, -CurveRadius, 0f);
        transform.SetParent(pipe.transform.parent);
        transform.localScale = Vector3.one;
    }

    public void Generate(bool withItems = true)
    {
        CurveRadius = Random.Range(minCurveRadius, maxCurveRadius);
        curveSegmentCount = Random.Range(minCurveSegmentCount, maxCurveSegmentCount + 1);
        mesh.Clear();
        SetVertices();
        SetUV();
        SetTriangles();
        mesh.RecalculateNormals();

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        if (withItems)
        {
            int randIndex = Random.Range(0, generators.Length);

            if (generators[randIndex])
            {
                generators[randIndex].GenerateItems(this);
            }
        }

    }

    public float CurveRadius { get; private set; }

    public float CurveAngle { get; private set; }

    public float RelativeRotation { get; private set; }

    public float CurveSegmentCount
    {
        get
        {
            return curveSegmentCount;
        }
    }
}