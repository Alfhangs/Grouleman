using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int startingLives;
    private int lifeCounter;
    private Text lifeText;

    private GameObject player;
    public GameObject gameOverScreen;
    public float waitAfterGameOver;

    private void Start()
    {
        lifeText = GetComponent<Text>();
        lifeCounter = startingLives;
        player = GameManager.instance.Player;
    }
    private void Update()
    {
        lifeText.text = "X " +lifeCounter;
        if(lifeCounter <= 0)
        {
            gameOverScreen.SetActive(true);
            player.gameObject.SetActive(false);
        }
        if (gameOverScreen.activeSelf)
        {
            waitAfterGameOver -= Time.deltaTime;
        }
        if (waitAfterGameOver < 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
    public void GiveLife()
    {
        lifeCounter++;
    }
    public void TakeLife()
    {
        lifeCounter--;
    }
}
