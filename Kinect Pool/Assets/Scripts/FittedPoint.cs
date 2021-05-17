using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class FittedPoint : MonoBehaviour
{
    public FittedPlane Plane;
    public Transform PointProjection;
    public Transform PointProjectionOnNearPoints;

    public Transform A;
    public Transform B;
    public Transform C;

    private Vector3 _pointProjection;
    private Vector3 _pointNearProjection;
    private float _pointNearProjectionDistanceRatio;

    public bool IsWithinPlane()
    {
        // TODO rotate point projection with the plane itself to reduce dimensions from 3D to 2D.
        // TODO check if normalized point projection is contained by the fitted plane polygon.
        // TODO simply project point projection to "near" points to get single-axis mapping proportions.
        // TODO apply mapping proportions to output plane avatar point.

        return false;
    }

    public float GetPointNearProjectionDistanceRatio()
    {
        return _pointNearProjectionDistanceRatio;
    }

    public Vector3 GetPointNearProjection()
    {
        return _pointNearProjection;
    }

    // Start is called before the first frame update
    void Start()
    {
        FitPointOnPlane();
        ComputePointNearProjection();
    }

    // Update is called once per frame
    void Update()
    {
        FitPointOnPlane();
        ComputePointNearProjection();

        if (Application.isEditor)
        {
            if (PointProjection) PointProjection.position = _pointProjection;
            if (PointProjectionOnNearPoints) PointProjectionOnNearPoints.position = _pointNearProjection;
        }        
    }

    void FitPointOnPlane()
    {
        _pointProjection = Plane.FitPoint(transform.position);
    }

    void ComputePointNearProjection()
    {
        Vector3 basePoint = Plane.CloseLeftPoint.position;
        Vector3 a = Plane.CloseRightPoint.position - basePoint;
        Vector3 b = _pointProjection - basePoint;

        float projectionMagnitude = b.magnitude * Mathf.Cos(Mathf.Deg2Rad * Vector3.Angle(b, a));
        _pointNearProjection = projectionMagnitude * a.normalized + basePoint;

        float maxDistance = Vector3.Distance(Plane.CloseLeftPoint.position, Plane.CloseRightPoint.position);
        float distance = Vector3.Distance(_pointNearProjection, Plane.CloseRightPoint.position);
        _pointNearProjectionDistanceRatio = (maxDistance - distance) / maxDistance;
    }
}
