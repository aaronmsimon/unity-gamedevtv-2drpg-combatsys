using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private void Update() {
        FaceMouse();
    }

    private void FaceMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 dir = transform.position - mousePos;

        // transform.right = -dir;
    }
}
