using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CamazotzScript : MonoBehaviour {

    public Rigidbody rb;
    NavMeshAgent agent;
    RaycastHit hit;

    GameObject target;
    public LayerMask thisCollider;
    LayerMask ignoreLayer;

    public Animator anim;

    bool dead = false;

    // Stats
    public Vector3 startPos;

    // Use this for initialization
    void Start()
    {
        ignoreLayer = ~thisCollider;
        agent = GetComponent<NavMeshAgent>();

        startPos = transform.position;
    }

    // Update
    void Update()
    {
        // Player in Radius
        if (!dead && target != null)
        {
            // Look Direction
            FaceTarget(target.transform.position);

            // Attack player
            agent.SetDestination(target.transform.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Player in radius
        if (other.CompareTag("Player") && !dead)
        {
            // Check if player is in sight
            Physics.Raycast(this.transform.position, other.transform.position - transform.position, out hit, Mathf.Infinity, ignoreLayer);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                target = other.gameObject;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !dead)
        {
            // Damage
            collision.gameObject.GetComponent<PlayerScript>().Health -= 5;

            // Knockback
            if (agent != null && !dead)
            {
                agent.isStopped = true;
                Invoke("Attack", 1f);
            }
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
            agent.SetDestination(target.transform.position);
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
