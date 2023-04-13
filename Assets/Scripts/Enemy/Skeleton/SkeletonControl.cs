using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonControl : MonoBehaviour
{
    private SkeletonControl instance;
    [SerializeField] private Transform rayCast;
    [SerializeField] private LayerMask raycastMask;

    [SerializeField] private float rayCastLength;
    [SerializeField] private float attackDistance; // Min distance for attack
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timer; // Time cooldown for attack
    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;
    [HideInInspector][SerializeField] public Transform target;
    [HideInInspector][SerializeField] public bool inRange; // Check if player is in range
    // [SerializeField] public GameObject hotZone;
    // [SerializeField] public GameObject triggerArea;

    private RaycastHit2D hit;
    // private Transform target;
    private Animator anim;
    private Rigidbody2D aliveRb;
    private float distance; // store the distance b/w enemy and player
    private bool attackMode;

    private bool cooling; // Check if Enemy is cooling after attack
    private float intTimer;

    // Move back and down
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    private Transform currentPoint;

    private SkeletonMovement abc;
    private void Start()
    {
        instance = this;
        anim = GetComponent<Animator>();
        aliveRb = GetComponent<Rigidbody2D>();
        // currentPoint = pointB.transform;
        intTimer = timer;
    }
    private void Awake()
    {
        SelectTarget();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {

            SelectTarget();
        }
        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, transform.right , rayCastLength, raycastMask);
            RayCastDebugger();
        }
        // When player is detected
        if (hit.collider != null )
        {
            EnemyLogic();
        } else if (hit.collider == null)
        {
            inRange = false;
        }

        if (inRange)
        {
            // anim.SetBool("moving", false);
            //StopAttack();
            EnemyLogic();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = collision.transform;
            inRange = true;
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
        } else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("isAttack", false);
        }
    }

    // Ray Cast
    private void RayCastDebugger()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
        } else if (attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
        }
    }
    // Move
    public void Move()
    {
        anim.SetBool("moving", true);
        float velocityX = moveSpeed;
        
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack")) // get from animator 
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            // aliveRb.velocity = new Vector2(velocityX, aliveRb.velocity.y);
        }

        
    }

    // Attack 
    private void Attack()
    {
        timer = intTimer; // Reset timer when Player enter attack range
        attackMode = true; // To check if enemy can still attack or not

        anim.SetBool("moving", false);
        anim.SetBool("isAttack", true);
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
        anim.SetBool("isAttack", false);
       
    }

    // Trigger to Cooling animation attack 
    private void TriggerColling()
    {
        cooling = true;
    }
    
    // Limits Range 
    private bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x; // if match will be true
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position); // cal distance enemy from left boundary
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        } else
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

   
}
