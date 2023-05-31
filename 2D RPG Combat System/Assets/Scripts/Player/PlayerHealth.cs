using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockbackThrust = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private int currentHealth;
    private bool canTakeDamage = true;
    private Slider healthSlider;

    private Knockback knockback;
    private Flash flash;

    protected override void Awake() {
        base.Awake();

        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    private void Start() {
        currentHealth = maxHealth;
        UpdateHealthSlider();
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

        UpdateHealthSlider();
        CheckPlayerDeath();
    }

    public void Heal() {
        if (currentHealth < maxHealth) {
            currentHealth += 1;
            UpdateHealthSlider();
        }
    }

    private IEnumerator TakeDamageRecoveryRoutine() {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private void UpdateHealthSlider() {
        if (healthSlider == null) {
            healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    private void CheckPlayerDeath() {
        if (currentHealth <= 0) {
            currentHealth = 0;
            Debug.Log("Player Death.");
        }
    }
}
