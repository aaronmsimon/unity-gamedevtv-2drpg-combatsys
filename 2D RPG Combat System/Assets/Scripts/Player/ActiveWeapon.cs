using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }

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

    public void NewWeapon(MonoBehaviour newWeapon) {
        CurrentActiveWeapon = newWeapon;
    }

    public void WeaponNull() {
        CurrentActiveWeapon = null;
    }

    private void Attack() {
        if (attackButtonDown && !isAttacking) {
            isAttacking = true;
            (CurrentActiveWeapon as IWeapon).Attack();
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
