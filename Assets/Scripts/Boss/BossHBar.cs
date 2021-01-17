using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHBar : MonoBehaviour
{
    [SerializeField] private float fillAmount;
    [SerializeField] private Image fill;
    [SerializeField] private float lerpSpeed;

    public GameObject headTrigger;
    private BossHealth health;
    public int currentHealth;
    public float maxHealth;

    private void Start()
    {
        health = headTrigger.GetComponent<BossHealth>();
    }
    void Update()
    {
        HandleBar();
    }
    void HandleBar()
    {
        fill.fillAmount = Mathf.Lerp(fill.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);

        if(currentHealth >= 0)
        {
            currentHealth = health.bossHealth;
            fillAmount = (currentHealth / maxHealth);
        }
    }
}
