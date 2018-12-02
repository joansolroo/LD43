using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGenerator : MonoBehaviour {

    [SerializeField] float force = 10;
    [SerializeField] float holdTime = 0;

    Collider2D collider;

    private void Start()
    {
        collider = this.gameObject.GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ragdoll2DPart RDpart = collision.gameObject.GetComponent<Ragdoll2DPart>();
        if (RDpart != null)
        {
            StartCoroutine(LaunchWithDelay(RDpart.ragdoll));
        }
        //Debug.LogError("qshk");
    }

    bool launching = false;
    IEnumerator LaunchWithDelay(Ragdoll2D ragdoll)
    {
        if (!launching)
        {
            launching = true;
            collider.enabled = false;
            yield return new WaitForSeconds(0.5f);
            ragdoll.Simulated = false;
            yield return new WaitForSeconds(holdTime);
            ragdoll.Simulated = true;
            //yield return new WaitForSeconds(0.1f);
            ragdoll.ResetVelocty();
            ragdoll.AddForceCenter(this.transform.right * force);
            Debug.DrawLine(ragdoll.center.transform.position, ragdoll.center.transform.position + this.transform.right * force);
            yield return new WaitForSeconds(2f);
            launching = false;
            collider.enabled = true;
        }

    }
}
