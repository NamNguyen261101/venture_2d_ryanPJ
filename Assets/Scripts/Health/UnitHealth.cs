using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth 
{
    // Fields
    private float currentHealth;
    private float currentMaxHealth;

    // Properties
    public float Health
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
        }
    }

    public float MaxHealth
    {
        get
        {
            return currentMaxHealth;
        }
        set
        {
            currentMaxHealth = value;
        }
    }

    // Constructor
    public UnitHealth(float health, float maxHealth)
    {
        this.currentHealth = health;
        this.currentMaxHealth = maxHealth;
    }

    // Methods
    public void DamageUnit(float dmgAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= dmgAmount;
        }
    }

    public void HealUnit(float healAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth += healAmount;
        }

        if (currentHealth > currentMaxHealth)
        {
            currentHealth = currentMaxHealth;
        }
    }
}
