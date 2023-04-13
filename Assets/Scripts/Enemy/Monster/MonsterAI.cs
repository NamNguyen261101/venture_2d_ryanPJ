using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    [SerializeField] private GameObject face;
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private Transform castPoint;

    [SerializeField] private Transform player;
    [SerializeField] private float argoRange, moveSpeed;

    private bool isFacingLeft;
    private bool isArgo = false;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer < argoRange)
        {
            
            //argo enemy
            ChasePlayer();
        } else 
        {

            StopChasingPlayer();
        }
       
    }

    // How far can see
    private bool CanSeePlayer(float distance)
    {
        bool value = false;
        float castDist = distance;

        if (isFacingLeft)
        {
            castDist = -distance;
        }

        Vector2 endPos = castPoint.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {

                value = true;
            } else
            {
                value = false;
            }
            Debug.DrawLine(castPoint.position, hit.point, Color.red);
        } else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.red);
        }

        return value;
    }

    private void StopChasingPlayer()
    {
        
        rb.velocity = new Vector2(0, 0);
        anim.SetBool("isMoving", false);
    }

    private void ChasePlayer()
    {
        anim.SetBool("isMoving", true);
        if (transform.position.x < player.position.x) // to the left 
        {
            // enemy is to the left side of player,  so move right
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.localScale = new Vector2(6, 6);
            isFacingLeft = false;
        }
        else // to the right
        {
            // enemy is to the right side of player,  so move left 
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.localScale = new Vector2(-6, 6);
            isFacingLeft = true;
        }
    }


}
