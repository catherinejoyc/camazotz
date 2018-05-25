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

    private void Update()
    {
        // Area7
        AllFencesInRoom.transform.position = Vector3.MoveTowards(AllFencesInRoom.transform.position, sinkPos, 2f * Time.deltaTime);
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

    // --- Special Events ---
    // Area7
    public GameObject AllFencesInRoom;
    Vector3 sinkPos = new Vector3();
    public void ExitArea7()
    {
        //All Lights out for 2 Seconds
        ToggleLight();

        //All fences sink into the ground
        sinkPos = new Vector3(AllFencesInRoom.transform.position.x, AllFencesInRoom.transform.position.y - 3f, AllFencesInRoom.transform.position.z);

        //Turn Lights on again
        Invoke("ToggleLight", 1f);
    }
    void ToggleLight()
    {
        foreach (Light _light in FindObjectsOfType<Light>())
        {
            if (_light.enabled)
                _light.enabled = false;
            else
                _light.enabled = true;
        }
    }


}
