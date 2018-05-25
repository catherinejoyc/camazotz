using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour {

    public GameObject area;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Checkpoint
            GameManager.MyInstance.lastCheckpoint = this;

            UIManager.MyInstance.UpdateStatus("I sense danger.");

            GameManager.MyInstance.spawnArea = area;

            gameObject.SetActive(false);
        }
    }
}
