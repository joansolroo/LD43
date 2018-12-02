using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour {

    public float gameDuration;
    public bool pause;

    public GameObject player;
    public GameObject gameUI;
    public GameObject stopUI;

    public Text scoreDisplay;
    public Button retry, quit;

    // Use this for initialization
    void Start() {
        pause = false;
        gameUI.SetActive(true);
        stopUI.SetActive(false);

        quit.onClick.AddListener(QuitGame);
        retry.onClick.AddListener(RetryLevel);
    }

    void QuitGame()
    {
        Application.Quit();
    }
    void RetryLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void SetPause(bool b)
    {
        if(b)
        {
            pause = true;
            player.GetComponent<controller>().Stop();
            player.GetComponent<controller>().enabled = false;
        }
        else
        {
            pause = false;
            player.GetComponent<controller>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update () {
       
        if((gameDuration < 0 || !player.GetComponent<Ragdoll2D>().IsAlive()) && !pause)
        {
            SetPause(true);
            gameUI.SetActive(false);
            stopUI.SetActive(true);

            scoreDisplay.text = ((int)(player.GetComponent<controller>().score)).ToString();
        }
        else if (gameDuration >= 0.0f)
        {
            gameDuration -= Time.deltaTime;
        }
    }
}
