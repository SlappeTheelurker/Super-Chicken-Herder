using UnityEngine;
using System.Collections;

public class ThrowArcMesh : MonoBehaviour
{
    //Wiki for the maths: https://en.wikipedia.org/wiki/Projectile_motion

    public float meshWidth, velocity, velocityPerTick, angle;
    public int arcSegments;

    private float gForce, radianAngle;
    private Mesh mesh;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        gForce = Mathf.Abs(Physics2D.gravity.y);

        RenderArc(CalculateArcArray());
    }

    private void Update()
    {
        if (GetComponent<MeshRenderer>().enabled && (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetKeyDown(KeyCode.E)))
        {
            velocity += velocityPerTick;
        }

        if (GetComponent<MeshRenderer>().enabled && (Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetKeyDown(KeyCode.Q)))
        {
            velocity -= velocityPerTick;
        }

        RenderArc(CalculateArcArray());
    }

    private void RenderArc(Vector3[] arcPoints)
    {
        mesh.Clear();
        Vector3[] vertices = new Vector3[(arcSegments + 1) * 2];
        int[] triangles = new int[arcSegments * 6 * 2]; //*2 so the arc also has triangles pointing downwards so you can see the arc from below

        for (int i = 0; i <= arcSegments; i++)
        {
            //Set right (even) vertices
            vertices[i * 2] = new Vector3(meshWidth * 0.5f, arcPoints[i].y, arcPoints[i].x);
            //Set left (odd) vertices
            vertices[i * 2 + 1] = new Vector3(meshWidth * -0.5f, arcPoints[i].y, arcPoints[i].x);

            //Set triangles
            if (i != arcSegments)
            {
                //Get first pionts and then next points in the segment
                //CLOCKWISE 
                triangles[i * 12] = i * 2;
                triangles[i * 12 + 1] = triangles[i * 12 + 4] = i * 2 + 1; 
                triangles[i * 12 + 2] = triangles[i * 12 + 3] = (i + 1) * 2;
                triangles[i * 12 + 5] = (i + 1) * 2 + 1;

                //COUNTER CLOCKWISE
                triangles[i * 12 + 6] = i * 2;
                triangles[i * 12 + 7] = triangles[i * 12 + 10] = (i + 1) * 2;
                triangles[i * 12 + 8] = triangles[i * 12 + 9] = i * 2 + 1;
                triangles[i * 12 + 11] = (i + 1) * 2 + 1;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateBounds();
        }
    }

    //Create all the positions for an arc
    private Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[arcSegments + 1];

        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / gForce;

        for (int i = 0; i <= arcSegments; i++)
        {
            float t = i / (float)arcSegments;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return arcArray;
    }

    //calculate height and distance of vertexes
    private Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = (x * Mathf.Tan(radianAngle)) - ((gForce * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        return new Vector3(x, y, 0);
    }
}
