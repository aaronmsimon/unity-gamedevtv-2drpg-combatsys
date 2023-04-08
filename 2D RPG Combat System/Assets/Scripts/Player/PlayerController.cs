using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } }

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashMultiplier = 4f;
    [SerializeField] private float dashTime = .2f;
    [SerializeField] private float dashCooldownTime = .25f;
    [SerializeField] private TrailRenderer trailRenderer;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator playerAnimator;
    private SpriteRenderer sr;
    private bool facingLeft = false;
    private bool isDashing = false;
    private float startingMoveSpeed;

    private void Awake() {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        playerControls.Movement.Dash.performed += _ => Dash();
        startingMoveSpeed = moveSpeed;
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void Update() {
        PlayerInput();
        AdjustPlayerFacingDirection();
    }

    private void FixedUpdate() {
        Move();
    }

    private void PlayerInput() {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        playerAnimator.SetFloat("moveSpeed", Mathf.Abs(movement.magnitude));
    }

    private void Move() {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        sr.flipX = mousePos.x < playerScreenPoint.x;
        facingLeft = mousePos.x < playerScreenPoint.x;
    }

    private void Dash() {
        if (!isDashing) {
            StartCoroutine(DashRoutine());
            isDashing = true;
        }
    }

    private IEnumerator DashRoutine() {
        moveSpeed *= dashMultiplier;
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCooldownTime);
        isDashing = false;
    }
}
