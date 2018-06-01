using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

    public Animator blackScreen;
    public CamazotzScript camazotz;
    public Animator red;

    private void Start()
    {
        Invoke("Flash", 2f);
    }

    void Flash()
    {
        red.SetTrigger("red");
        Invoke("Die", 1f);
    }

    void Die()
    {
        camazotz.Die();
        blackScreen.SetTrigger("toggleBlackScreen");
        Invoke("MainMenu", 1f);
    }

    void MainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
