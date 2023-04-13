using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterColider : MonoBehaviour
{
    private float getDamageFromPlayer = 1;
    /*private void OnCollisionEnter2D (Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("-damage");
            collision.GetComponent<Health>().TakeDamage(getDamageFromPlayer); ;
        }
    }*/

    private void OnCollisionEnter2D (Collision2D collision)
    {
        var enemy = collision.collider.GetComponent<EnemyBehaviourHealth>();
        
        if (enemy)
        {
            enemy.TakeHit(getDamageFromPlayer);
        }

        /*Destroy(enemy);*/
    }


}
