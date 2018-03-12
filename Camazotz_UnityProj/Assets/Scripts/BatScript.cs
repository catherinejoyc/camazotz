
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : MonoBehaviour {

    bool active;
    Vector3 currPos;

    bool getNewPos;
    bool up;

    public Rigidbody rb;

    GameObject target;
    RaycastHit rayHit;
    public LayerMask thisCollider;
    LayerMask ignoreLayer;

    // Use this for initialization
    void Start () {
        currPos = this.transform.position;
        ignoreLayer = ~thisCollider;
	}
	
	// Update is called once per frame
	void Update () {

        //Player in Radius
        if (target != null)
        {
            //check if player is in sight
            Physics.Raycast(currPos, target.transform.position - currPos, out rayHit, 20f, ignoreLayer);

            if (rayHit.collider.gameObject.CompareTag("Player"))
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, Time.deltaTime * 10);
            }
            else
                Idle();
        }
        else
        {
            //Idle
            Idle();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            target = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
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
        rb.useGravity = true;
        Destroy(this.GetComponent<BatScript>());
    }

    public void Idle()
    {
        //Neue Base-Position
        if (this.gameObject.transform.position.x != currPos.x || this.gameObject.transform.position.z != currPos.z)
            getNewPos = true;

        if (getNewPos)
        {
            currPos = this.gameObject.transform.position;
            getNewPos = false;
        }

        if (!up)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(currPos.x, 2.5f, currPos.z), Time.deltaTime);
            if (this.transform.position.y >= 2.5f)
                up = true;
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(currPos.x, currPos.y, currPos.z), Time.deltaTime);
            if (this.transform.position.y <= currPos.y)
                up = false;
        }
    }
}
