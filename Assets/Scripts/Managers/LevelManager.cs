using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float timer = 0f;
    public float waitTime;

    public GameObject currentCheckPoint;
    private GameObject player;
    private PlayerHealth playerHealth;

    private CharacterMovement characterMovement;

    private Animator anim;
    private LifeManager lifeSystem;

    private void Start()
    {
        lifeSystem = FindObjectOfType<LifeManager>();
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        characterMovement = player.GetComponent<CharacterMovement>();
        anim = player.GetComponent<Animator>();
    }
    public void RespawnPlayer()
    {
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            print("Player Respawn");
            lifeSystem.TakeLife();
            player.transform.position = currentCheckPoint.transform.position;
            playerHealth.CurrentHealth = 100;
            timer = 0f;
            playerHealth.HealthSlider.value = playerHealth.CurrentHealth;
            characterMovement.enabled = true;
            anim.Play("Blend Tree");
        }

    }
}
