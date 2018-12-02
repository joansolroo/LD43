using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpShower : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] float duration = 10;

    [SerializeField] Transform up;
    [SerializeField] Transform left;
    [SerializeField] Transform right;

    [SerializeField] SpriteRenderer upInfo;
    [SerializeField] SpriteRenderer leftInfo;
    [SerializeField] SpriteRenderer rightInfo;

    Vector3 upPos;
    // Use this for initialization
    void Start () {
        upPos = up.localPosition;
        StartCoroutine(StopAfter(duration));
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = target.position;
        transform.eulerAngles = new Vector3(0,0,Mathf.Sin(Time.time)*5);

        float h = Input.GetAxis("Horizontal");
        if (h < 0)
        {
            left.localRotation = Quaternion.RotateTowards(left.localRotation, Quaternion.Euler(0, 0, 20), 1);
            leftInfo.color = Color.gray;
        }
        else
        {
            left.localRotation = Quaternion.RotateTowards(left.localRotation, Quaternion.Euler(0, 0, 0), 5);
            leftInfo.color = new Color(0,0,0,0);
        }
        if (h > 0)
        {
            right.localRotation = Quaternion.RotateTowards(right.localRotation, Quaternion.Euler(0, 0, -20), 1);
            rightInfo.color = Color.gray;
        }
        else
        {
            right.localRotation = Quaternion.RotateTowards(right.localRotation, Quaternion.Euler(0, 0, 0), 5);
            rightInfo.color = new Color(0, 0, 0, 0);
        }
        if (Input.GetKey(KeyCode.Space)){
            up.localPosition = Vector3.MoveTowards(up.localPosition, upPos+Vector3.up, 2f);
            upInfo.color = Color.gray;
        }
        else
        {
            up.localPosition = Vector3.MoveTowards(up.localPosition, upPos, 1f);
            upInfo.color = new Color(0, 0, 0, 0);
        }
    }

    IEnumerator StopAfter(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }
}
