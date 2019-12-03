using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLight : MonoBehaviour {

    [SerializeField] public bool on = true;
    [SerializeField] float intensity = 0.3f;

    [SerializeField] Transform target;
    [SerializeField] float followDelay = 0.95f;

    [SerializeField] SpriteRenderer lightSprite;
    // Use this for initialization
    void Start () {

        lightSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}
    float prevRot_z = 0;
    bool init = true;

    // Update is called once per frame
    void Update () {
        if (on)
        {
            Vector3 diff = target.transform.position - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            if (init)
            {
                prevRot_z = rot_z;
                init = false;

            }
            rot_z = (1 - followDelay) * rot_z + followDelay * prevRot_z;
            prevRot_z = rot_z;

            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
            //this.transform.LookAt(ragdoll.center.transform.position, Vector3.right);

            lightSprite.color = new Color(1, 1, 1, Mathf.MoveTowards(lightSprite.color.a, intensity, 2 * Time.deltaTime));
        }
        else
        {
            lightSprite.color = new Color(1, 1, 1, Mathf.MoveTowards(lightSprite.color.a, 0,2*Time.deltaTime));
        }
	}


    public void TurnOn()
    {
        on = true;
    }
    public void TurnOff()
    {
        on = false; 
    }

}
