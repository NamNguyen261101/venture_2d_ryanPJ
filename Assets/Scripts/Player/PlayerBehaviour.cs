using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private float maxHealth = 3;
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

    }

    public void PlayerTakeDmg(float dmg)
    {
        // currentHealth = Mathf.Clamp(currentHealth - dmg, dmg, maxHealth);
        currentHealth -= dmg;
        if (currentHealth > 0 )
        {
            Debug.Log("Still Hurt");
            // player hurt
            anim.SetTrigger("hurt");
            healthBar.SetHealth(currentHealth);
        }
        else if (currentHealth <= 0)
        {

                Debug.Log("Die");
                anim.SetTrigger("die");
                // Player
                GetComponent<PlayerController>().enabled = false;
        }
  
    }

    public void PlayerHeal(float heal)
    {
        currentHealth += heal;
        healthBar.SetHealth(currentHealth);
    }


}
