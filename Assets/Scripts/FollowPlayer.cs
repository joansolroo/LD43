﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform player;
    public Transform floor;

    public float threshold;
    Camera camera;
    // Use this for initialization
    void Start () {
        camera = GetComponent<Camera>();
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
        
        if(floor!=null && (transform.position.y-floor.transform.position.y - camera.orthographicSize) <0 )
        {
           // Debug.Log(transform.position.y - floor.transform.position.y - camera.orthographicSize);
            Vector3 pos = transform.position;
            pos.y = floor.transform.position.y+camera.orthographicSize;
            transform.position = pos;
        }
        
    }
}
