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
        uiCanvas = GameObject.Find("Configuration UI Canvas");
        Avatar = GameObject.Find("Avatar Stub");

        FRP = GameObject.Find("Far Right");
        FLP = GameObject.Find("Far Left");
        CRP = GameObject.Find("Close Right");
        CLP = GameObject.Find("Close Left");


        exit.GetComponent<Button>();
        quit.GetComponent<Button>();

        NR.GetComponent<Button>();
        NL.GetComponent<Button>();
        FR.GetComponent<Button>();
        FL.GetComponent<Button>();

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
            break;
        case "FR":
            FRP.transform.position = avatarPosition;
            break;
        case "NL":
            CLP.transform.position = avatarPosition;
            break;
        case "NR":
            CRP.transform.position = avatarPosition;
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
