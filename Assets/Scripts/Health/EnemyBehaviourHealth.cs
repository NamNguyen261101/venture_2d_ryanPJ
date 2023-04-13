using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourHealth : MonoBehaviour
{
    [SerializeField] private float hitPoints;
    [SerializeField] private float maxHealth;
    [SerializeField] private HealthBarAll healthBar;

    public Animator anim;
    private bool isHurting;
    private float timerToDie = 1f;
    private void Start()
    {
        
        hitPoints = maxHealth;
        healthBar.SetHealth(hitPoints, maxHealth);
    }

    public void TakeHit(float damaged)
    {
        hitPoints -= damaged;
       if (hitPoints <= 0)
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
