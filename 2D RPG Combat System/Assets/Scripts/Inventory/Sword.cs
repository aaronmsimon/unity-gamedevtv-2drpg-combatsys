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
        /*Vector3*/ mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        /*float*/ angle = Mathf.Atan2(Camera.main.ScreenToWorldPoint(mousePos).y, Camera.main.ScreenToWorldPoint(mousePos).x) * Mathf.Rad2Deg;

        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, mousePos.x < playerScreenPoint.x ? -180f : 0f, (mousePos.x < playerScreenPoint.x ? 180f - angle : angle));
        weaponCollider.transform.rotation = Quaternion.Euler(0, mousePos.x < playerScreenPoint.x ? 180f : 0f, 0);
    }

    private IEnumerator AttackCooldownRoutine() {
        yield return new WaitForSeconds(cooldownSeconds);
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }

    // everthing below here can be removed
    public Vector3 mousePos;
    public float angle;
    private void OnDrawGizmos() {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(mousePos), Color.red);

        angle = Mathf.Atan2(Camera.main.ScreenToWorldPoint(mousePos).y, Camera.main.ScreenToWorldPoint(mousePos).x) * Mathf.Rad2Deg;
        Debug.Log(angle);
        //if (angle > 90) angle += 90;
        float angleInRadians = Mathf.Deg2Rad * angle;
        float distance = 10f;

        // Calculate the x and z components of the vector
        float x = distance * Mathf.Sin(angleInRadians);
        float z = distance * Mathf.Cos(angleInRadians);

        Debug.DrawLine(new Vector3(-10f, 0), new Vector3(10f, 0), Color.red);

        // Create the Vector3 using the calculated components
        Debug.DrawRay(transform.position, new Vector3(x, z, 0f), Color.cyan);
        Debug.DrawRay(transform.position, transform.up * 10f, Color.blue);
    }
}
