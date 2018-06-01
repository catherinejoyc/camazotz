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

    //Disable Movement
    float startTimeMD;
    float timeDisabled;

    //Camera
    public GameObject camMover;

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
            UIManager.MyInstance.healthbar.value = health;
        }
    }

    float timeOfLastHeal;

    void Heal()
    {
        if(Healthpacks >= 1 && Health != 100)
        {
            UIManager.MyInstance.ActivationProcess(1f);
            Invoke("_Heal", 1f);
        }
    }
    void _Heal()
    {
        Health += 20;
        Healthpacks--;
        UIManager.MyInstance.UpdateStatus("I feel better now.");
    }

    //Healthpack
    private int healthpacks = 0;
    public int Healthpacks
    {
        get
        {
            return healthpacks;
        }
        set
        {
            healthpacks = value;
            UIManager.MyInstance.txt_healthpacks.text = healthpacks.ToString();
        }
    }

    public void CollectHealthpack(int number)
    {
        Healthpacks += number;
    }

    //Animation
    public Animator anim;

    //Game Over
    bool winning = false;
    void GameOver()
    {
        GameManager.MyInstance.GameOver();
    }

    void Die()
    {
        DisableMovement("death");
        GameManager.MyInstance.OnPlayerDeath();
        //GameManager.MyInstance.OnPlayerRespawn();
        //health = 100f;
    }

    void Update () {
        //Heal
        if (Input.GetButtonDown("Fire3") && timeOfLastHeal + 1 < Time.time)
        {
            timeOfLastHeal = Time.time;
            Heal();
        }

        //Game Over
        if (winning)
            UIManager.MyInstance.UpdateStatus("I have to pull that heart out!");

        if (winning && Input.GetButtonDown("Fire1"))
        {
            DisableMovement("heart");
            UIManager.MyInstance.ActivationProcess(2f);
            Invoke("GameOver", 2f);
        }
    }

    private void FixedUpdate()
    {
        //Laufen
        if (startTimeMD + timeDisabled < Time.time)
        {
            playerMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;
            rb.AddForce(correctedMovement);
        }

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
        correctedMovement = camMover.transform.TransformDirection(playerMovement);
        if (correctedMovement.sqrMagnitude > 0.1f && startTimeMD + timeDisabled < Time.time)
            transform.rotation = Quaternion.Euler(Vector3.Scale(camMover.transform.rotation.eulerAngles, Vector3.up));

        //Quaternion.Euler(Vector3.Scale(camMover.transform.rotation.eulerAngles, Vector3.up)); doesn't rotate around z but also won't rotate around y
        //Quaternion.LookRotation(correctedMovement); rotates around y but also around z

        //Springen
        if (Input.GetButtonDown("Jump") && isGrounded && startTimeMD + timeDisabled < Time.time)
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

    public void DisableMovement(string cause)
    {
        switch (cause)
        {
            case "hfr_pickUp":
                timeDisabled = 4f;
                break;
            case "hfr_attack":
                timeDisabled = 2f;
                break;
            case "heart":
                timeDisabled = 2f;
                break;
            case "death":
                timeDisabled = 2f;
                break;
            default:
                break;
        }
        startTimeMD = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Game Over
        if (other.CompareTag("Heart"))
        {
            winning = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Heart"))
        {
            winning = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(new Vector3(rb.transform.position.x, rb.transform.position.y + groundCheckYOffset, rb.transform.position.z), groundCheckradius);
    }
}
