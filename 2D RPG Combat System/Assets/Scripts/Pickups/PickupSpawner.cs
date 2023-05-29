using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab, healthPrefab, staminaPrefab;

    public void DropItems() {
        int randomNum = Random.Range(1, 5);

        switch (randomNum) {
            case 1:
                Instantiate(healthPrefab, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(staminaPrefab, transform.position, Quaternion.identity);
                break;
            case 3:
                int coinCount = Random.Range(1, 4);
                for (int i = 0; i < coinCount; i++)
                {
                    Instantiate(coinPrefab, transform.position, Quaternion.identity);
                }
                break;
        }
    }
}
