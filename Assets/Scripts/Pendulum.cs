using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour {

    private float t;
    public float speed;
    public float angle;

    // Use this for initialization
    void Start () {
        t = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        t += speed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Sin(t));
	}
}
