using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public Rigidbody rb;
    public float speed = 17.5f;
    public float jumpSpeed;
    public float gravity;

    private Vector3 playerMovement;
    private Vector3 correctedMovement;

    public GameObject camMover;
    public float cameraSpeed;

    public float groundCheckYOffset;
    public float groundCheckradius;
    public LayerMask ground;
    bool isGrounded;

    public GameObject attackHitBox;

	// Update is called once per frame
	void Update () {
        //Ausrichtung
        correctedMovement = camMover.transform.TransformDirection(playerMovement);
        //Kamera
        camMover.transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal_right"), 0) * cameraSpeed * Time.deltaTime);
        camMover.transform.position = this.transform.position;
    }

    private void FixedUpdate()
    {
        //Laufen
        playerMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;
        rb.AddForce(correctedMovement);
        //Ausrichtung
        if(correctedMovement.sqrMagnitude > 0.1f)
            rb.rotation = Quaternion.LookRotation(correctedMovement);
        //Springen
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x/5, 0, rb.velocity.z/5);
            rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
        }
        //Groundcheck
        if (Physics.CheckSphere(new Vector3(rb.transform.position.x, rb.transform.position.y + groundCheckYOffset, rb.transform.position.z), groundCheckradius, ground))
        {
            isGrounded = true;
            speed = 17.5f;
        }
        else
        {
            isGrounded = false;
            speed = 6f;
        }
        //Gravity
        rb.AddForce(new Vector3(0, -gravity, 0));
        //Interagieren + Aufsammeln
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(new Vector3(rb.transform.position.x, rb.transform.position.y + groundCheckYOffset, rb.transform.position.z), groundCheckradius);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Interactables
        //if(other.GetComponent<IInteractable>)
        if (other.CompareTag("interactable"))
        {

        }
    }
}
