using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State {
        Roaming,
        Attacking
    }

    [SerializeField] private float roamTime = 2f;
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private bool showAttackRange;
    [SerializeField][ColorUsage(true)] private Color attackRangeColor;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;

    private State state;
    private Vector2 roamingPos;
    private float timeRoaming;
    private bool canAttack = true;

    private EnemyPathfinding enemyPathFinding;

    private void Awake() {
        enemyPathFinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start() {
        roamingPos = GetRoamingPosition();
    }

    private void Update() {
        MovementStateControl();
    }

    private void MovementStateControl() {
        switch (state) {
            case State.Roaming:
                Roaming();
                break;
            case State.Attacking:
                Attacking();
                break;
            default:
                break;
        }
    }

    private void Roaming() {
        timeRoaming += Time.deltaTime;

        enemyPathFinding.MoveTo(roamingPos);

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange) {
            state = State.Attacking;
        }
    
        if (timeRoaming > roamTime) {
            roamingPos = GetRoamingPosition();
        }
    }

    private void Attacking() {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange) {
            state = State.Roaming;
        }

        if (attackRange != 0 && canAttack) {
            (enemyType as IEnemy).Attack();
            canAttack = false;

            if (stopMovingWhileAttacking) {
                enemyPathFinding.StopMoving();
            } else {
                enemyPathFinding.MoveTo(roamingPos);
            }

            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private IEnumerator AttackCooldownRoutine() {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private Vector2 GetRoamingPosition() {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    private void OnDrawGizmos() {
        Gizmos.color = attackRangeColor;
        if (showAttackRange) {
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
