using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Knockback kb;
    //private Animator enemyAnimator;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        kb = GetComponent<Knockback>();
        //enemyAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        if (!kb.GettingKnockedBack)
            Move();
    }

    private void Move() {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPos) {
        movement = targetPos;
    }
}
