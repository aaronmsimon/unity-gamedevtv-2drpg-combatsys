using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private int flashCount;
    [SerializeField] private float flashTime = .2f;

    private Material defaultMat;
    private SpriteRenderer sr;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
        defaultMat = sr.material;
    }

    public IEnumerator FlashRoutine() {
        int totalFlashes = flashCount * 2;
        for (int i = 0; i < totalFlashes; i++) {
            sr.material = i % 2 == 0 ? whiteFlashMat : defaultMat;
            yield return new WaitForSeconds(flashTime);
        }
    }
}
