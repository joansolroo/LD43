using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLight : MonoBehaviour {


    [SerializeField] Transform target;
	// Use this for initialization
	void Start () {
		
	}
    float prevRot_z = 0;
    bool init = true;
    // Update is called once per frame
    void Update () {
        Vector3 diff = target.transform.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        if (init)
        {
            prevRot_z = rot_z;
            init = false;
            
        }
        rot_z = 0.05f * rot_z + 0.95f * prevRot_z; 
        prevRot_z = rot_z;

        transform.rotation = Quaternion.Euler(0f, 0f, rot_z );
        //this.transform.LookAt(ragdoll.center.transform.position, Vector3.right);
	}
}
