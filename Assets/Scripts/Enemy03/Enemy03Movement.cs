using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy03Movement : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody rigidbody;
    private Transform transform;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        Vector2 vel = rigidbody.velocity;
        vel.x = -moveSpeed;
        rigidbody.velocity = vel;
    }
}
