
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BatScript : MonoBehaviour {

    public Rigidbody rb;
    NavMeshAgent agent;
    public Light aggroLight;

    GameObject target;
    RaycastHit hit;
    public LayerMask thisCollider;
    LayerMask ignoreLayer;

    public Animator anim;

    bool dead = false;

    Vector3 startPos;

    // Use this for initialization
    void Start () {
        ignoreLayer = ~thisCollider;
        agent = GetComponent<NavMeshAgent>();

        startPos = transform.position;

        aggroLight.enabled = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        // Player in Radius
        if (target != null && !dead)
        {
            // Look Direction
            FaceTarget(target.transform.position);

            // Activate Light
            aggroLight.enabled = true;

            // Check if player is in sight
            Physics.Raycast(this.transform.position, target.transform.position - transform.position, out hit, 10f, ignoreLayer);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                // Attack player
                agent.SetDestination(target.transform.position);
            }
        }
        else
        {
            // Deactivate Light
            aggroLight.enabled = false;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        // Player in radius
        if (other.CompareTag("Player"))
            target = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        // Player out of radius
        if (other.CompareTag("Player"))
            target = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // Damage
            collision.gameObject.GetComponent<PlayerScript>().Health--;

            // Knockback
            agent.isStopped = true;
            Invoke("Attack", 1f);
        }

        //if(dead && transform.position.y <= 0.5f)
        //{
        //    GetComponent<BoxCollider>().enabled = false;
        //    rb.useGravity = false;
        //}
    }

    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
    }

    void Attack()
    {
        if (target != null)
            agent.SetDestination(target.transform.position);          
        agent.isStopped = false;
    }

    public void Die()
    {
        print("die");
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
