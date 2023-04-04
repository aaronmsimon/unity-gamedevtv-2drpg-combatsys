using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerControls playerControls;
    private Animator swordAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

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
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update() {
        MouseFollowWithOffset();
    }

    private void Attack() {
        swordAnimator.SetTrigger("Attack");
    }

    private void MouseFollowWithOffset() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        activeWeapon.transform.rotation = Quaternion.Euler(0, mousePos.x < playerScreenPoint.x ? 180f : 0f, angle);
    }
}
