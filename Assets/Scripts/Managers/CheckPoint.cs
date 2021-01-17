using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            levelManager.currentCheckPoint = gameObject;
        }
    }
}
