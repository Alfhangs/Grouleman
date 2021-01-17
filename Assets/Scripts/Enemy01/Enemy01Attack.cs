using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Attack : MonoBehaviour
{
    [SerializeField] private float range = 3.0f;
    [SerializeField] private float timeBetweenAttack = 1.0f;

    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private BoxCollider weaponCollider;

    private Enemy01Health enemy01Health;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameManager.instance.Player;
        weaponCollider = GetComponentInChildren<BoxCollider>();
        StartCoroutine(attack());
        enemy01Health = GetComponent<Enemy01Health>();
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range && enemy01Health.IsAlive)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
        //Debug.Log("Player in range" + playerInRange);
    }

    IEnumerator attack()
    {
        if (playerInRange && !GameManager.instance.GameOver)
        {
            anim.Play("Attacking");
            yield return new WaitForSeconds(timeBetweenAttack);
        }
        yield return null;
        StartCoroutine(attack());
    }
}
