using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGravity : MonoBehaviour {

    [SerializeField] Vector2 gravity;
    Vector2 prevGravity;

    private void OnEnable()
    {
        prevGravity = Physics2D.gravity;
        Physics2D.gravity = gravity;
    }

    private void OnDisable()
    {
        Physics2D.gravity = prevGravity;
    }
}
