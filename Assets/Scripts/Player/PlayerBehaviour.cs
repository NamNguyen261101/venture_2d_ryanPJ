using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private float maxHealth = 5;
    private float currentHealth;
    public HealthBarAll healthBar;
    private Animator anim;
    private bool dead;
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerTakeDmg(2);
        }
    }

    public void PlayerTakeDmg(float dmg)
    {
        // currentHealth = Mathf.Clamp(currentHealth - dmg, dmg, maxHealth); // 2 -> 3 // -1 , 3 , 5 
        currentHealth -= dmg;
        if (currentHealth > 0 )
        {
            Debug.Log("Still Hurt");
            // player hurt
            anim.SetTrigger("hurt");
            healthBar.SetHealth(currentHealth);
        }
        if (currentHealth < 0)
        {
            
            Dead();
        }
  
    }

    public void PlayerHeal(float heal)
    {
        currentHealth += heal;
        healthBar.SetHealth(currentHealth);
    }

    public void Dead()
    {
        Debug.Log("Die");
        anim.SetTrigger("die");
        // Player
        GetComponent<PlayerController>().enabled = false;
    }
}
