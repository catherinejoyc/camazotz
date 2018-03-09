using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : MonoBehaviour {

    bool active;
    Vector3 currPos;

    public Rigidbody rb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(active)
        {
        }
        else
        {
            currPos = this.transform.position;

        }
	}

    public void Die()
    {
        rb.useGravity = true;
    }
}
