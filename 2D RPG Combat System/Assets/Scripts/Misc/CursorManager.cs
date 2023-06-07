using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    private Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    private void Start() {
        // disable operating system cursor
        Cursor.visible = false;

        // if playing in the editor
        if (Application.isPlaying) {
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Update() {
        Vector2 cursorPos = Input.mousePosition;;
        image.rectTransform.position = cursorPos;
    }
}
