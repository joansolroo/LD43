using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncySurface : MonoBehaviour
{

    [SerializeField] float bounciness = 10;
    [SerializeField] float minFactor = 3;
    [SerializeField] float maxFactor = 3;
    [SerializeField] AudioSource sound;

    Vector3 scale;

    [SerializeField] bool forceBack = false;
    private void Start()
    {
        scale = this.transform.localScale;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float proj = Vector2.Dot(collision.contacts[0].normal, collision.relativeVelocity);

        Vector2 v = -(collision.relativeVelocity + 2 * proj * collision.contacts[0].normal);
        float speed = Mathf.Max(0, Mathf.Min(1, (v.magnitude - minFactor + 1) / (maxFactor - minFactor + 1)) - 0.5f);
        v = Mathf.Max(minFactor, Mathf.Min(maxFactor, v.magnitude)) * v.normalized;

        Ragdoll2DPart RDpart = collision.gameObject.GetComponent<Ragdoll2DPart>();

        if (RDpart != null && RDpart.attached)
        {
            if (!sound.isPlaying)
            {
                sound.pitch = Random.Range(0.8f, 1.2f);
                sound.Play();
            }
            RDpart.ragdoll.ResetVelocty();
            RDpart.ragdoll.AddVelocity(v * bounciness);
            Debug.DrawLine(RDpart.ragdoll.center.transform.position, RDpart.ragdoll.center.transform.position + new Vector3(v.x * 0.1f, v.y * 0.1f, 0));
        }
        else if (collision.rigidbody != null)
        {
            collision.rigidbody.AddForce(v * bounciness * collision.rigidbody.mass);
            Debug.DrawLine(collision.contacts[0].point, collision.contacts[0].point + v * 0.1f);
        }
        if (forceBack)
        {
            Vector2 inverseForce = -v;
            //inverseForce.x = -v.x;
            this.GetComponent<Rigidbody2D>().AddForce(inverseForce);
        }
        //StartCoroutine(BouncyAnimation(Mathf.Lerp(0.05f, 0.10f, speed*speed), Mathf.Lerp(1, 0.75f, speed * speed)));
        //Debug.LogError("qshk");
    }
    bool animating = false;
    IEnumerator BouncyAnimation(float duration = 0.15f, float minScale = 0.75f)
    {
        if (!animating && minScale<1)
        {
            animating = true;
            float t = 0;
            
            Vector3 minSize = scale * minScale;
            while (t < duration)
            {
                this.transform.localScale = Vector3.MoveTowards(this.transform.localScale, minSize, t / duration);
                t += Time.deltaTime;
                yield return new WaitForEndOfFrame();

            }
            t = 0;
            while (t < duration)
            {
                this.transform.localScale = Vector3.MoveTowards(this.transform.localScale, scale, t / duration);
                t += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            this.transform.localScale = scale;
            animating = false;
        }
    }
}
