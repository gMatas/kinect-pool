using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    public Transform Avatar;
    public FittedPlane SourcePlane;

    public GameObject UiCanvas;

    public Button NL;
    public Button NR;
    public Button FL;
    public Button FR;
    public Button Exit;
    public Button Quit;

    private Dictionary<object, string> _buttonNames;

    void Start()
    {
        _buttonNames = new Dictionary<object, string>
        {
            { FL, "FL" },
            { FR, "FR" },
            { NL, "NL" },
            { NR, "NR" }
        };

        Exit.onClick.AddListener(() => ExitUI());
        Quit.onClick.AddListener(() => QuitSimulation());

        AssignButtonToPoint(FL, SourcePlane.FarLeftPoint);
        AssignButtonToPoint(FR, SourcePlane.FarRightPoint);
        AssignButtonToPoint(NL, SourcePlane.NearLeftPoint);
        AssignButtonToPoint(NR, SourcePlane.NearRightPoint);
    }

    void AssignButtonToPoint(Button button, Transform point)
    {
        button.onClick.AddListener(() => AddCoords(button, point));
    }

    void AddCoords(Button button, Transform point)
    {
        var position = Avatar.transform.position;
        point.position = position;
        button.GetComponentInChildren<Text>().text = $"{_buttonNames[button]} {position}";
    }

    void ExitUI()
    {
        UiCanvas.SetActive(false);
    }

    void QuitSimulation()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            UiCanvas.SetActive(true);
        }
    }
}
