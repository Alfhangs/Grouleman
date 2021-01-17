using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float range = 15f;
    [SerializeField] private float timeBetweenSpawn = 1f;

    private GameObject player;
    private bool playerInRange;

    public Transform enemySpawn;
    public Rigidbody enemyPrefab;

    Rigidbody clone;

    private void Start()
    {
        player = GameManager.instance.Player;
        StartCoroutine(SpawnEnemies());
    }
    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position)< range)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
        Debug.Log("Player in Spawner range " + playerInRange);
    }

    public IEnumerator SpawnEnemies()
    {
        if(playerInRange && !GameManager.instance.GameOver)
        {
            clone = Instantiate(enemyPrefab, enemySpawn.position, enemySpawn.rotation) as Rigidbody;
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
        yield return null;
        StartCoroutine(SpawnEnemies());
    }
}
