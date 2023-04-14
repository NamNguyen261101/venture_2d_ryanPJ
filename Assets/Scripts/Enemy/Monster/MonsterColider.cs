using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterColider : MonoBehaviour
{
    [SerializeField] private float getDamageFromPlayer = 1;
    public EnemyBehaviourHealth health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           health.TakeHit(getDamageFromPlayer);
        }
    }


}
