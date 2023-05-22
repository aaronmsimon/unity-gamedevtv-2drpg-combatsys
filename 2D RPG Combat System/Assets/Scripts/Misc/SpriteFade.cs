using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    [SerializeField] private float fadeTime;

    private SpriteRenderer sr;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    public IEnumerator FadeRoutine() {
        float elapsedTime = 0f;
        float newAlpha = 0f;
        float startAlpha = sr.color.a;
        float targetAlpha = 0f;

        while (elapsedTime < fadeTime) {
            elapsedTime += Time.deltaTime;
            newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeTime);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b , newAlpha);
            yield return null;
        }

        Destroy(gameObject);
    }
}
