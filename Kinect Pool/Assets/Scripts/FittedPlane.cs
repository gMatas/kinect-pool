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
