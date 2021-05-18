using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DropPoint : MonoBehaviour
{
    public FittedPoint SourcePoint;
    public Transform SourceNearLeftPoint;
    public Transform SourceNearRightPoint;

    public Transform NearLeftPoint;
    public Transform NearRightPoint;
    public Transform FarLeftPoint;
    public Transform FarRightPoint;

    public float XRatio;
    public float YRatio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        XRatio = SourcePoint.GetPointNearProjectionDistanceRatio();
    }
}
