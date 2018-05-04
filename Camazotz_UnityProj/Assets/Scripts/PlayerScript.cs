using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    //Rigidbody
    public Rigidbody rb;

    /// <summary>
    /// --- Rigidbody Einstellungen ---
    /// Speed = 17.5f
    /// Jump Speed = 9
    /// Gravity = 10
    /// Ground Check Y Offset = -0.65
    /// Ground Check Radius = 0.43
    /// Ground = Ground
    /// </summary>

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
    public GameObject camMoverY;
    public GameObject camMoverX;
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

    //Animation
    public Animator anim;

	// Update is called once per frame
	void Update () {
        //Camera
        camMoverY.transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal_right")) * cameraSpeed * Time.deltaTime);

        camMoverX.transform.Rotate(new Vector3(Input.GetAxis("Vertical_right"), 0) * cameraSpeed * Time.deltaTime);

        camMoverY.transform.position = transform.position;

        //Heal
        if (Input.GetButtonDown("Fire3"))
            Heal();
    }

    private void LateUpdate()
    {
        //Camera - Wand Collision
        RaycastHit wallHit = new RaycastHit();
    }

    private void FixedUpdate()
    {
        //Laufen
        playerMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;
        rb.AddForce(correctedMovement);

        //Animation
        if (rb.velocity.magnitude > 0.01f)
        {
            anim.SetBool("isWalking", true);
            if (rb.velocity.magnitude > 1f)
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", true);
            }
            else
                anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
        }

        //Ausrichtung
        correctedMovement = camMoverY.transform.TransformDirection(playerMovement);
        if (correctedMovement.sqrMagnitude > 0.1f)
            rb.rotation = Quaternion.LookRotation(correctedMovement);

        //Springen
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x/5, 0, rb.velocity.z/5);
            rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
            anim.SetTrigger("jump");
        }

        //Groundcheck
        if (Physics.CheckSphere(new Vector3(rb.transform.position.x, rb.transform.position.y + groundCheckYOffset, rb.transform.position.z), groundCheckradius, ground))
        {
            isGrounded = true;
            speed = 17.5f;
            anim.SetBool("isJumping", false);
        }
        else
        {
            isGrounded = false;
            speed = 8f;
            anim.SetBool("isJumping", true);
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
