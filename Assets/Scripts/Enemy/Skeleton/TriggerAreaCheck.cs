using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    private SkeletonControl skeletonControl;

    private void Awake()
    {
        skeletonControl = GetComponentInParent<SkeletonControl>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            skeletonControl.target = collider.transform;
            skeletonControl.inRange = true;
            // skeletonControl.hotZone.SetActive(true);
            
        }
    }
}
