using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 100;
    [SerializeField] float timeSinceLastHit = 2.0f;
    [SerializeField] int currentHealth;
    [SerializeField] private float timer = 0f;
    private Animator anim;

    private CharacterMovement CharacterMovement;
    [SerializeField] Slider healthSlider;

    private AudioSource audio;
    public AudioClip hurtAudio;
    public AudioClip deadAudio;
    public AudioClip pickItem;

    private ParticleSystem particle;

    public LevelManager levelManager;
    public bool isDead;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (value < 0)
                currentHealth = 0;
            else
                currentHealth = value;
        }
    }
    public Slider HealthSlider
    {
        get { return healthSlider; }
    }
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        CharacterMovement = GetComponent<CharacterMovement>();
        particle = GetComponent<ParticleSystem>();
        particle.enableEmission = false;
        levelManager = FindObjectOfType<LevelManager>();
        isDead = false;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        PlayerKill();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if (other.tag == "Weapon")
            {
                TakeHit();
                timer = 0;
            }
        }
    }
    public void TakeHit()
    {
        if (currentHealth > 0)
        {
            GameManager.instance.PlayerHit(currentHealth);
            anim.Play("Hurt");
            currentHealth -= 10;
            healthSlider.value = currentHealth;
            audio.PlayOneShot(hurtAudio);
        }
        if (currentHealth <= 0)
        {
            //GameManager.instance.PlayerHit(currentHealth);
            anim.SetTrigger("isDead");
            audio.PlayOneShot(deadAudio);
            CharacterMovement.enabled = false;
        }
    }

    public void PowerUPHealth()
    {
        
        if (currentHealth <= 80)
        {
            currentHealth += 20;
        }
        else if (currentHealth < startingHealth)
        {
            currentHealth = startingHealth;
        }

        healthSlider.value = currentHealth;
        audio.PlayOneShot(pickItem);
    }
    public void KillBox()
    {
        currentHealth = 0;
        healthSlider.value = currentHealth;
    }
    public void PlayerKill()
    {
        if (currentHealth <= 0)
        {
            CharacterMovement.enabled = false;
            levelManager.RespawnPlayer();
        }
    }
}
