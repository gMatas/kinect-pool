using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DropPoint : MonoBehaviour
{
    public FittedPoint SourcePoint;
    public FittedPlane SourcePlane;

    public Transform NearLeftPoint;
    public Transform NearRightPoint;
    public Transform FarLeftPoint;
    public Transform FarRightPoint;

    public float HeightConstant = 0;
    public float HeightMultiplier = 1;

    public bool IsAbove;

    private Vector2 _point;

    public Vector2 GetPoint()
    {
        return _point;
    }

    // Start is called before the first frame update
    void Start()
    {
        _point = new Vector2(0, 0);
        UpdatePoint();
        UpdatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePoint();
        UpdatePosition();
    }

    void UpdatePoint()
    {
        float distance = Vector3.Distance(
            SourcePoint.transform.position,
            SourcePoint.GetPointProjection()
        );

        _point.x = SourcePoint.GetPointNearProjectionDistanceRatio();
        _point.y = HeightConstant + HeightMultiplier * (SourcePoint.IsAbove ? distance : -distance);
    }

    void UpdatePosition()
    {
        transform.localPosition = new Vector2(Mathf.Clamp(_point.x, 0, 1), Mathf.Clamp(_point.y, 0, 1));
    }
}
