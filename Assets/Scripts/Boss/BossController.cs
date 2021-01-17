using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public bool bossAwake = false;
    private Animator anim;

    public bool inBattle = false;
    public bool attacking = false;
    public float idleTimer = 0.0f;
    public float idleWaitTime = 10.0f;

    private BossHealth bossHealth;
    private float attackTimer = 0.0f;
    private float attackWaitTime = 4.0f;

    private BoxCollider swordTrigger;

    public GameObject bossHealthBar;

    private SmoothFollow smoothFollow;
    private GameObject player;
    private PlayerHealth playerHealth;

    private BoxCollider bossCheckPoint;

    private ParticleSystem particleSystem;


    private void Start()
    {
        anim = GetComponent<Animator>();
        bossHealth = GetComponent<BossHealth>();
        swordTrigger = GetComponentInChildren<BoxCollider>();
        bossHealthBar.SetActive(false);
        smoothFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothFollow>();
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        bossCheckPoint = GameObject.FindGameObjectWithTag("BossCheckPoint").GetComponent<BoxCollider>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (bossAwake)
        {
            print("boss is awake");
            anim.SetBool("BossAwake", true);
            bossHealthBar.SetActive(true);

            if (inBattle)
            {
                if (!attacking)
                {
                    idleTimer += Time.deltaTime;
                }
                else
                {
                    idleTimer = 0.0f;
                    attackTimer += Time.deltaTime;
                    if(attackTimer >= attackWaitTime)
                    {
                        switch (Random.Range(0, 3))
                        {
                            case 0:
                                BossAttack01();
                                break;
                            case 1:
                                BossAttack02();
                                break;
                            case 2:
                                BossAttack03();
                                break;
                        }
                    }
                }
                if(idleTimer >= idleWaitTime)
                {
                    print("Boss is attacking");
                    attacking = true;
                    idleTimer = 0.0f;
                }
            }
            else
            {
                idleTimer = 0.0f;
            }
            if(bossHealth.bossHealth > 0 && playerHealth.CurrentHealth > 0)
            {
                if (bossHealth.bossHealth > 15)
                {
                    attackWaitTime = 4.0f;
                }
                if (bossHealth.bossHealth > 10 && bossHealth.bossHealth<17)
                {
                    attackWaitTime = 3.0f;
                }
                if (bossHealth.bossHealth > 5 && bossHealth.bossHealth < 11)
                {
                    attackWaitTime = 2.0f;
                }
                if (bossHealth.bossHealth > 1 && bossHealth.bossHealth < 6)
                {
                    attackWaitTime = 1.0f;
                }
            }
        }
        BossReset();
    }
    private void BossReset()
    {
        if(playerHealth.CurrentHealth == 0)
        {
            bossAwake = false;
            bossCheckPoint.isTrigger = true;
            print("Boss is sleeping again");
            smoothFollow.bossCameraActive = false;
            anim.Play("Idle");
            anim.SetBool("BossAwake", false);
            bossHealth.bossHealth = 20;
        }
    }
    void BossAttack01()
    {
        attacking = true;
        anim.SetTrigger("BossAttack");
        attackTimer = 0.0f;
        print("Boss smash");
        swordTrigger.enabled = true;
        print("SwordTrigger is enabled");
    }
    void BossAttack02()
    {
        attacking = true;
        anim.SetTrigger("BossAttack02");
        attackTimer = 0.0f;
        print("Boss Attack02");
        swordTrigger.enabled = true;
    }
    void BossAttack03()
    {
        attacking = true;
        anim.SetTrigger("BossAttack03");
        attackTimer = 0.0f;
        print("Boss Attack03");
        swordTrigger.enabled = true;
        StartCoroutine(FallingRocks());
    }
    IEnumerator FallingRocks()
    {
        yield return new WaitForSeconds(2);
        particleSystem.enableEmission = true;
        particleSystem.Play();
        yield return new WaitForSeconds(3);
        particleSystem.enableEmission = false;
    }
}
