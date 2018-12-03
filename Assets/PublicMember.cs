using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicMember : MonoBehaviour {

    Vector3 initialPos;
    public float happiness= 5;
    float initialOffset;
    [SerializeField] bool visible;
	// Use this for initialization
	void Start () {
        initialPos = transform.position;
        initialOffset = Random.RandomRange(0, 10);
        t = initialOffset;
    }
    float t;
    // Update is called once per frame
    void Update () {

        if (visible)
        {
            t += Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, initialPos + new Vector3(0, (1 + Mathf.Cos(t * happiness)) * 0.5f * happiness), happiness*5 * Time.deltaTime);
            this.transform.eulerAngles = new Vector3(0, 0, Mathf.Sin(t * happiness) * 15);
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, initialPos + new Vector3(0, -10,0),2);
        }
	}
}
