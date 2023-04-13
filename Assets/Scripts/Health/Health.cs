using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    private float currentHealth;
    private Animator anim;
    private bool dead;
    private bool isHurting;
    [Header("iframes")]
    [SerializeField] private float iframeDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;


    public float CurrentHealth
    {
        get { return currentHealth; }
        private set { currentHealth = value; }
    }

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

   
    public void TakeDamage(float damage)
    {
        currentHealth -= Mathf.Clamp(currentHealth - damage, damage, startingHealth);

        if (currentHealth > 0)
        {
            Debug.Log("Still Hurt");
            // player hurt
            anim.SetTrigger("hurt");
           
        } else if (currentHealth < 0) 
        {
            if(!dead)
            {
                // player dead
                //Debug.Log("Dead");
                anim.SetTrigger("die");
                // Player
                if (GetComponent<PlayerController>() != null)
                {
                    GetComponent<PlayerController>().enabled = false;
                }
                // Enemy
                // Monster
                if (GetComponent<EnemyPatrol>() != null)
                {
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                    
                }
                if (GetComponent<MonsterControl>() != null)
                {
                    GetComponent<MonsterControl>().enabled = false;
                }
                
            }  
        }
    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }




    /*private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(3,9, true);

        // invunerabity duration
        for (int i = 0;i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds( iframeDuration / (numberOfFlashes *1));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds( iframeDuration / (numberOfFlashes * 1));
        }

        Physics2D.IgnoreLayerCollision(3, 9, false);
    } */
}
