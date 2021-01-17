using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy03Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 10;
    [SerializeField] private int currentHealth;

    private Rigidbody rigidbody;
    private SphereCollider sphereCollider;
    private AudioSource audio;
    public AudioClip killAudio;
    public GameObject explosionEffect;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        audio = GetComponent<AudioSource>();
        currentHealth = startingHealth;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.instance.GameOver)
        {
            if(other.tag == "PlayerWeapon")
            {
                TakeHit();
            }
        }
    }
    void TakeHit()
    {
        if (currentHealth > 0)
        {
            GameObject newExplosionEffect = Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            currentHealth -= 10;
        }
        if(currentHealth <= 0)
        {
            KillEnemy();
        }
    }
    void KillEnemy()
    {
        sphereCollider.enabled = false;
        audio.PlayOneShot(killAudio);
        Destroy(gameObject);
    }
}
