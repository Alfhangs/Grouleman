﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    private CharacterMovement characterMovement;
    private Animator playerAnim;
    private BossController bossController;

    // Start is called before the first frame update
    void Start()
    {
        characterMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        bossController = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();
    }
    
    public void EnableBossBattle()
    {
        characterMovement.enabled = true;
        playerAnim.Play("Blend Tree");
        bossController.inBattle = true;
    }
}
