using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour {

    public float gameDuration;
    public bool pause;

    public GameObject player;
    public GameObject gameUI;
    public GameObject stopUI;

    public Text timeDisplay;
    public Text endScoreDisplay;
    public Button retry, quit;

    public string[] scenes;
    public Fade fader;


    // Use this for initialization
    void Start() {
        pause = false;
        //gameUI.SetActive(true);
        //stopUI.SetActive(false);

        quit.onClick.AddListener(QuitGame);
        retry.onClick.AddListener(RetryLevel);
        fader.FadeIn();
        StartCoroutine(LoadSceneDelayed(scenes[0], 10, true));
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

        timeDisplay.text = ((int)gameDuration).ToString();

        if((gameDuration < 0 || !player.GetComponent<Ragdoll2D>().IsAlive()) && !pause)
        {
            SetPause(true);
            //gameUI.SetActive(false);
            //stopUI.SetActive(true);

            endScoreDisplay.text = ((int)(player.GetComponent<controller>().score)).ToString();
            player.GetComponent<Rigidbody2D>().isKinematic = true;
        }
        else if (gameDuration >= 0.0f)
        {
            gameDuration -= Time.deltaTime;
        }
    }

    IEnumerator LoadSceneDelayed(string level, float time, bool incremental = false)
    {

        yield return new WaitForSeconds(time);
        fader.FadeToWhite();
        SceneManager.LoadScene(level, incremental?LoadSceneMode.Additive : LoadSceneMode.Single);
        yield return new WaitForSeconds(15);
        fader.FadeToWhite();
        SceneManager.UnloadScene(level);
    }
}

