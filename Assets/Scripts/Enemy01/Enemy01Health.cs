using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy01Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 20;
    [SerializeField] private float timeSinceLastHit = 0.5f;
    [SerializeField] private float dissapearSpeed = 2.0f;
    [SerializeField] private int currentHealth;

    private float timer = 0f;
    private Animator anim;
    private NavMeshAgent nav;
    private bool isAlive;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private bool dissapearEnemy = false;
    private BoxCollider weaponCollider;

    private AudioSource audio;
    public AudioClip hurtAudio;
    public AudioClip deadAudio;

    public bool IsAlive
    {
        get { return isAlive; }
    }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        weaponCollider = GetComponentInChildren<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        isAlive = true;
        currentHealth = startingHealth;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (dissapearEnemy)
            transform.Translate(Vector3.down * dissapearSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(timer >= timeSinceLastHit && !GameManager.instance.GameOver)
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
        capsuleCollider.enabled = false;
        nav.enabled = false;
        anim.SetTrigger("EnemyDie");
        rb.isKinematic = true;
        weaponCollider.enabled = false;
        audio.PlayOneShot(deadAudio);
        StartCoroutine(RemoveEnemy());
    }
    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(2f);
        dissapearEnemy = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
