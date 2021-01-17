using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : MonoBehaviour
{
    [SerializeField] private float range = 10f;
    [SerializeField] private float timeBetweenAttacks = 1f;

    private Animator anim;
    private GameObject player;
    private bool playerInRange;

    public float arrowSpeed = 600f;
    public Transform arrowSpawn;
    public Rigidbody arrowPrefab;

    Rigidbody clone;

    private void Start()
    {
        anim = GetComponent<Animator>();
        arrowSpawn = GameObject.Find("ArrowSpawn").transform;
        player = GameManager.instance.Player;
    }
    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < range)
        {
            playerInRange = true;
            anim.SetTrigger("isAttacking");
        }
        else
        {
            playerInRange = false;
        }
        Debug.Log("Player in range " + playerInRange);
    }
    public void FireArcherProjectile()
    {
        clone = Instantiate(arrowPrefab, arrowSpawn.position, arrowSpawn.rotation) as Rigidbody;
        clone.AddForce(arrowSpawn.transform.forward * arrowSpeed);
    }
}
