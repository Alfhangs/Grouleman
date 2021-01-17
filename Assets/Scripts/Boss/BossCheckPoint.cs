using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheckPoint : MonoBehaviour
{
    public BoxCollider boxCollider;
    private BossController bossController;
    private CharacterMovement characterController;
    private Animator playerAnimator;
    private SmoothFollow smoothFollow;

    public AudioClip newTrack;
    private AudioManager audioManager;

    private void Start()
    {
        bossController = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        smoothFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothFollow>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            boxCollider.isTrigger = false;
            bossController.bossAwake = true;
            characterController.enabled = false;
            playerAnimator.Play("Player_Idle");
            smoothFollow.bossCameraActive = true;

            if(newTrack != null)
            {
                audioManager.ChangeMusic(newTrack);
            }
        }
    }
}
