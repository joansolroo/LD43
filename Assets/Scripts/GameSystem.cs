using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour {

    public float gameDuration = 60.0f;
    public bool pause;

    public GameObject player;
    public Canvas gameUI;
    public Canvas stopUI;

    // Use this for initialization
    void Start () {
        pause = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(gameDuration < 0 && !pause)
        {
            pause = true;
            gameUI.enable = false;
            stopUI.enable = false;
            player.GetComponent<controller>().enabled = false;
        }
        else if (gameDuration >= 0.0f)
        {
            gameDuration -= Time.deltaTime;
        }
    }
}
