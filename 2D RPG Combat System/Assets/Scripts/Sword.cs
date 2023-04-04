using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerControls playerControls;
    private Animator swordAnimator;

    private void Awake() {
        playerControls = new PlayerControls();
        swordAnimator = GetComponent<Animator>();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void Start() {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Attack() {
        swordAnimator.SetTrigger("Attack");
    }
}
