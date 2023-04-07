using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth;
    // remove this
    [SerializeField] private Transform player;

    private int currentHealth;
    private Knockback knockback;

    private void Awake() {
        knockback = GetComponent<Knockback>();
    }

    private void Start() {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        knockback.GetKnockedBack(player, 15f);
        DetectDeath();
    }

    private void DetectDeath() {
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}
