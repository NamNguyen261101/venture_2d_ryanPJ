using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHp = 100;
    private float hp;

    private float MaxHp => maxHp;
    public float Hp
    {
        get { return hp; }
        private set
        {
            var isDamage = value < hp;
            hp = Mathf.Clamp(value, 0, maxHp);

            if (isDamage)
            {
                Damaged?.Invoke(hp);
            } else
            {
                Healed?.Invoke(hp);
            }
            if (hp <= 0 )
            {
                Die?.Invoke();
            }
        }
    }
    public UnityEvent<float> Healed;
    public UnityEvent<float> Damaged;
    public UnityEvent Die;

    private void Awake()
    {
        hp = maxHp;
    }

    public void Damage(float damage)
    {
        hp -= damage;
    }
    

    public void Heal(float amount) 
    {
        Hp += amount;
    }

    public void HealFull()
    {
        Hp = maxHp;
    }

    public void Kill()
    {
        Hp = 0;
    }

    public void Adjust(float value)
    {
        Hp = value;
    }
}
