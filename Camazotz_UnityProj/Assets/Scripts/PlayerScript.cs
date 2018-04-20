using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    //Movement
    public Rigidbody rb;
    public float speed = 17.5f;
    public float jumpSpeed;
    public float gravity;

    private Vector3 playerMovement;
    private Vector3 correctedMovement;

    public float groundCheckYOffset;
    public float groundCheckradius;
    public LayerMask ground;
    bool isGrounded;

    //Kamera
    public GameObject camMover;
    public float cameraSpeed;

    //Health
    private float health = 100f;
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health > 100)
                health = 100;
            if (health <= 0)
                Die();
            UIManager.MyInstance.txt_health.text = "Health: " + health.ToString();
        }
    }

    //Healthpack
    private int healthpacks;
    public int Healthpacks
    {
        get
        {
            return healthpacks;
        }
        set
        {
            healthpacks = value;
            UIManager.MyInstance.txt_healthpacks.text = "Healthpacks: " + healthpacks.ToString();
        }
    }

	// Update is called once per frame
	void Update () {
        //Ausrichtung
        correctedMovement = camMover.transform.TransformDirection(playerMovement);
        //Kamera
        camMover.transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal_right"), 0) * cameraSpeed * Time.deltaTime);
        camMover.transform.position = this.transform.position;

        //Heal
        if (Input.GetButtonDown("Fire3"))
            Heal();
    }

    private void LateUpdate()
    {
        //Kamera - Wand Collision
        RaycastHit wallHit = new RaycastHit();

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
            speed = 8f;
        }
        //Gravity
        rb.AddForce(new Vector3(0, -gravity, 0));
    }

    public void CollectHealthpack(int number)
    {
        Healthpacks += number;
    }

    void Heal()
    {
        if(Healthpacks >= 1 && Health != 100)
        {
            Health += 5;
            Healthpacks--;
            UIManager.MyInstance.txt_statusUpdate.text = "Healed!" ;
        }
    }

    void Die()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(new Vector3(rb.transform.position.x, rb.transform.position.y + groundCheckYOffset, rb.transform.position.z), groundCheckradius);
    }
}
