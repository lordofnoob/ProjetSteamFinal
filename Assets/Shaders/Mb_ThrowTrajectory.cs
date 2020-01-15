using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Mb_ThrowTrajectory : MonoBehaviour
{

    LineRenderer trajectory;

    public float velocity;
    public float angle;
    public int resolution;

    float gravity;
    float radianAngle;

    private void Awake()
    {
        trajectory = GetComponent<LineRenderer>();
        gravity = Mathf.Abs(Physics2D.gravity.y);
    }


    void Start()
    {
        RenderArc();
    }

    private void RenderArc()
    {
        trajectory.positionCount = resolution + 1;
        trajectory.SetPositions(CalculateArcArray());
    }

    private Vector3[] CalculateArcArray(){

        Vector3[] arcArray = new Vector3[resolution + 1];

        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Cos(2 * radianAngle)) / gravity;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }
        return arcArray;
    }

    private Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((gravity * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        return new Vector3(x, y);
    }
}
