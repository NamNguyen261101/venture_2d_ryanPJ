using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    [SerializeField] private GameObject chestClose, chestOpen;
    [SerializeField] private GameObject coinPrefab;

    private void Start()
    {
        chestClose.SetActive(true);
        chestOpen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        chestClose.SetActive(false);
        // Destroy(chestClose);
        chestOpen.SetActive(true);

    }
}
