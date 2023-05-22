using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] private float laserGrowTime;

    private float laserRange;

    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider;
    private SpriteFade spriteFade;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        spriteFade = GetComponent<SpriteFade>();
    }

    private void Start() {
        LaserFaceMouse();
    }

    public void UpdateLaserRange(float laserRange) {
        this.laserRange = laserRange;
        StartCoroutine(IncreaseLaserLengthRoutine());
    }

    private void LaserFaceMouse() {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 dir = transform.position - mousePos;

        transform.right = -dir;
    }

    private IEnumerator IncreaseLaserLengthRoutine() {
        float timeElapsed = 0f;
        Vector2 originalSpriteSize = spriteRenderer.size;
        Vector2 originalColliderSize = capsuleCollider.size;
        Vector2 originalColliderOffset = capsuleCollider.offset;

        while (spriteRenderer.size.x < laserRange) {
            timeElapsed += Time.deltaTime;
            float linearTime = timeElapsed / laserGrowTime;

            // sprite
            spriteRenderer.size = new Vector2(Mathf.Lerp(originalSpriteSize.x, laserRange, linearTime), originalSpriteSize.y);

            // capsule collider
            capsuleCollider.size = new Vector2(Mathf.Lerp(originalColliderSize.x, laserRange, linearTime), originalColliderSize.y);
            capsuleCollider.offset = new Vector2(Mathf.Lerp(originalColliderOffset.x, laserRange / 2, linearTime), originalColliderOffset.y);

            yield return null;
        }

        StartCoroutine(spriteFade.FadeRoutine());
    }
}
