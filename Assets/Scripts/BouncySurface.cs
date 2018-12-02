using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncySurface : MonoBehaviour {

    [SerializeField] float bounciness = 10;
    [SerializeField] float minFactor = 3;
    [SerializeField] float maxFactor = 3;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        float proj = Vector2.Dot(collision.contacts[0].normal, collision.relativeVelocity);

        Vector2 v = -(collision.relativeVelocity + 2 * proj * collision.contacts[0].normal);
        v = Mathf.Max(minFactor,Mathf.Min(maxFactor, v.magnitude)) * v.normalized;

        Ragdoll2DPart RDpart = collision.gameObject.GetComponent<Ragdoll2DPart>();
        if (RDpart!=null)
        {
            RDpart.ragdoll.ResetVelocty();
            RDpart.ragdoll.AddVelocity(v * bounciness); 
            Debug.DrawLine(RDpart.ragdoll.center.transform.position, RDpart.ragdoll.center.transform.position + new Vector3(v.x * 0.1f, v.y * 0.1f,0));
        }
        else if(collision.rigidbody!=null)
        {
            collision.rigidbody.AddForce(v * bounciness*collision.rigidbody.mass);
            Debug.DrawLine(collision.contacts[0].point, collision.contacts[0].point + v * 0.1f);
        }
       
        //Debug.LogError("qshk");
    }
}
