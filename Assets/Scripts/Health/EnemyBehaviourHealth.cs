using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourHealth : MonoBehaviour
{
    private float maxHealth = 10;
    private float currentHealth;
    public HealthBarAll healthBar;

    private Animator anim;
    private bool isHurting;
    private float timerToDie = 1f;
    private void Start()
    {
        
       anim = GetComponent<Animator>();

        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeHit(2);
        }
    }

    public void TakeHit(float damaged)
    {
        currentHealth -= damaged;
        if (currentHealth > 0 )
        {
            anim.SetTrigger("isHurting");
            healthBar.SetHealth(currentHealth);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        anim.SetTrigger("Die");
        Destroy(gameObject, timerToDie);
    }
}
