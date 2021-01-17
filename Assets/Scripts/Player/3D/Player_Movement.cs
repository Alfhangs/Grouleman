using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public float maxSpeed;
    public float moveDirection;

    Animator anim;
    Rigidbody rigidbody;
    Vector3 movement;
    Quaternion rotation;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }
    public float HorizontalMovement
    {
        get { return Input.GetAxis("Horizontal"); }
    }
    public float VerticalMovement
    {
        get { return Input.GetAxis("Vertical"); }
    }
    void FixedUpdate()
    {
        moveDirection = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * speed * VerticalMovement * Time.deltaTime);
        transform.Rotate(Vector3.up * turnSpeed * HorizontalMovement * Time.deltaTime);
        //anim.SetFloat("IsWalking", Mathf.Abs(moveDirection));
        anim.SetFloat("Speed", Mathf.Abs(moveDirection));
    }
   

}