using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoblinController : MonoBehaviour
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

    // Ray cast
     private RaycastHit2D hit;
     private Transform target;
    [SerializeField] private Transform rayCast;
    [SerializeField] private LayerMask raycastMask;

    [SerializeField] private float rayCastLength;

    private float distance; // store the distance b/w enemy and player
    private bool attackMode;

    private bool cooling; // Check if Enemy is cooling after attack
    private float intTimer;
    [SerializeField] private float attackDistance; // Min distance for attack
    [SerializeField] private float timer; // Time cooldown for attack
    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;
    public bool inRange; // Check if player is in range
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

    private void Update()
    {
        if (!InsideofLimits() && !inRange && !aliveAnimator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            SelectTarget();
        }

        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLength, raycastMask);
            RayCastDebugger();
        }
        // When player is detected
        if (hit.collider != null)
        {
            EnemyLogic();
        }
        else if (hit.collider == null)
        {
            inRange = false;
        }

        if (inRange == false)
        {
            // anim.SetBool("moving", false);
            StopAttack();
            //EnemyLogic();
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

        if (newDirection == LEFT)
        {
            newScale.x = -baseScale.x;
        }
        else if (newDirection == RIGHT)
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


        private void OnTriggerEnter2D(Collider2D trig)
        {
            if (trig.gameObject.tag == "Player")
            {
                target = trig.transform;
                inRange = true;
                Flip();
            }
        }


    private void RayCastDebugger() 
    {
          if (distance > attackDistance) 
           {
               Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
           } else if (distance < attackDistance) {
               Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
          }
           
     }

    // Enemy Loic
    private void EnemyLogic()
    {
        // distance btween enemy and player
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            aliveAnimator.SetBool("isAttack", false);
        }
    }

    // Attack 
    private void Attack()
    {
        timer = intTimer; // Reset timer when Player enter attack range
        attackMode = true; // To check if enemy can still attack or not

        aliveAnimator.SetBool("moving", false);
       // aliveAnimator.SetBool("isAttack", true);
    }
    // Cooldown attack
    private void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }
    // Stop Attack
    private void StopAttack()
    {
        cooling = false;
        attackMode = false;
        // aliveAnimator.SetBool("isAttack", false);

    }

    // Trigger to Cooling animation attack 
    private void TriggerColling()
    {
        cooling = true;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position); // cal distance enemy from left boundary
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    // Flip
    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }

    // Limits Range 
    private bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x; // if match will be true
    }
}
