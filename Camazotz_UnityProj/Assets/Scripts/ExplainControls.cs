using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplainControls : MonoBehaviour {

    public string controllerControls;
    public string keyboardControls;

    string controlText;

    private void Start()
    {
        controllerControls = "Use your Left Joystick to Move and Y to Jump. Move your Right Joystick to Look Around.";
        keyboardControls = "Use W, A, S, D to Move and Space to Jump. Use Arrows to Look Around.";

        UIManager.MyInstance.NewControlsText(controlText);
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        UIManager.MyInstance.NewControlsText(controlText);
    }

}
