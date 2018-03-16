using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableScript : MonoBehaviour
{
    //Key Behaviour
    public void UnlockObject()
    {
        GameObject lockedObj = this.gameObject;
        lockedObj.GetComponent<InteractableScript>().unlocked = true;
    }

    //Chests and unlocked Doorswitches Behaviour
    public bool unlocked;

    //basic interactions
    public UnityEvent Interact;
    bool active;

    void Update () {
        if (Input.GetButtonDown("Fire1") && active)
        {
            if (unlocked)
                Interact.Invoke();
            else
                UIManager.MyInstance.txt_statusUpdate.text = "Key needed!" ;
        }
	}

    public void UpdateStatus(string message)
    {
        UIManager.MyInstance.txt_statusUpdate.text = message;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            active = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            active = false;
    }
}
