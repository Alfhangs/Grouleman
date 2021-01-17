using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public float maxSpeed = 6.0f;
    public float moveDirection;
    public bool facingRight = true;

    Rigidbody rigidbody;
    private Animator anim;

    public float jumpSpeed = 800.0f;
    public bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    public float knifeSpeed = 600.0f;
    public Transform KnifeSpawn;
    public Rigidbody knifePrefab;
    Rigidbody clone;

    private AudioSource audio;
    public AudioClip jumpAudio;
    public AudioClip knifeAudio;
    public Joystick joystick;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
    }
    private void FixedUpdate()
    {
        //moveDirection = joystick.Horizontal;
        moveDirection = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(moveDirection * maxSpeed, rigidbody.velocity.y);

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        anim.SetFloat("Speed", Mathf.Abs(moveDirection));
        if (moveDirection > 0.0f && !facingRight)
        {
            Flip();
        }
        else if (moveDirection < 0.0f && facingRight)
        {
            Flip();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }
    private void Update()
    {
        //moveDirection = joystick.Horizontal;
        moveDirection = Input.GetAxis("Horizontal");
        if (grounded && Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("isJumping");
            rigidbody.AddForce(new Vector2(0, jumpSpeed));
            audio.PlayOneShot(jumpAudio);
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180f, Space.World);
    }
    //public void Jump()
    //{
    //    print("boton salto");
    //    if (grounded)
    //    {
    //        print("Salto");
    //        anim.SetTrigger("isJumping");
    //        rigidbody.AddForce(new Vector2(0, jumpSpeed));
    //        audio.PlayOneShot(jumpAudio);
    //    }
    //}
    public void Attack()
    {
        anim.SetTrigger("attacking");
    }
    public void CallFireProjectile()
    {
        clone = Instantiate(knifePrefab, KnifeSpawn.position, KnifeSpawn.rotation) as Rigidbody;
        clone.AddForce(KnifeSpawn.transform.forward * knifeSpeed);
        audio.PlayOneShot(knifeAudio);
    }
}
