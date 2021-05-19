using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ConfigurationUI : MonoBehaviour
{
    public Transform Avatar;
    public FittedPlane SourcePlane;
    public DropPoint DropPoint;

    public GameObject UiCanvas;
    public InputField HeightMultiplier;
    public InputField HeightConstant;

    public Button NL;
    public Button NR;
    public Button FL;
    public Button FR;
    public Button Exit;
    public Button Quit;

    private float _heightConstant;
    private float _heightMultiplier;
    private Dictionary<object, string> _buttonNames;

    void Start()
    {
        _heightConstant = DropPoint.HeightConstant;
        _heightMultiplier = DropPoint.HeightMultiplier;

        HeightConstant.text = _heightConstant.ToString();
        HeightMultiplier.text = _heightMultiplier.ToString();

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

        HeightConstant.onValueChanged.AddListener(UpdateHeightConstant);
        HeightMultiplier.onValueChanged.AddListener(UpdateHeightMultiplier);
    }

    void UpdateHeightMultiplier(string value)
    {
        _heightMultiplier = ParseFormToFloat(HeightMultiplier.text, _heightMultiplier);
    }

    void UpdateHeightConstant(string value)
    {
        _heightConstant = ParseFormToFloat(HeightConstant.text, _heightConstant);
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

    float ParseFormToFloat(string text, float defaultValue)
    {
        bool isSuccess = float.TryParse(text, out float outputValue);
        if (isSuccess) return outputValue;
        return defaultValue;
    }

    public float GetHeightMultiplier()
    {
        return _heightMultiplier;
    }

    public float GetHeightConstant()
    {
        return _heightConstant;
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
