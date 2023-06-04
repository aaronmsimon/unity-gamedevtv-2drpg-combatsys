using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public bool FacingLeft { get { return facingLeft; } }

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashMultiplier = 4f;
    [SerializeField] private float dashTime = .2f;
    [SerializeField] private float dashCooldownTime = .25f;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Transform weaponCollider;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator playerAnimator;
    private SpriteRenderer sr;
    private Knockback kb;
    private bool facingLeft = false;
    private bool isDashing = false;
    private float startingMoveSpeed;

    protected override void Awake() {
        base.Awake();
        
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        kb = GetComponent<Knockback>();
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

    public Transform GetWeaponCollider() {
        return weaponCollider;
    }

    private void PlayerInput() {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        playerAnimator.SetFloat("moveSpeed", Mathf.Abs(movement.magnitude));
    }

    private void Move() {
        if (kb.GettingKnockedBack) { return; }
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        sr.flipX = mousePos.x < playerScreenPoint.x;
        facingLeft = mousePos.x < playerScreenPoint.x;
    }

    private void Dash() {
        if (!isDashing && Stamina.Instance.CurrentStamina > 0) {
            StartCoroutine(DashRoutine());
            isDashing = true;
            Stamina.Instance.UseStamina();
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
