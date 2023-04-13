using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    // private Rigidbody2D rb;
    [SerializeField] private Transform enemy; 
    [SerializeField] private float moveSpeed;
    private Vector3 initScale;
    private bool movingLeft;

    [SerializeField] private Animator anim;
    [SerializeField] private float idleDuration; // how much time when he reach to the edge
    private float idleTimer;
    private void Awake()
    {
        initScale = enemy.localScale;
        
    }
    private void FixedUpdate()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                
                MoveInDirection(-1);
            } else 
            {
               /* Debug.Log("Touch 2");*/
                ChangeDirection();
            }
        } else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                /*Debug.Log("Touch 1");*/
                ChangeDirection();
            }
        }
        
    }
    private void OnDisable()
    {
        anim.SetBool("isMoving", false);
    }
    private void ChangeDirection()
    {
        anim.SetBool("isMoving", false);

        idleTimer += Time.deltaTime;
        if(idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
    }
    private void MoveInDirection(int direction)
    {
        idleTimer = 0;
        anim.SetBool("isMoving", true);
        // Face Direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);

        // Move in face direction
        enemy.position = new Vector3(enemy.position.x  + Time.deltaTime * moveSpeed * direction,
                                     enemy.position.y, enemy.position.z);
        // rb.velocity = new Vector2(moveSpeed, rb.velocity.y);


    }
}
