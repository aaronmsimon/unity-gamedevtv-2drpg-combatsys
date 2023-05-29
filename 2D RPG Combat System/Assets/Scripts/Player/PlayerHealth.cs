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

        if (enemy) {
            // take damage
            TakeDamage(1, other.gameObject.transform);
        }
    }

    public void TakeDamage(int damageAmount, Transform damageSource) {
        if (!canTakeDamage) { return; }
        
        // knock player back
        knockback.GetKnockedBack(damageSource, knockbackThrust);
        // screen shake
        ScreenShakeManager.Instance.ScreenShake();
        // flash white material
        StartCoroutine(flash.FlashRoutine());
        
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(TakeDamageRecoveryRoutine());
    }

    private IEnumerator TakeDamageRecoveryRoutine() {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
}
