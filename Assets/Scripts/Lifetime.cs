using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Lifetime : MonoBehaviour {

    // Use this for initialization
    void Start () {
        Destroy(gameObject, 1.0f);
    }
}
