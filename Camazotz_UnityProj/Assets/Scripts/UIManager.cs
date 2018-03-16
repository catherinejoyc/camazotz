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

    public Text txt_selectedObj;

    //interactable
    public Text txt_statusUpdate;

    //hfr
    public Text txt_hfr;

    //Player
    public Text txt_health;
    public Text txt_healthpacks;
}
