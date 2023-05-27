using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject projectileVFX;
    [SerializeField] private bool isEnemyProjectile = false;
    [SerializeField] private float projectileRange = 10f;

    private Vector3 startPos;

    private void Update() {
        MoveProjectile();
        DetectFireDistance();
    }

    private void Start() {
        startPos = transform.position;
    }

    public void UpdateProjectileRange(float projectileRange) {
        this.projectileRange = projectileRange;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        Indestructable indestructable = other.GetComponent<Indestructable>();
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth && isEnemyProjectile) {
            // player take damage
            playerHealth.TakeDamage(1, transform);
        }

        if (!other.isTrigger && (enemyHealth || indestructable || playerHealth)) {
            Instantiate(projectileVFX, transform.position, transform.rotation);
        }
    }

    private void MoveProjectile() {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void DetectFireDistance() {
        if (Vector3.Distance(transform.position, startPos) > projectileRange) {
            Destroy(gameObject);
        }
    }
}
