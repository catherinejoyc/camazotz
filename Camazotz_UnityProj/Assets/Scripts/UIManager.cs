using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private static UIManager myInstance;
    public static UIManager MyInstance
    {
        get
        {
            return myInstance;
        }
    }

    private void Awake()
    {
        if (MyInstance == null)
            myInstance = this;
        else
            Debug.LogError("Singleton already exists!");
    }

    //Update
    private void Update()
    {
        //Activation Process
        if (activated)
        {
            activationBar.value += Time.deltaTime / processTime;
            Invoke("Deactivate", processTime);
        }

        //Status Log Mission Log
        if (status_startTime + 3f < Time.time)
        {
            txt_statusUpdate.text = "";
        }

        //Controls Text
        //if (ctrl_start + 5f < Time.time)
        //    txt_controls.text = "";

        if (Input.GetButtonDown("Submit"))
            ToggleControls();
    }



    // ------------------------------------------------------------------------ //

    //Activation Process
    public Slider activationBar;

    bool activated = false;
    float processTime;
    public void ActivationProcess(float _processTime)
    {
        processTime = _processTime;
        activationBar.value = 0f;
        activationBar.gameObject.SetActive(true);
        activated = true;
    }

    void Deactivate()
    {
        activationBar.value = 0f;
        activationBar.gameObject.SetActive(false);
        activated = false;
    }

    //Status Log Mission Log
    public Text txt_statusUpdate;
    float status_startTime;
    public void UpdateStatus(string message)
    {
        txt_statusUpdate.text = message;
        status_startTime = Time.time;
    }

    //Controls Text
    public Text showControls;
    public Text controls;
    void ToggleControls()
    {
        if (controls.enabled)
        {
            showControls.text = "Enter/Start - Show Controls";
            controls.enabled = false;
        }
        else
        {
            showControls.text = "Enter/Start - Hide Controls";
            controls.enabled = true;
        }
    }

    //HFR
    public Image hfr_fill;

    //Player
    public Slider healthbar;
    public Text txt_healthpacks;

    //BlackScreen
    public Animator blackScreen;
}
