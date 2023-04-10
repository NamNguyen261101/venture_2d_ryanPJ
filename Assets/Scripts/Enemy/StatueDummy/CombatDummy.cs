using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummy : MonoBehaviour
{
    [SerializeField]
    private float maxHealth, knockbackSpeedX, knockbackSpeedY, knockbackDuration, knockbackDeathSpeedX, knockbackDeathSpeedY, deadTorque; // deadTorque use only for further when breaks
    [SerializeField]
    private bool applyKnockBack;
    [SerializeField]
    private GameObject hitParticle;

    private float currentHealth, knockbackStart;

    private int playerFacingDirection;

    private bool playerOnLeft, knockback;

    private PlayerController playerController;
    private GameObject aliveGameObject, brokenTopGameObject, brokenBotGameObject;
    private Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBot;
    private Animator aliveAnimator;

    private void Start()
    {
        currentHealth = maxHealth;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>(); // return the first game object name Player

        // references 
        aliveGameObject = transform.Find("Alive").gameObject;
        brokenTopGameObject = transform.Find("BrokenTop").gameObject;
        brokenBotGameObject = transform.Find("BrokenBottom").gameObject;

        aliveAnimator = aliveGameObject.GetComponent<Animator>();
        rbAlive = aliveGameObject.GetComponent<Rigidbody2D>();

        rbBrokenTop = brokenTopGameObject.GetComponent<Rigidbody2D>();
        rbBrokenBot = brokenBotGameObject.GetComponent<Rigidbody2D>();

        aliveGameObject.SetActive(true);
        brokenTopGameObject.SetActive(false);
        brokenBotGameObject.SetActive(false);
    }
    private void Update()
    {
        CheckKnockback();
    }
    private void Damage(float amountDamage)
    {
        currentHealth -= amountDamage;
        playerFacingDirection = playerController.GetFacingDirection(); // let animator know what side the players on  

        Instantiate(hitParticle, aliveAnimator.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if (playerFacingDirection == 1)
        {
            playerOnLeft = true;
        } else
        {
            playerOnLeft = false;
        }

        aliveAnimator.SetBool("playerOfLeft", playerOnLeft);
        aliveAnimator.SetTrigger("getDamage");

        if (applyKnockBack && currentHealth > 0.0f)
        {
            // Knockback
            Knockback();
        }

        if (currentHealth < 0.0f)
        {
            // die
            Die();
        }
    }

    private void Knockback()
    {
        knockback = true;
        knockbackStart = Time.time;
        rbAlive.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
    }

    private void CheckKnockback()
    {
        if (Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }

    private void Die()
    {
        aliveGameObject.SetActive(false);
        brokenTopGameObject.SetActive(true);
        brokenBotGameObject.SetActive(true);

        brokenTopGameObject.transform.position = aliveGameObject.transform.position;
        brokenBotGameObject.transform.position = aliveGameObject.transform.position;

        rbBrokenBot.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
        rbBrokenTop.velocity = new Vector2(knockbackDeathSpeedX * playerFacingDirection, knockbackDeathSpeedY);
        rbBrokenTop.AddTorque(deadTorque * -playerFacingDirection);
    }
}
