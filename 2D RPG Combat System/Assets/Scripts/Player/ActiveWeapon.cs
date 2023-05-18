using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    [SerializeField] private MonoBehaviour currentActiveWeapon;

    private bool attackButtonDown, isAttacking = false;

    private PlayerControls playerControls;

    protected override void Awake() {
        base.Awake();

        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void Start() {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update() {
        Attack();
    }

    private void Attack() {
        if (attackButtonDown && !isAttacking) {
            isAttacking = true;
            (currentActiveWeapon as IWeapon).Attack();
        }
    }

    private void StartAttacking() {
        attackButtonDown = true;
    }

    private void StopAttacking() {
        attackButtonDown = false;
    }

    public void ToggleIsAttacking(bool value) {
        isAttacking = value;
    }
}
