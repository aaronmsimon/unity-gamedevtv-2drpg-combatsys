using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    private Animator bowAnimator;

    // more performant
    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private void Awake() {
        bowAnimator = GetComponent<Animator>();
    }

    public void Attack() {
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        bowAnimator.SetTrigger(FIRE_HASH);
    }

    public WeaponInfo GetWeaponInfo() {
        return weaponInfo;
    }
}
