using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject projectileVFX;

    private WeaponInfo weaponInfo;
    private Vector3 startPos;

    private void Update() {
        MoveProjectile();
        DetectFireDistance();
    }

    private void Start() {
        startPos = transform.position;
    }

    public void UpdateWeaponInfo(WeaponInfo weaponInfo) {
        this.weaponInfo = weaponInfo;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        Indestructable indestructable = other.GetComponent<Indestructable>();

        if (!other.isTrigger && (enemyHealth || indestructable)) {
            Instantiate(projectileVFX, transform.position, transform.rotation);
        }
    }

    private void MoveProjectile() {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void DetectFireDistance() {
        if (Vector3.Distance(transform.position, startPos) > weaponInfo.weaponRange) {
            Destroy(gameObject);
        }
    }
}
