using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoblinController : MonoBehaviour
{
    private enum GoblinState
    {
        Moving,
        Knockback,
        Dead
    }
    // AI state
    private GoblinState currentState;

    [SerializeField]
    private float
            groundCheckDistance,
            wallCheckDistance,
            movementSpeed,
            maxHealth,
            knockbackDuration;
    [SerializeField]
    private Transform
            groundCheck,
            wallCheck;
    [SerializeField]
    private LayerMask
            whatIsGround; // same for player
    [SerializeField]
    private Vector2
            knockbackSpeed;

    private int 
            facingDirection,
            damageDirection;

    private float 
            currentHealth,
            knockbackStartTime;

    private Vector2 movement;

    private bool 
            groundDetected,
            wallDetected;

    private GameObject alive; // var to contain all obj -> by finding
    private Rigidbody2D aliveRb;
    private Animator aliveAnimator;

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();
        aliveAnimator = alive.GetComponent<Animator>();

        facingDirection = 1; // Set default facing
    }
    private void Update()
    {
        switch (currentState)
        {
            case GoblinState.Moving:
                UpdateMovingState();
                break;
            case GoblinState.Knockback:
                UpdateKnockbackState();
                break;
            case GoblinState.Dead:
                UpdateDeadState();
                break;
        }
    }
    // Moving State
    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if (!groundDetected || wallDetected)
        {
            // Flip enemy
            Flip();
        } else
        {
            movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y);
            aliveRb.velocity = movement;
        }
    }

    private void ExitMovingState()
    {

    }
    // Knockback State
    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRb.velocity = movement;
        aliveAnimator.SetBool("isKnockback", true);

    }

    private void UpdateKnockbackState()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration)
        {
            SwitchState(GoblinState.Moving);
        }
    }

    private void ExitKnockbackState()
    {
        aliveAnimator.SetBool("isKnockback", false);
    }

    // Dead State
    private void EnterDeadState()
    {
        // Spawn chunks and blood
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    //

    // Switch state
    private void SwitchState(GoblinState state)
    {
        switch (currentState)
        {
            case GoblinState.Moving:
                ExitMovingState();
                break;
            case GoblinState.Knockback:
                ExitKnockbackState();
                break;
            case GoblinState.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case GoblinState.Moving:
                EnterMovingState();
                break;
            case GoblinState.Knockback:
                EnterKnockbackState();
                break;
            case GoblinState.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    // Flip Direction
    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);

    }

    // Damage Function
    private void Damage(float[] attackDetails) // send damg + x location of the player doing the attack 
    {
        currentHealth -= attackDetails[0]; // always want send damg in first index

        // x position = second index of array
        if (attackDetails[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        } else
        {
            damageDirection = 1;
        }

        // Hit particle
        if (currentHealth > 0.0f)
        {
            SwitchState(GoblinState.Knockback);
        } else if (currentHealth < 0.0f)
        {
            SwitchState(GoblinState.Dead);
        }
    }

    // Draw Gizmo
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}
