
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

    // Stats
    Vector3 startPos;

    // Use this for initialization
    void Start () {
        ignoreLayer = ~thisCollider;
        agent = GetComponent<NavMeshAgent>();

        startPos = transform.position;

        aggroLight.enabled = false;
    }
	
    // FixedUpdate
	void FixedUpdate () {

        // Player in Radius
        if (target != null && !dead)
        {
            // Check if player is in sight
            Physics.Raycast(this.transform.position, target.transform.position - transform.position, out hit, 20f, ignoreLayer);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                // Look Direction
                FaceTarget(target.transform.position);

                // Activate Light
                aggroLight.enabled = true;

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
        if (other.CompareTag("Player") && !dead)
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
        if(collision.gameObject.CompareTag("Player") && !dead)
        {
            // Damage
            collision.gameObject.GetComponent<PlayerScript>().Health--;

            // Knockback
            if (agent != null && agent.isActiveAndEnabled && !dead)
            {               
                agent.isStopped = true;
                Invoke("Attack", 1f);
            }
        }
        else if(dead)
        {
            Disappear();
        }
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
        if (!dead)
        {
            if (target != null)
                agent.SetDestination(target.transform.position);
            if (agent != null)
                agent.isStopped = false;
        }
    }

    public void Die()
    {
        agent.enabled = false;

        rb.useGravity = true;
        anim.SetTrigger("death");
        dead = true;
        target = null;
    }

    void Disappear()
    {
        gameObject.SetActive(false);
    }

    public void OnPlayerRespawn() // Make sure to activate the gameObject first
    {
        // Respawn
        transform.position = startPos;

        agent.enabled = true;
        agent.ResetPath();
        agent.updatePosition = true;
        target = null;

        rb.useGravity = false;
        anim.SetTrigger("alive");
        dead = false;
    }
}
