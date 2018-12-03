using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicMember : MonoBehaviour {

    Vector3 initialPos;
    public float happiness= 5;
    float initialOffset;
	// Use this for initialization
	void Start () {
        initialPos = transform.position;
        initialOffset = Random.RandomRange(0, 10);
        t = initialOffset;
    }
    float t;
    // Update is called once per frame
    void Update () {
        t+= Time.deltaTime;
        this.transform.position = initialPos+new Vector3(0, (1+Mathf.Cos(t * happiness)) * 0.5f * happiness);
        this.transform.eulerAngles = new Vector3(0, 0, Mathf.Sin(t* happiness) * 15);
	}
}
