
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

    // Use this for initialization
    void Start () {
        ignoreLayer = ~thisCollider;
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {

        //Player in Radius
        if (target != null)
        {
            //check if player is in sight
            Physics.Raycast(this.transform.position, target.transform.position - this.transform.position, out rayHit, 20f, ignoreLayer);
            if (rayHit.collider.gameObject.CompareTag("Player"))
            {
                //this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, Time.deltaTime * 10);
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
    }

    public void Die()
    {
        Destroy(this.GetComponent<NavMeshAgent>());
        rb.useGravity = true;
        anim.SetTrigger("death");
        Destroy(GetComponent<Rigidbody>(), 2f);
        Destroy(GetComponent<BoxCollider>(), 3f);
        Destroy(this.GetComponent<BatScript>());
    }
}
