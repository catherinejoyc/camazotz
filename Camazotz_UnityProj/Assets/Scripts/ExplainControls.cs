using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplainControls : MonoBehaviour {

    string controllerControls;
    string keyboardControls;

    string controlText;

    private void Start()
    {
        if (Input.GetJoystickNames().Length > 0)
            controlText = controllerControls;
        else
            controlText = keyboardControls;
    }

    private void Update()
    {
        if (Input.GetJoystickNames().Length > 0)
            controlText = controllerControls;
        else
            controlText = keyboardControls;
    }

    private void OnTriggerStay(Collider other)
    {
        switch (this.gameObject.name)
        {
            case "RunAndJump":
                controllerControls = "Use your Left Joystick to Run. \n Press Y to Jump.";
                keyboardControls = "Use W,A,S,D to Run. \n Press Space to Jump.";
                break;
            case "Interact":
                controllerControls = "Press A to Interact with Objects.";
                keyboardControls = "Press CTRL to Interact with Objects.";
                break;
            case "Collect":
                controllerControls = "Press A to Collect Objects.";
                keyboardControls = "Press CTRL to Collect Objects.";
                break;
            case "HFR":
                controllerControls = "Press B to Attack.";
                keyboardControls = "Press Alt to Attack.";
                break;
        }

    }
}
