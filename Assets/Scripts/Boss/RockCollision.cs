using UnityEngine;

public class RockCollision : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth playerHealth;

    private void Start()
    {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
    }
    private void OnParticleCollision(GameObject other)
    {
        if(other == player)
        {
            playerHealth.TakeHit();
            print("Player hit");
        }
    }
}
