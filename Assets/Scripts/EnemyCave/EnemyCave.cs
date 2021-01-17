using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCave : MonoBehaviour
{
    [SerializeField] private int startingHealth = 50;
    [SerializeField] private float timeSinceLasthit = 0.2f;
    [SerializeField] private float dissapearSpeed = 2f;
    [SerializeField] private int currentHealth;

    private float timer = 0f;
    private Animator anim;
    private bool isAlive;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private bool dissapearEnemy = false;
    private AudioSource audio;
    public AudioClip hurtAudio;
    public AudioClip killAudio;
    private DropItem dropItem;
    public GameObject explosionEffect;

    public bool IsAlive { get { return isAlive; } }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        isAlive = true;
        currentHealth = startingHealth;
        audio = GetComponent<AudioSource>();
        dropItem = GetComponent<DropItem>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(timer >= timeSinceLasthit && !GameManager.instance.GameOver)
        {
            if(other.tag == "PlayerWeapon")
            {
                TakeHit();
                timer = 0;
            }
        }
    }
    void TakeHit()
    {
        if (currentHealth > 0)
        {
            GameObject newExplosionEffect = (GameObject)Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(newExplosionEffect, 1);

            anim.Play("Hurt");
            currentHealth -= 10;
            audio.PlayOneShot(hurtAudio);
        }
        if (currentHealth <= 0)
        {
            isAlive = false;
            KillEnemy();
        }
    }
    void KillEnemy()
    {
        boxCollider.enabled = false;
        anim.SetTrigger("EnemyDie");
        audio.PlayOneShot(killAudio);

        StartCoroutine(RemoveEnemy());
        dropItem.Drop();
    }
    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
