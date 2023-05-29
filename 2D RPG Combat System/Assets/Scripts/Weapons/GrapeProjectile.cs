using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeProjectile : MonoBehaviour
{
    [SerializeField] private float lobTime = 1f;
    [SerializeField] private float heightY = 3f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private GameObject grapeShadowPrefab;

    private void Start() {
        Vector3 playerPos = PlayerController.Instance.transform.position;
        StartCoroutine(ProjectileCurveRoutine(transform.position, playerPos));

        GameObject grapeShadow = Instantiate(grapeShadowPrefab, transform.position + new Vector3(0f, -0.3f, 0f), Quaternion.identity);
        Vector3 grapeShadowStartPos = grapeShadow.transform.position;

        StartCoroutine(MoveGrapeShadowRoutine(grapeShadow, grapeShadowStartPos, playerPos));
    }

    private IEnumerator ProjectileCurveRoutine(Vector3 startPos, Vector3 endPos) {
        float timePassed = 0f;

        while (timePassed < lobTime) {
            timePassed += Time.deltaTime;
            float linearT = timePassed / lobTime;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPos, endPos, linearT) + new Vector2(0f, height);

            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator MoveGrapeShadowRoutine(GameObject grapeShadow, Vector3 startPos, Vector3 endPos) {
        float timePassed = 0f;

        while (timePassed < lobTime) {
            timePassed += Time.deltaTime;
            float linearT = timePassed / lobTime;

            grapeShadow.transform.position = Vector2.Lerp(startPos, endPos, linearT);

            yield return null;
        }

        Destroy(grapeShadow);
    }
}
