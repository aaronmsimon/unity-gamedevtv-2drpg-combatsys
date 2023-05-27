using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Knockback kb;
    private SpriteRenderer sr;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        kb = GetComponent<Knockback>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        if (kb.GettingKnockedBack) { return; }
        
        Move();

        sr.flipX = movement.x < 0;
    }

    private void Move() {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPos) {
        movement = targetPos;
    }

    public void StopMoving() {
        movement = Vector3.zero;
    }
}
