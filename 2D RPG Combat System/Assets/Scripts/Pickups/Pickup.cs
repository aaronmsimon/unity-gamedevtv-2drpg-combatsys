using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float pickupDistance = 5f;
    [SerializeField] private float pickupAcceleration = 0.5f;
    [SerializeField] private float moveSpeed = 2;

    private Vector3 moveDir;

    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        ResetMovement();
    }

    private void Update() {
        Vector3 playerPos = PlayerController.Instance.transform.position;

        if (Vector3.Distance(playerPos, transform.position) < pickupDistance) {
            moveDir = (playerPos - transform.position).normalized;
            moveSpeed += pickupAcceleration;
        } else {
            ResetMovement();
        }
    }

    private void FixedUpdate() {
        rb.velocity = moveDir * moveSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>()) {
            Destroy(gameObject);
        }
    }

    private void ResetMovement() {
        moveDir = Vector3.zero;
        moveSpeed = 0;
    }
}
