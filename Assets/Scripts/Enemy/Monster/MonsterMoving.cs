using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoving : MonoBehaviour
{
    [SerializeField]
    private Transform
           castPos;

    [SerializeField]
    private float
            moveSpeed = 3f,
            baseCastPosDistance;

    private string
            facingDirection;

    private Rigidbody2D
            aliveRb;
    private Animator
            anim;
    private Vector3
            baseScale;
    const string RIGHT = "right";
    const string LEFT = "left";
    public bool Active{ get; set; }

    [SerializeField] private float idleDuration; // how much time when he reach to the edge
    private float idleTimer;
    void Start()
    {
        facingDirection = RIGHT;
        baseScale = transform.localScale;

        aliveRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


    }


    public void FixedUpdate()
    {
        if (!Active)
            return;

        GoblinApplyMovement();

        if (IsHittingWall() || IsNearEdge())
        {
            // Debug.Log("touch");
            if (facingDirection == LEFT)
            {
                ChangingDirection(RIGHT);
            }
            else if (facingDirection == RIGHT)
            {
                ChangingDirection(LEFT);
            }
        }
    }

    // Moving 
    private void GoblinApplyMovement()
    {
        anim.SetBool("isMoving", true);
        float velocityX = moveSpeed;
        idleTimer = 0;
        if (facingDirection == LEFT) // left side
        {
            velocityX = -moveSpeed;
        }
        aliveRb.velocity = new Vector2(velocityX, aliveRb.velocity.y);
    }

    // Flip
    private void ChangingDirection(string newDirection)
    {
        anim.SetBool("isMoving", false);
        idleTimer += Time.deltaTime;
        Vector3 newScale = baseScale;
        if (newDirection == LEFT)
        {
            newScale.x = -baseScale.x;
        }
        else if (newDirection == RIGHT)
        {
            newScale.x = baseScale.x;
        }
        transform.localScale = newScale;
        /*if (idleTimer> idleDuration)
        {
            facingDirection = newDirection;
        }*/
        facingDirection = newDirection;
    
    }

    private bool IsHittingWall()
    {
        bool isHit = false;

        float castDist;

        // Define cast distance left or right
        if (facingDirection == LEFT)
        {
            castDist = -baseCastPosDistance;
        }
        else
        {
            castDist = baseCastPosDistance;
        }
        // determine target based by cast distance
        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.red);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground"))) // 1 << LayerMask.NameToLayer
        {
            isHit = true;
        }
        else
        {
            isHit = false;
        }

        return isHit;
    }

    private bool IsNearEdge()
    {
        bool isHitEdge = true;

        float castDist = baseCastPosDistance;

        // determine target based by cast distance
        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.blue);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground"))) // 1 << LayerMask.NameToLayer
        {
            isHitEdge = false;
        }
        else
        {
            isHitEdge = true;
        }

        return isHitEdge;
    }
}
