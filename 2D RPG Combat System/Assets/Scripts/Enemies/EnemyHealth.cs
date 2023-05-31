using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    [SerializeField] private GameObject deathVFX;
    // remove this
    [SerializeField] private Transform player;
    [SerializeField] private float knockBackThrust = 15f;

    private int currentHealth;
    private Knockback knockback;
    private Flash flash;

    private void Awake() {
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    private void Start() {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        knockback.GetKnockedBack(player, knockBackThrust);
        StartCoroutine(CheckForDeathRoutine());
    }

    private IEnumerator CheckForDeathRoutine() {
        yield return StartCoroutine(flash.FlashRoutine());
        CheckForDeath();
    }

    private void CheckForDeath() {
        if (currentHealth <= 0) {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
            GetComponent<PickupSpawner>().DropItems();
            Destroy(gameObject);
        }
    }
}
