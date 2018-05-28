using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class MainMenuScript : MonoBehaviour {

    //Main Menu
    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
    

    private void Start()
    {
        foreach (NavMeshAgent bat in FindObjectsOfType<NavMeshAgent>())
        {
            GotoNextPoint(bat);
        }
    }

    void Update()
    {
        foreach (NavMeshAgent bat in FindObjectsOfType<NavMeshAgent>())
        {
            if (bat.remainingDistance != Mathf.Infinity && bat.pathStatus == NavMeshPathStatus.PathComplete && bat.remainingDistance == 0)
            {
                print("arrived");
                GotoNextPoint(bat);
            }
        }
    }

    public Transform[] points;
    private int destPoint = 0;


    void GotoNextPoint(NavMeshAgent _bat)
    {
        // Set the agent to go to the currently selected destination.
        _bat.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }
}
