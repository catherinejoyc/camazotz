
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

        //Angriff
        if (target != null)
        {
            //check if player is in sight
            Physics.Raycast(currPos, target.transform.position - currPos, out rayHit, 20f, ignoreLayer);
            if (rayHit.collider.gameObject.CompareTag("Player"))
            {
                Debug.DrawLine(currPos, target.transform.position);
                this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, Time.deltaTime * 2);
            }
            else
                print(rayHit.collider.gameObject.name);
        }
        else
        {
            //Neue Base-Position
            if (this.transform.position.x != currPos.x || this.transform.position.z != currPos.z)
                getNewPos = true;
            if (getNewPos)
            {
                currPos = this.transform.position;
                getNewPos = false;
            }
            if (currPos.y > 1.5f)
                currPos.y = 1.5f;

            //Idle
            if (!up)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(currPos.x, currPos.y + 1, currPos.z), Time.deltaTime);
                if(this.transform.position.y >= currPos.y + 1)
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

    public void Die()
    {
        rb.useGravity = true;
        this.GetComponent<BatScript>().enabled = false;
    }
}
