
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BatScript : MonoBehaviour {

    public Rigidbody rb;
    NavMeshAgent agent;

    GameObject target;
    RaycastHit rayHit;
    public LayerMask thisCollider;
    LayerMask ignoreLayer;

    public Animator anim;

    bool dead = false;

    //Start Stats
    Vector3 startPos;

    // Use this for initialization
    void Start () {
        ignoreLayer = ~thisCollider;
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        //Player in Radius
        if (target != null && !dead)
        {
            //check if player is in sight
            Physics.Raycast(this.transform.position, target.transform.position - this.transform.position, out rayHit, 20f, ignoreLayer);
            if (rayHit.collider.gameObject.CompareTag("Player"))
            {
                agent.SetDestination(target.transform.position);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        //Player in Radius
        if (other.CompareTag("Player"))
            target = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        //Player out of Radius
        if (other.CompareTag("Player"))
            target = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerScript>().Health--;
        }

        if(dead && transform.position.y <= 0.5f)
        {
            GetComponent<BoxCollider>().enabled = false;
            rb.useGravity = false;
        }
    }

    public void Die()
    {
        agent.baseOffset = 0f;
        agent.destination = transform.position;
        target = null;
        rb.useGravity = true;
        anim.SetTrigger("death");
        dead = true;
        target = null;
    }

    public void OnPlayerRespawn()
    {
        target = null;

        
        agent.baseOffset = 3.2f;
        agent.destination = startPos;

        rb.useGravity = false;
        anim.SetTrigger("alive");
        dead = false;

        GetComponent<BoxCollider>().enabled = true;

        transform.position = startPos;
    }
}
