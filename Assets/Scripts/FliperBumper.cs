using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FliperBumper : MonoBehaviour {

    private float t;
    public float speed;
    public float d;

    private Transform initPos;

    // Use this for initialization
    void Start () {
        t = Random.Range(-10.0f, 10.0f);
        initPos = transform;
    }
	
	// Update is called once per frame
	void Update () {
        t += speed * Time.deltaTime;
        transform.position = initPos.position + new Vector3(d * Mathf.Sin(t), 0, 0);
	}
}
