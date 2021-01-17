﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItemScript : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth playerHealth;

    private void Start()
    {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerHealth.PowerUPHealth();
            Destroy(gameObject);
        }
    }
}
