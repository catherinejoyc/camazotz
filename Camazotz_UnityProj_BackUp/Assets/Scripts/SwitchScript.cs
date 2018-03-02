using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchScript : MonoBehaviour
{
    public UnityEvent Interact;
    bool active;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1") && active)
            Interact.Invoke();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            active = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            active = false;
    }
}
