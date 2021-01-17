using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public int bossHealth = 20;
    private Animator anim;
    public bool bossDead = false;
    private BossController bossController;

    private CapsuleCollider capsuleCollider;
    private BoxCollider weaponCollider;
    private SphereCollider triggerCollider;

    public Material hurtBossMaterial;
    private GameObject bossModel;

    public GameObject videoPlayer;
    private void Start()
    {
        videoPlayer.SetActive(false);
        anim = GetComponentInParent<Animator>();
        bossController = GetComponentInParent<BossController>();
        capsuleCollider = GetComponentInParent<CapsuleCollider>();
        weaponCollider = GameObject.Find("Boss").GetComponentInChildren<BoxCollider>();
        triggerCollider = GameObject.Find("Boss").GetComponentInChildren<SphereCollider>();
        bossModel = GameObject.FindGameObjectWithTag("BossModel");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerWeapon" && bossHealth >= 1)
        {
            bossHealth--;
            anim.SetTrigger("IsHit");
            print("Boss Health " + bossHealth);
            if (bossHealth < 6)
            {
                bossModel.GetComponent<SkinnedMeshRenderer>().material = hurtBossMaterial;
            }
        }
        else
        {
            BossDead();
        }
    }
    void BossDead()
    {
        bossDead = true;
        anim.SetTrigger("IsDead");
        bossController.bossAwake = false;
        //capsuleCollider.enabled = false;
        weaponCollider.enabled = false;
        triggerCollider.enabled = false;

        StartCoroutine(PlayVideo());
        //Musica para cuando muera el boss
        //if (newTrack02 != null)
        //{
        //    audioManager.ChangeMusic(newTrack02);
        //}

    }

    IEnumerator PlayVideo()
    {
        yield return new WaitForSeconds(5);
        videoPlayer.SetActive(true);
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene(0);
    }
}
