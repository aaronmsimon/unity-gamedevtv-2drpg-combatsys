using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private float cooldownSeconds;

    private Animator swordAnimator;
    private GameObject slashAnim;
    private Transform weaponCollider;
    private Transform slashAnimSpawnPoint;

    private void Awake() {
        swordAnimator = GetComponent<Animator>();
    }

    private void Start() {
        // two methods:
        // 1) use a getter method (but can't use #2 here because it gets toggled as inactive)
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        // 2) use a string referencer (cringe)
        slashAnimSpawnPoint = GameObject.Find("SlashAnimSpawnPoint").transform;
    }

    private void Update() {
        MouseFollowWithOffset();
    }

    public void Attack() {
        // isAttacking = true;
        swordAnimator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);

        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;

        StartCoroutine(AttackCooldownRoutine());
    }

    public void DoneAttackingAnimEvent() {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimEvent() {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180f, 0, 0);

        slashAnim.GetComponent<SpriteRenderer>().flipX = PlayerController.Instance.FacingLeft;
    }

    public void SwingDownFlipAnimEvent() {
        slashAnim.GetComponent<SpriteRenderer>().flipX = PlayerController.Instance.FacingLeft;
    }

    private void MouseFollowWithOffset() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, mousePos.x < playerScreenPoint.x ? 180f : 0f, angle);
        weaponCollider.transform.rotation = Quaternion.Euler(0, mousePos.x < playerScreenPoint.x ? 180f : 0f, 0);
    }

    private IEnumerator AttackCooldownRoutine() {
        yield return new WaitForSeconds(cooldownSeconds);
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}
