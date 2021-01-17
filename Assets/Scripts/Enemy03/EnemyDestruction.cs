using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestruction : MonoBehaviour
{
    public float lifeSpawn = 10;

    private void Start()
    {
        Destroy(gameObject, lifeSpawn);
    }
}
