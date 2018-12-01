using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendOrFoe : MonoBehaviour {

    public bool friend = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        FriendOrFoe other = collision.collider.gameObject.GetComponent<FriendOrFoe>();
        if(other!=null&&friend !=other.friend){
            StartCoroutine(DestroyDelayed());
        }
    }

    IEnumerator DestroyDelayed()
    {
        yield return new WaitForEndOfFrame();
        Destroy(this.gameObject);
    }
}
