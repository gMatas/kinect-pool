using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    public GameObject uiCanvas;
    public GameObject Avatar;

    public GameObject FRP;
    public GameObject FLP;
    public GameObject CRP;
    public GameObject CLP;

    public Button exit;
    public Button quit;
    public Button FL;
    public Button FR;
    public Button NL;
    public Button NR;


    void Start()
    {
		exit.onClick.AddListener(() => exitUI());
        quit.onClick.AddListener(() => quitSimulation());

		FL.onClick.AddListener(() => AddCoords("FL"));
		FR.onClick.AddListener(() => AddCoords("FR"));
		NL.onClick.AddListener(() => AddCoords("NL"));
		NR.onClick.AddListener(() => AddCoords("NR"));
    }

    void AddCoords(string name){
        var avatarPosition = Avatar.transform.position;

        switch (name)
        {
        case "FL":
            FLP.transform.position = avatarPosition;
            FL.GetComponentInChildren<Text>().text = $"FL {avatarPosition}";
            break;
        case "FR":
            FRP.transform.position = avatarPosition;
            FR.GetComponentInChildren<Text>().text = $"FR {avatarPosition}";
            break;
        case "NL":
            CLP.transform.position = avatarPosition;
            NL.GetComponentInChildren<Text>().text = $"NL {avatarPosition}";
            break;
        case "NR":
            CRP.transform.position = avatarPosition;
            NR.GetComponentInChildren<Text>().text = $"NR {avatarPosition}";
            break;
        default:
            break;
        }
    }

    void exitUI() {
        uiCanvas.SetActive(false);
	}

    void quitSimulation() {
        Application.Quit();
	}

    void Update() {
     if (Input.GetKeyDown("escape")){
          uiCanvas.SetActive(true);
      }
    }
}
