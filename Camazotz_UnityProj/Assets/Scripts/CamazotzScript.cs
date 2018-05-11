using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CamazotzScript : MonoBehaviour {

    public Rigidbody rb;
    NavMeshAgent agent;

    GameObject target;
    RaycastHit rayHit;
    public LayerMask thisCollider;
    LayerMask ignoreLayer;

    public Animator anim;

    //Start Stats
    Vector3 startPos;

    // Use this for initialization
    void Start()
    {
        ignoreLayer = ~thisCollider;
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //Player in Radius
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Player in Radius
        if (other.CompareTag("Player"))
            target = other.gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerScript>().Health-=5f;
        }
    }

    public void OnPlayerRespawn()
    {
        target = null;
        transform.position = startPos;
        agent.destination = startPos;
    }
}
