using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class FittedPoint : MonoBehaviour
{
    public FittedPlane Plane;
    public Transform PointProjection;

    private Vector3 _pointProjection;

    public bool IsWithinPlane()
    {
        // TODO rotate point projection with the plane itself to reduce dimensions from 3D to 2D.
        // TODO check if normalized point projection is contained by the fitted plane polygon.
        // TODO simply project point projection to "near" points to get single-axis mapping proportions.
        // TODO apply mapping proportions to output plane avatar point.

        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        FitPointOnPlane();
    }

    // Update is called once per frame
    void Update()
    {
        FitPointOnPlane();

        if (Application.isEditor)
        {
            if (PointProjection) PointProjection.position = _pointProjection;
        }
    }

    void FitPointOnPlane()
    {
        _pointProjection = Plane.FitPoint(transform.position);
    }
}
