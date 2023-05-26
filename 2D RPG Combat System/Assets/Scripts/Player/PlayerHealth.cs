using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockbackThrust = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private int currentHealth;
    private bool canTakeDamage = true;

    private Knockback knockback;
    private Flash flash;

    private void Awake() {
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    private void Start() {
        currentHealth = maxHealth;
    }

    private void OnCollisionStay2D(Collision2D other) {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy && canTakeDamage) {
            // take damage
            TakeDamage(1);
            // knock player back
            knockback.GetKnockedBack(other.gameObject.transform, knockbackThrust);
            // flash white material
            StartCoroutine(flash.FlashRoutine());
        }
    }

    private void TakeDamage(int damageAmount) {
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(TakeDamageRecoveryRoutine());
    }

    private IEnumerator TakeDamageRecoveryRoutine() {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
}
