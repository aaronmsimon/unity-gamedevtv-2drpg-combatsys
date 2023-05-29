using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;

    public void DropItems() {
        Instantiate(coinPrefab, transform.position, Quaternion.identity);
    }
}
