using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeLandSplatter : MonoBehaviour
{
    [SerializeField] private float disableColliderTime = 0.2f;

    private SpriteFade fade;
    private CapsuleCollider2D capCollider;

    private void Awake() {
        fade = GetComponent<SpriteFade>();
        capCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start() {
        StartCoroutine(fade.FadeRoutine());

        // could use a coroutine, but using Invoke as an alternate method
        Invoke("DisableCollider", disableColliderTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        playerHealth?.TakeDamage(1, transform);
    }

    private void DisableCollider() {
        capCollider.enabled = false;
    }
}
