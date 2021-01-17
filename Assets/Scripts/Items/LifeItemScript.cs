using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeItemScript : MonoBehaviour
{
    private GameObject player;
    private AudioSource audio;
    private LifeManager lifeManager;
    private SpriteRenderer spriteRenderer;
    private BoxCollider boxCollider;
    private void Start()
    {
        player = GameManager.instance.Player;
        lifeManager = FindObjectOfType<LifeManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            PickLife();
            print("Life Collected");
        }
    }
    public void PickLife()
    {
        lifeManager.GiveLife();
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
        Destroy(gameObject);
    }
}
