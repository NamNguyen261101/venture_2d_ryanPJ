using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinControl : MonoBehaviour
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
            aliveAnimator;
    private Vector3
            baseScale;
    const string RIGHT = "right";
    const string LEFT = "left";
    void Start()
    {
        facingDirection = RIGHT;
        baseScale = transform.localScale;

        aliveRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
        GoblinApplyMovement();

        if (IsHittingWall() || IsNearEdge())
        {
            // Debug.Log("touch");
            if (facingDirection == LEFT )
            {
                ChangingDirection(RIGHT); 
            } else if (facingDirection == RIGHT)
            {
                ChangingDirection(LEFT);
            }
        }
    }

    // Moving 
    private void GoblinApplyMovement()
    {
        float velocityX = moveSpeed;

        if (facingDirection == LEFT) // left side
        {
            velocityX = -moveSpeed;
        }
        aliveRb.velocity = new Vector2(velocityX, aliveRb.velocity.y);
    }

    // Flip
    private void ChangingDirection(string newDirection)
    {
        Vector3 newScale = baseScale;

        if (newDirection == LEFT )
        {
            newScale.x = -baseScale.x;
        } else if (newDirection == RIGHT)
        {
            newScale.x = baseScale.x;
        }

        transform.localScale = newScale;

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
        } else
        {
            castDist = baseCastPosDistance;
        }
        // determine target based by cast distance
        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.red);

        if (Physics2D.Linecast(castPos.position, targetPos,1 << LayerMask.NameToLayer("Ground"))) // 1 << LayerMask.NameToLayer
        {
            isHit = true;
        } else
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
