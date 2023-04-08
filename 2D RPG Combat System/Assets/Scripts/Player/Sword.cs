using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private float cooldownSeconds;

    private PlayerControls playerControls;
    private Animator swordAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;
    private GameObject slashAnim;
    private bool attackButtonDown, isAttacking = false;

    private void Awake() {
        playerControls = new PlayerControls();
        swordAnimator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void Start() {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update() {
        MouseFollowWithOffset();
        Attack();
    }

    private void StartAttacking() {
        attackButtonDown = true;
    }

    private void StopAttacking() {
        attackButtonDown = false;
    }

    private void Attack() {
        if (attackButtonDown && !isAttacking) {
            isAttacking = true;
            swordAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);

            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;

            StartCoroutine(AttackCooldownRoutine());
        }
    }

    public void DoneAttackingAnimEvent() {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimEvent() {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180f, 0, 0);

        slashAnim.GetComponent<SpriteRenderer>().flipX = playerController.FacingLeft;
    }

    public void SwingDownFlipAnimEvent() {
        slashAnim.GetComponent<SpriteRenderer>().flipX = playerController.FacingLeft;
    }

    private void MouseFollowWithOffset() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        activeWeapon.transform.rotation = Quaternion.Euler(0, mousePos.x < playerScreenPoint.x ? 180f : 0f, angle);
        weaponCollider.transform.rotation = Quaternion.Euler(0, mousePos.x < playerScreenPoint.x ? 180f : 0f, 0);
    }

    private IEnumerator AttackCooldownRoutine() {
        yield return new WaitForSeconds(cooldownSeconds);
        isAttacking = false;
    }
}
