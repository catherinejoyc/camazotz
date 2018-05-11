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

    //hfr
    public Image hfr_fill;

    //Player
    public Slider healthbar;
    public Text txt_healthpacks;
}
