using UnityEngine;
using System.Collections;

public class ThrowArcRenderer : MonoBehaviour
{
    //Wiki for the maths: https://en.wikipedia.org/wiki/Projectile_motion

    LineRenderer lr;

    public float velocity, angle;
    public int arcSegments;

    private float gForce, radianAngle;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        gForce = Mathf.Abs(Physics2D.gravity.y);

        RenderArc();
    }

    private void Update()
    {
        RenderArc();
    }

    private void RenderArc()
    {
        lr.positionCount = arcSegments;
        lr.SetPositions(CalculateArcArray());
    }

    //Create all the positions for an arc
    private Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[arcSegments + 1];

        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / gForce;

        for (int i = 0; i < arcSegments; i++)
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
        return new Vector3(x + transform.position.x, y + transform.position.y, transform.position.z);
    }
}
