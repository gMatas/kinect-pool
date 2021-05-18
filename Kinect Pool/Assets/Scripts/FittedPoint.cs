using UnityEngine;

[ExecuteAlways]
public class FittedPoint : MonoBehaviour
{
    public FittedPlane Plane;

    public Transform PointProjection;
    public Transform PointNearProjection;
    public float PointNearProjectionDistanceRatio;
    public bool IsWithinPlane;

    private Vector3 _pointProjection;
    private Vector3 _pointNearProjection;
    private float _pointNearProjectionDistanceRatio;
    private bool _isWithinPlane;

    public bool GetIsWithinPlane()
    {
        return _isWithinPlane;
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

        float maxDistance = Vector3.Distance(Plane.NearLeftPoint.position, Plane.NearRightPoint.position);
        float distance = Vector3.Distance(_pointNearProjection, Plane.NearRightPoint.position);
        _pointNearProjectionDistanceRatio = (maxDistance - distance) / maxDistance;
    }

    void CheckIfWithinPlane()
    {
        _isWithinPlane = Plane.IsWithinPlane(_pointProjection);
    }
}
