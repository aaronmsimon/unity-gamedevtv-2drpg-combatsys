using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    private void Update() {
        MouseFollowWithOffset();
    }

    private void MouseFollowWithOffset() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, mousePos.x < playerScreenPoint.x ? 180f : 0f, angle);
    }

    public void Attack() {
        Debug.Log("Staff attack");
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}
