﻿using System.Collections;
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
        if (statusChanged)
        {
            Invoke("StatusReset", 3f);
        }

        //Controls Text
        if (ctrl_start + 10f < Time.time)
            txt_controls.text = "";
    }



    // ------------------------------------------------------------------------ //

    //activate
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


    //Status
    public Text txt_statusUpdate;
    public bool statusChanged;
    public void UpdateStatus(string message)
    {
        txt_statusUpdate.text = message;
        statusChanged = true;
    }
    private void StatusReset()
    {
        txt_statusUpdate.text = "";
        statusChanged = false;
    }

    //Controls
    public Text txt_controls;
    float ctrl_start;
    public void NewControlsText(string message)
    {
        txt_controls.text = message;
        ctrl_start = Time.time;
    }

    //hfr
    public Image hfr_fill;

    //Player
    public Slider healthbar;
    public Text txt_healthpacks;
}
