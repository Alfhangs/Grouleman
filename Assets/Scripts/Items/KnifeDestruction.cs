using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeDestruction : MonoBehaviour
{
    public float lifeSpawn = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            Destroy(gameObject);
        }
    }
}
