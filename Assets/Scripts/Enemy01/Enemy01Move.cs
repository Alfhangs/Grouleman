using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy01Move : MonoBehaviour
{
    [SerializeField] private Transform player;
    private NavMeshAgent nav;
    private Animator anim;
    private Enemy01Health enemy01Health;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemy01Health = GetComponent<Enemy01Health>();
    }

    void Update()
    {
        if(Vector3.Distance(player.position, this.transform.position) < 10)
        {
            if(!GameManager.instance.GameOver && enemy01Health.IsAlive)
            {
                nav.SetDestination(player.position);
                anim.SetBool("isWalking", true);
                anim.SetBool("isIdle", false);
            }        
        }
        else if (GameManager.instance.GameOver || !enemy01Health.IsAlive)
        { 
            Debug.Log("GAMEOVER");
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", true);
            nav.enabled = false;
        }
    }
}
