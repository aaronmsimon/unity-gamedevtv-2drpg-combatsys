using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject grapeProjectilePrefab;

    private Animator grapeAnimator;
    private SpriteRenderer sr;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake() {
        grapeAnimator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void Attack() {
        grapeAnimator.SetTrigger(ATTACK_HASH);

        sr.flipX = transform.position.x - PlayerController.Instance.transform.position.x > 0;
    }

    public void SpawnProjectileAnimEvent() {
        Instantiate(grapeProjectilePrefab, transform.position, Quaternion.identity);
    }
}
