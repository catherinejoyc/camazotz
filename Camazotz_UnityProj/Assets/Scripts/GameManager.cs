using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager myInstance;
    public static GameManager MyInstance
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

    //Checkpoint
    public CheckpointScript lastCheckpoint;
    public GameObject spawnArea;

    //Player
    public PlayerScript player;

    public void OnPlayerRespawn()
    {
        //Player
        player.transform.position = lastCheckpoint.transform.position;

        //Bats
        foreach (BatScript enemy in spawnArea.GetComponentsInChildren<BatScript>())
            enemy.OnPlayerRespawn();
        FindObjectOfType<CamazotzScript>().OnPlayerRespawn();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
