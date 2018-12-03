using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform player;
    public Transform background;

    public float threshold;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {/*
        float x = Mathf.Clamp(player.position.x, background.position.x - 0.5f * background.localScale.x, background.position.x + 0.5f * background.localScale.x);
        float y = Mathf.Clamp(player.position.y, background.position.y - 0.5f * background.localScale.y, background.position.y + 0.5f * background.localScale.y);
        transform.position = new Vector3(x, y, transform.position.z);
        */
        Vector3 targetPos =  new Vector3(player.position.x, player.position.y, transform.position.z);
        Vector3 distance = targetPos- this.transform.position;
        float d =distance.magnitude;
        if (d>threshold)
        {
            this.transform.position = this.transform.position + distance.normalized * (d- threshold/2) ;
        }
        
    }
}
