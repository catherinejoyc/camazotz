using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frequency : MonoBehaviour {

    float cooldStart = -3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bat") && other.gameObject.GetComponent<BatScript>() != null)
            other.gameObject.GetComponent<BatScript>().Die();
    }

    private void Awake()
    {
        GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        //Frequency Attack
        if (Input.GetButton("Fire2"))
        {
            if (cooldStart + 3f <= Time.time)
            {
                UIManager.MyInstance.txt_hfr.text = "HFR activated!";
                GetComponent<Collider>().enabled = true;
                cooldStart = Time.time;
            }
            else
            {
                UIManager.MyInstance.txt_hfr.text = "HFR not ready!";
            }
        }
        else
            GetComponent<Collider>().enabled = false;
    }
}
