using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItemScripts : MonoBehaviour
{
    private GameObject player;
    private AudioSource audio;
    private ParticleSystem particleSystem;
    private PlayerHealth playerHealth;

    private MeshRenderer meshRenderer;
    private ParticleSystem brainParticles;

    public GameObject pickupEffect;

    private ItemExplode itemExplode;
    private SphereCollider sphereCollider;

    private void Start()
    {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.enabled = true;

        particleSystem = player.GetComponent<ParticleSystem>();
        particleSystem.enableEmission = false;

        meshRenderer = GetComponent<MeshRenderer>();
        brainParticles = GetComponent<ParticleSystem>();
        itemExplode = GetComponent<ItemExplode>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            StartCoroutine(InvincibleRoutine());
            meshRenderer.enabled = false;
        }
    }

    IEnumerator InvincibleRoutine()
    {
        print("Pick PowerItem");
        itemExplode.Pickup();
        particleSystem.enableEmission = true;
        playerHealth.enabled = false;
        brainParticles.enableEmission = false;
        sphereCollider.enabled = false;

        yield return new WaitForSeconds(10f);
        print("No more invincible");
        particleSystem.enableEmission = false;
        playerHealth.enabled = true;
        Destroy(gameObject);

    }
    void Pickup()
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);
    }
}
