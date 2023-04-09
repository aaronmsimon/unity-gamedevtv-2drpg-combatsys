using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float transapencyAmount = 0.8f;
    [SerializeField] private float fadeTime = 0.4f;

    private SpriteRenderer sr;
    private Tilemap tm;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
        tm = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>()) {
            if (sr != null)
                StartCoroutine(FadeRoutine(sr, fadeTime, sr.color.a, transapencyAmount));
            if (tm != null)
                StartCoroutine(FadeRoutine(tm, fadeTime, tm.color.a, transapencyAmount));
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>()) {
            if (sr) {
                StartCoroutine(FadeRoutine(sr, fadeTime, sr.color.a, 1f));
            } else if (tm) {
                StartCoroutine(FadeRoutine(tm, fadeTime, tm.color.a, 1f));
            }
        }
    }

    private IEnumerator FadeRoutine(SpriteRenderer sr, float fadeTime, float startAlpha, float targetAlpha) {
        float elapsedTime = 0f;
        float newAlpha = 0f;
        while (elapsedTime < fadeTime) {
            elapsedTime += Time.deltaTime;
            newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeTime);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b , newAlpha);
            yield return null;
        }
    }

    private IEnumerator FadeRoutine(Tilemap tm, float fadeTime, float startAlpha, float targetAlpha) {
        float elapsedTime = 0f;
        float newAlpha = 0f;
        while (elapsedTime < fadeTime) {
            elapsedTime += Time.deltaTime;
            newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeTime);
            tm.color = new Color(tm.color.r, tm.color.g, tm.color.b , newAlpha);
            yield return null;
        }
    }
}
