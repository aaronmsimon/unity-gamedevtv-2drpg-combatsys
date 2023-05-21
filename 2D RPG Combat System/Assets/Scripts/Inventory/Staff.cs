using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicLaser;
    [SerializeField] private Transform magicLaserSpawnPoint;

    private Animator staffAnimator;
    readonly int LASER_HASH = Animator.StringToHash("Attack");

    private void Awake() {
        staffAnimator = GetComponent<Animator>();
    }

    private void Update() {
        MouseFollowWithOffset();
    }

    private void MouseFollowWithOffset() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPos = PlayerController.Instance.transform.position;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, mousePos.x < playerPos.x ? -180f : 0f, (mousePos.x < playerPos.x ? 180f - angle : angle));
    }

    public void Attack() {
        staffAnimator.SetTrigger(LASER_HASH);
    }

    public WeaponInfo GetWeaponInfo() {
        return weaponInfo;
    }

    public void SpawnProjectileAnimEvent() {
        Instantiate(magicLaser, magicLaserSpawnPoint.position, Quaternion.identity);
    }
}
