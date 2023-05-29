using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private enum PickupType
    {
        Coin,
        Health,
        Stamina
    }

    [SerializeField] private PickupType pickupType;

    [Header("Pickup Mechanics")]
    [SerializeField] private float pickupDistance = 5f;
    [SerializeField] private float pickupAcceleration = 0.5f;
    [SerializeField] private float moveSpeed = 2;

    [Header("Popup Mechanics")]
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float popupHeight;
    [SerializeField] private float popupTime;
    [SerializeField] private float popupDistanceMin;
    [SerializeField] private float popupDistanceMax;

    private Vector3 moveDir;

    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        ResetMovement();
        StartCoroutine(AnimCurveSpawnRoutine());
    }

    private void Update() {
        Vector3 playerPos = PlayerController.Instance.transform.position;

        if (Vector3.Distance(playerPos, transform.position) < pickupDistance) {
            moveDir = (playerPos - transform.position).normalized;
            moveSpeed += pickupAcceleration;
        } else {
            ResetMovement();
        }
    }

    private void FixedUpdate() {
        rb.velocity = moveDir * moveSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>()) {
            DetectPickupType();
            Destroy(gameObject);
        }
    }

    private void ResetMovement() {
        moveDir = Vector3.zero;
        moveSpeed = 0;
    }

    private IEnumerator AnimCurveSpawnRoutine() {
        Vector2 startPos = transform.position;
        float angle = Random.Range(0, 359);
        float angleInRadians = angle * Mathf.Deg2Rad;
        float distance = Random.Range(popupDistanceMin, popupDistanceMax);
        Vector2 endPos = startPos + new Vector2(distance * Mathf.Cos(angleInRadians), distance * Mathf.Sin(angleInRadians));

        float timePassed = 0f;

        while (timePassed < popupTime) {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popupTime;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, popupHeight, heightT);

            transform.position = Vector2.Lerp(startPos, endPos, linearT) + new Vector2(0f, height);

            yield return null;
        }
    }

    private void DetectPickupType() {
        switch (pickupType) {
            case PickupType.Coin:
                // coin stuff
                Debug.Log("Received one gold coin.");
                break;
            case PickupType.Health:
                // heal player
                int playerHealth = PlayerHealth.Instance.Heal();
                Debug.Log("Healed player. Health is now " + playerHealth);
                break;
            case PickupType.Stamina:
                // stamina stuff
                Debug.Log("Received one stamina globe.");
                break;
        }
    }
}
