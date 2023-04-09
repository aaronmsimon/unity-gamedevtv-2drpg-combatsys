using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxOffset = -.15f;

    private Camera cam;
    private Vector2 startPos;
    private Vector2 travelDist => (Vector2)cam.transform.position - startPos;

    private void Awake() {
        cam = Camera.main;
    }

    private void Start() {
        startPos = transform.position;
    }

    private void FixedUpdate() {
        transform.position = startPos + travelDist * parallaxOffset;
    }
}
