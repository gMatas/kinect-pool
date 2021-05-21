using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DropPoint : MonoBehaviour
{
    public FittedPoint SourcePoint;
    public FittedPlane SourcePlane;
    public float HeightConstant = 0;
    public float HeightMultiplier = 1;
    public float MinMoveDistance;

    public bool IsSourcePointMoving;

    private Vector2 _point;
    private Vector3 _prevSourcePointPosition;

    public Vector2 GetRatios()
    {
        return _point;
    }

    // Start is called before the first frame update
    void Start()
    {
        _point = new Vector2(0, 0);
        UpdatePoint();
        UpdatePosition();

        IsSourcePointMoving = false;
        _prevSourcePointPosition = SourcePoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePoint();
        UpdatePosition();

        // Check if source point is moving
        UpdateSourcePointMotionState();
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
        transform.localPosition = new Vector3(Mathf.Clamp(_point.x, 0, 1), Mathf.Clamp(_point.y, 0, 1), 0);
    }

    void UpdateSourcePointMotionState()
    {
        Vector3 _sourcePointPosition = SourcePoint.transform.position;
        float movedDistance = Vector3.Distance(_prevSourcePointPosition, _sourcePointPosition);
        IsSourcePointMoving = movedDistance >= MinMoveDistance;
        _prevSourcePointPosition = SourcePoint.transform.position;        
    }
}
