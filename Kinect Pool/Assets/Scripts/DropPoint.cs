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

    public float XRatio;
    public float YRatio = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        XRatio = SourcePoint.GetPointNearProjectionDistanceRatio();
    }

    // Update is called once per frame
    void Update()
    {
        XRatio = SourcePoint.GetPointNearProjectionDistanceRatio();
    }    
}
