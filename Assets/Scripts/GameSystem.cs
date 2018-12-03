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
    public string[] transitionsScenes;
    public float[] scenesTime;
    public float[] transitionTime;
    private int index = 0;
    public Fade fader;

    public Camera camera;
    // Use this for initialization
    void Start() {
        pause = false;
        //gameUI.SetActive(true);
        //stopUI.SetActive(false);

        quit.onClick.AddListener(QuitGame);
        retry.onClick.AddListener(RetryLevel);
        fader.FadeIn(8);
        StartCoroutine(LoadSceneDelayed(true));
        camera = Camera.main;
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

    
    IEnumerator LoadSceneDelayed(bool incremental = false)
    {

        yield return new WaitForSeconds(15f);
        fader.FadeToWhite(0.25f);
        yield return new WaitForSeconds(0.26f);
        camera.enabled = false;

        for (int i = 0; i < 4; i++)
        {
            
            SceneManager.LoadScene(scenes[i], incremental ? LoadSceneMode.Additive : LoadSceneMode.Single);
            fader.FadeIn(1f);

            yield return new WaitForSeconds(scenesTime[i]);

            fader.FadeToBlack(0.5f);
            yield return new WaitForSeconds(0.51f);
            SceneManager.LoadScene(transitionsScenes[i], incremental ? LoadSceneMode.Additive : LoadSceneMode.Single);
            SceneManager.UnloadScene(scenes[i]);
            fader.FadeIn(2f);

            yield return new WaitForSeconds(transitionTime[i]);

            fader.FadeToWhite(0.5f);
            yield return new WaitForSeconds(0.51f);
            SceneManager.UnloadScene(transitionsScenes[i]);
            //sorry
            if (i==transitionsScenes.Length-1)
            {
                i = -1;
            }
        }
    }
}

