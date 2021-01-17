using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy02Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 20;
    [SerializeField] private float timeSinceLastHit = 0.5f;
    [SerializeField] private float dissapearSpeed = 2f;
    [SerializeField] private int currentHealth;

    private float timer = 0f;
    private Animator anim;
    private bool isAlive;
    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private bool dissapearEnemy = false;
    private AudioSource audio;

    public AudioClip hitAudio;
    public AudioClip killAudio;

    private DropItem dropItem;
    public bool IsAlive { get { return isAlive; } }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        isAlive = true;
        currentHealth = startingHealth;
        audio = GetComponent<AudioSource>();
        dropItem = GetComponent<DropItem>();
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
                timer = 0f;
            }
        }
    }
    void TakeHit()
    {
        if (currentHealth > 0)
        {
            anim.Play("Hit");
            currentHealth -= 10;
            audio.PlayOneShot(hitAudio);
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
        anim.SetTrigger("EnemyDie");
        rigidbody.isKinematic = true;
        audio.PlayOneShot(killAudio);

        StartCoroutine(RemoveEnemy());
        print("drop random item");
        dropItem.Drop();
    }
    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(2f);
        dissapearEnemy = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
