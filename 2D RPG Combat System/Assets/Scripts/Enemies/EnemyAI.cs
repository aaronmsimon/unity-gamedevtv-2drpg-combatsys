using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State {
        Roaming
    }

    [SerializeField] private float roamTime = 2f;

    private State state;
    private EnemyPathfinding enemyPathFinding;

    private void Awake() {
        enemyPathFinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start() {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine() {
        while (state == State.Roaming) {
            Vector2 roamPos = GetRoamingPosition();
            enemyPathFinding.MoveTo(roamPos);
            yield return new WaitForSeconds(roamTime);
        }
    }

    private Vector2 GetRoamingPosition() {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
}
