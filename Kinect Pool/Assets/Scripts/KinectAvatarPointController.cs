using Neurorehab.Device_Kinect.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinectAvatarPointController : MonoBehaviour
{
    public Kinect KinectDevice;

    private FittedPoint _point;

    // Start is called before the first frame update
    void Start()
    {
        _point = GetComponent<FittedPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        KinectUnity avatar;
        if (avatar = KinectDevice.PrefabParent.GetComponentInChildren<KinectUnity>())
        {
            var renderer = avatar.GetComponent<Renderer>();
            GameObject[] bodyParts = new GameObject[] {
                avatar.Head,
                avatar.Body,
                avatar.Torso,
                avatar.LeftAnkle,
                avatar.RightAnkle,
                avatar.LeftShoulder,
                avatar.RightShoulder,
                avatar.LeftElbow,
                avatar.RightElbow,
                avatar.LeftWrist,
                avatar.RightWrist
            };

            Vector3[] bodyPoints = new Vector3[bodyParts.Length];
            for (int i = 0; i < bodyParts.Length; i++)
                bodyPoints[i] = bodyParts[i].transform.position;

            _point.transform.position = AveragePoint(bodyPoints);
        }
    }

    private static Vector3 AveragePoint(Vector3[] points)
    {
        Vector3 averagePoint = new Vector3(0, 0);
        for (int i = 0; i < points.Length; i++)
            averagePoint += points[i];

        averagePoint /= points.Length;
        return averagePoint;
    }
}
