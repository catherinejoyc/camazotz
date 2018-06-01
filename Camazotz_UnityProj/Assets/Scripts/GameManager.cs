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

    // Checkpoint
    public CheckpointScript lastCheckpoint;
    public GameObject spawnArea;

    // Bats in area
    public List<BatScript> batsInArea = new List<BatScript>();

    // Camazotz
    public CamazotzScript camazotz;

    // Player
    public PlayerScript player;
    public int player_healthpacks = 0;
    public List<InteractableScript> healthpacksInArea;

    public void OnPlayerDeath()
    {
        // Fade out
        UIManager.MyInstance.blackScreen.SetTrigger("toggleBlackScreen");

        // Kill all bats in area
        foreach (BatScript enemy in batsInArea)
        {
            enemy.Die();
        }

        // Kill Camazotz
        camazotz.Die();

        // Invoke OnPlayerRespawn
        Invoke("OnPlayerRespawn", 2f);
    }

    void OnPlayerRespawn()
    {
        // Healthpacks
        player.Healthpacks = player_healthpacks;
        foreach (InteractableScript h_pack in healthpacksInArea)
        {
            h_pack.gameObject.SetActive(true);
        }

        // Player
        player.transform.position = lastCheckpoint.transform.position;
        player.Health = 100;

        // Bats
        foreach (BatScript enemy in batsInArea)
        {
            enemy.gameObject.SetActive(true);
            enemy.OnPlayerRespawn();
        }

        // Camazotz
        camazotz.gameObject.SetActive(true);
        camazotz.OnPlayerRespawn();

        // Fade in
        UIManager.MyInstance.blackScreen.SetTrigger("toggleBlackScreen");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    // --- Special Events ---

    // Area7
    public GameObject AllFencesInRoom;
    Vector3 sinkPos = new Vector3();
    public void ExitArea7(InteractableScript _switch)
    {
        //All Lights out for 2 Seconds
        ToggleLight();

        //All fences sink into the ground
        sinkPos = new Vector3(AllFencesInRoom.transform.position.x, AllFencesInRoom.transform.position.y - 3f, AllFencesInRoom.transform.position.z);

        //Turn Lights on again
        Invoke("ToggleLight", 1f);

        //turn off switch
        _switch.enabled = false;
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

    // Area8
    public void OpenFinalRoom(DoorScript finalDoor1)
    {
        // Open other door (if closed)
        if (finalDoor1.open == false)
            finalDoor1.ToggleDoor();
    }

    // Area9
    public Vector3 camazotz_spawpoint;
    public void FinalCheckpoint()
    {
        camazotz.startPos = camazotz.transform.position;
    }
}
