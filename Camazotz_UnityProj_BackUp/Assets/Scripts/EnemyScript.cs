using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {

    public NavMeshAgent agent;
    public Vector3 goal1;
    public Vector3 goal2;

    Vector3 goal;

    GameObject chasedObject;

	// Use this for initialization
	void Start () {
        goal = goal1;
	}
	
	// Update is called once per frame
	void Update () {
        if(chasedObject == null)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.3f)
            {
                if (goal == goal1)
                    goal = goal2;
                else
                    goal = goal1;
            }
        }
        else
        {
            goal = chasedObject.transform.position;
        }

        agent.SetDestination(goal);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chasedObject = other.gameObject;
            this.GetComponent<Light>().intensity = 5f;
            agent.speed = 5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chasedObject = null;
            this.GetComponent<Light>().intensity = 1f;
            agent.speed = 2.5f;
        }
    }

    public void Disabled()
    {
        this.GetComponent<Light>().enabled = false;
        this.GetComponent<NavMeshAgent>().enabled = false;
        this.GetComponent<EnemyScript>().enabled = false;
    }
}
