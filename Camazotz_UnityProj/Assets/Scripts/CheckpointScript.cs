using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour {

    public GameObject area;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Checkpoint
            GameManager.MyInstance.lastCheckpoint = this;

            UIManager.MyInstance.UpdateStatus("I sense danger.");

            // Bats
            GameManager.MyInstance.spawnArea = area;
            GameManager.MyInstance.batsInArea.Clear();
            foreach (BatScript enemy in area.GetComponentsInChildren<BatScript>())
            {
                GameManager.MyInstance.batsInArea.Add(enemy);
            }

            // Healthpacks
            GameManager.MyInstance.player_healthpacks = other.GetComponent<PlayerScript>().Healthpacks;
            GameManager.MyInstance.healthpacksInArea.Clear();
            foreach (InteractableScript h_pack in area.GetComponentsInChildren<InteractableScript>())
            {
                if (h_pack.CompareTag("Healthpack"))
                {
                    GameManager.MyInstance.healthpacksInArea.Add(h_pack);
                }
            }

            // FINAL ROOM
            if (name == "Checkpoint@Area09")
                GameManager.MyInstance.FinalCheckpoint();

            gameObject.SetActive(false);
        }
    }
}
