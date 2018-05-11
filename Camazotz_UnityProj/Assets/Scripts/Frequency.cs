using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frequency : MonoBehaviour {

    float cooldStart = -3;

    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bat") && other.gameObject.GetComponent<BatScript>() != null)
            other.gameObject.GetComponent<BatScript>().Die();
    }

    private void Awake()
    {
        GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        //Frequency Attack
        if (Input.GetButtonDown("Fire2") && cooldStart + 3f <= Time.time)
        {
            player.GetComponentInChildren<Animator>().SetTrigger("hfr_attack");
            player.GetComponent<PlayerScript>().DisableMovement("hfr_attack");
            UIManager.MyInstance.ActivationProcess(2f);
            Invoke("Attack", 2f);
        }
        else
            GetComponent<Collider>().enabled = false;

        //Cooldown
        UIManager.MyInstance.hfr_fill.fillAmount += Time.deltaTime / 3f;
    }

    void Attack()
    {
        GetComponent<Collider>().enabled = true;
        cooldStart = Time.time;
        UIManager.MyInstance.hfr_fill.fillAmount = 0;
    }
}
