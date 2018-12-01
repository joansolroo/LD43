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

    // Update is called once per frame
    void Update () {
       
        if((gameDuration < 0 || !player.GetComponent<Ragdoll2D>().IsAlive()) && !pause)
        {
            pause = true;
            gameUI.SetActive(false);
            stopUI.SetActive(true);

            scoreDisplay.text = ((int)(player.GetComponent<controller>().score)).ToString();

            player.GetComponent<controller>().enabled = false;
        }
        else if (gameDuration >= 0.0f)
        {
            gameDuration -= Time.deltaTime;
        }
    }
}
