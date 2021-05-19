using UnityEngine;

[ExecuteAlways]
public class FittedPoint : MonoBehaviour
{
    public FittedPlane Plane;

    public Transform PointProjection;
    public Transform PointNearProjection;
    public float PointNearProjectionDistanceRatio;
    public bool IsWithinPlane;
    public bool IsWithinNearBounds;
    public bool IsPositive;
    public bool IsAbove;

    private Vector3 _pointProjection;
    private Vector3 _pointNearProjection;
    private float _pointNearProjectionDistanceRatio;
    private bool _isWithinPlane;

    public Vector3 GetPointProjection()
    {
        return _pointProjection;
    }

    public Vector3 GetPointNearProjection()
    {
        return _pointNearProjection;
    }

    public float GetPointNearProjectionDistanceRatio()
    {
        return _pointNearProjectionDistanceRatio;
    }

    public bool GetIsWithinPlane()
    {
        return _isWithinPlane;
    }

    // Start is called before the first frame update
    void Start()
    {
        FitPointOnPlane();
        ComputePointNearProjection();
        CheckIfWithinPlane();
    }

    // Update is called once per frame
    void Update()
    {
        FitPointOnPlane();
        ComputePointNearProjection();
        CheckIfWithinPlane();

        if (Application.isEditor)
        {
            if (PointProjection) PointProjection.position = _pointProjection;
            if (PointNearProjection) PointNearProjection.position = _pointNearProjection;
            PointNearProjectionDistanceRatio = _pointNearProjectionDistanceRatio;
            IsWithinPlane = _isWithinPlane;
        }    
    }

    void FitPointOnPlane()
    {
        _pointProjection = Plane.FitPoint(transform.position);
    }

    void ComputePointNearProjection()
    {
        Vector3 basePoint = Plane.NearLeftPoint.position;
        Vector3 a = Plane.NearRightPoint.position - basePoint;
        Vector3 b = _pointProjection - basePoint;

        float projectionMagnitude = b.magnitude * Mathf.Cos(Mathf.Deg2Rad * Vector3.Angle(b, a));
        _pointNearProjection = projectionMagnitude * a.normalized + basePoint;

        Vector3 maxDistanceVector = Plane.NearRightPoint.position - Plane.NearLeftPoint.position;
        Vector3 distanceVector = _pointNearProjection - Plane.NearLeftPoint.position;

        float distanceRatio = distanceVector.magnitude / maxDistanceVector.magnitude;
        bool isPositive = 0 <= Vector3.Dot(maxDistanceVector.normalized, distanceVector.normalized);
        bool isAbove = 0 > Vector3.Dot(
            Vector3.Normalize(transform.position - _pointProjection),
            Vector3.Normalize(Plane.GetPlaneNormal() - _pointProjection)
        );
        _pointNearProjectionDistanceRatio = IsPositive ? distanceRatio : -distanceRatio;
        IsWithinNearBounds = 0 <= _pointNearProjectionDistanceRatio && _pointNearProjectionDistanceRatio <= 1;
        IsPositive = isPositive;
        IsAbove = isAbove;
    }

    void CheckIfWithinPlane()
    {
        _isWithinPlane = Plane.IsWithinPlane(_pointProjection);
    }
}
