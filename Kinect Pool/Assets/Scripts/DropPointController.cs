using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPointController : MonoBehaviour
{
    public ConfigurationUI ConfigurationUI;

    private DropPoint _dropPoint;

    // Start is called before the first frame update
    void Start()
    {
        _dropPoint = GetComponent<DropPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        _dropPoint.HeightConstant = ConfigurationUI.GetHeightConstant();
        _dropPoint.HeightMultiplier = ConfigurationUI.GetHeightMultiplier();
    }
}
