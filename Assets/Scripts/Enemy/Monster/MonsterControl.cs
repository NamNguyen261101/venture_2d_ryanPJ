using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    // Damage
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float damage;
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] private BoxCollider2D boxCollider;
    private LayerMask playerLayer;
    
    private Animator anim;
    private PlayerBehaviour playerHealth;

    private MonsterMoving monsterMoving;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        monsterMoving = GetComponentInParent<MonsterMoving>();
    }

    public void Update()
    {
        cooldownTimer += Time.deltaTime;

        // Attack when player in sight
        if (PlayerInsight())
        {
            monsterMoving.Active= false;
            if (cooldownTimer >= attackCooldown)
            {
                // attack
                cooldownTimer = 0;
                anim.SetTrigger("attack");
            }
        }
        else
        {
            monsterMoving.Active= true;
        }

       /* if (monsterMoving != null)
        {
            monsterMoving.enabled = !PlayerInsight(); // check if not insight to change direction
        }*/

    }
    // Insight view
    public bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                                             new Vector3(boxCollider.bounds.size.x * range,boxCollider.bounds.size.y, boxCollider.bounds.size.z), 
                                             0 ,Vector2.left, 0, playerLayer);
        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<PlayerBehaviour>();
        }


        return hit.collider != null;
    }

    private void DamagePlayer()
    {
        // If player still in range damage him
        if (PlayerInsight())
        {
            playerHealth.PlayerTakeDmg(damage);
        }
    }

    // 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
