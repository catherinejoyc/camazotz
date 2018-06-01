using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frequency : MonoBehaviour {

    float cooldStart = -3;

    public GameObject player;

    ParticleSystem effect;

    private void OnTriggerStay(Collider other)
    {
        // Kill Bats
        if (other.CompareTag("Bat_Body") && other.gameObject.GetComponentInParent<BatScript>() != null)
        {
            other.gameObject.GetComponentInParent<BatScript>().Die();
        }

        // Camazotz
        if (other.CompareTag("Camazotz_Body"))
            UIManager.MyInstance.UpdateStatus("It's not working! Run!");
    }

    private void Awake()
    {
        GetComponent<Collider>().enabled = false;
        effect = GetComponentInChildren<ParticleSystem>();
        effect.Stop();
    }

    private void Update()
    {
        // Frequency Attack
        if (Input.GetButtonDown("Fire2") && cooldStart + 3f <= Time.time)
        {
            effect.Play();
            player.GetComponentInChildren<Animator>().SetTrigger("hfr_attack");
            player.GetComponent<PlayerScript>().DisableMovement("hfr_attack");
            UIManager.MyInstance.ActivationProcess(1f);
            Invoke("Attack", 1f);
        }
        else
            GetComponent<Collider>().enabled = false;

        // Cooldown
        UIManager.MyInstance.hfr_fill.fillAmount += Time.deltaTime / 3f;
    }

    void Attack()
    {
        GetComponent<Collider>().enabled = true;
        effect.Stop();
        cooldStart = Time.time;
        UIManager.MyInstance.hfr_fill.fillAmount = 0;
    }
}
