using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{
    private SkeletonControl skeletonControl;
    private bool inRange;
    private Animator anim;

    private void Awake()
    {
        skeletonControl = GetComponentInParent<SkeletonControl>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            skeletonControl.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            // skeletonControl.triggerArea.SetActive(true);
            skeletonControl.inRange = false;
            skeletonControl.SelectTarget();
            
        }
    }
}
