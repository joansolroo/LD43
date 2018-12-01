using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LauncherBox : MonoBehaviour {

    public GameObject player;
    public GameObject splashScreen;
    private bool launched;
    public string scene;

    // Use this for initialization
    void Start () {
        launched = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(!launched)
        {
            launched = true;
            splashScreen.SetActive(true);
            player.GetComponent<controller>().enabled = false;
        }
    }

    public void CloseCallback()
    {
        splashScreen.SetActive(false);
        player.GetComponent<controller>().enabled = true;
    }

    public void NextScene()
    {
        splashScreen.SetActive(false);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    void Update ()
    {
        if (string.IsNullOrEmpty(scene) && Input.GetButtonDown("Submit"))
            CloseCallback();
        else if (Input.GetButtonDown("Submit"))
            NextScene();
    }
}
