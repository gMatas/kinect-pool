using UnityEngine;

[ExecuteAlways]
public class FittedPlane : MonoBehaviour
{
    public Transform CloseLeftPoint;
    public Transform CloseRightPoint;
    public Transform FarLeftPoint;
    public Transform FarRightPoint;

    public Transform FarLeftPointProjection;
    public Transform FarRightPointProjection;

    private Vector3 _basePoint;
    private Vector3 _planeNormal;
    private Vector3 _farLeftPointProjection;
    private Vector3 _farRightPointProjection;

    public Vector3 GetPlaneBasePoint()
    {
        return _basePoint;
    }

    public Vector3 GetPlaneNormal()
    {
        return _planeNormal;
    }

    public Vector3 FitPoint(Vector3 point)
    {
        Vector3 fittedPoint = Vector3.ProjectOnPlane(point - _basePoint, _planeNormal) + _basePoint;
        return fittedPoint;
    }

    public bool IsWithinPlane(Vector3 point)
    {
        // TODO rotate point projection with the plane itself to reduce dimensions from 3D to 2D.
        // TODO check if normalized point projection is contained by the fitted plane polygon.
        // TODO simply project point projection to "near" points to get single-axis mapping proportions.
        // TODO apply mapping proportions to output plane avatar point.

        Vector3[] vertices = new Vector3[] {
            CloseLeftPoint.position,
            FarLeftPoint.position,
            FarRightPoint.position,
            CloseRightPoint.position
        };

        Vector3 baseUnitNormal = _planeNormal.normalized;
        Vector3 previous = vertices[vertices.Length - 1];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 current = vertices[i];
            Vector3 unitNormal = Vector3.Cross(point - previous, current - previous).normalized;
            float product = Vector3.Dot(unitNormal, baseUnitNormal);
            if (product < 0) return false;
            previous = current;
        }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        FitPlaneOnPoints();
    }

    // Update is called once per frame
    void Update()
    {
        FitPlaneOnPoints();
        
        if (Application.isEditor)
        {
            if (FarLeftPointProjection) FarLeftPointProjection.position = _farLeftPointProjection;
            if (FarRightPointProjection) FarRightPointProjection.position = _farRightPointProjection; 
        }
    }

    void FitPlaneOnPoints()
    {
        _basePoint = (FarLeftPoint.position + FarRightPoint.position) * 0.5f;
        _planeNormal = Vector3.Cross(
            CloseLeftPoint.position - _basePoint,
            CloseRightPoint.position - _basePoint
        );
        _farLeftPointProjection = FitPoint(FarLeftPoint.position);
        _farRightPointProjection = FitPoint(FarRightPoint.position);
    }
}
