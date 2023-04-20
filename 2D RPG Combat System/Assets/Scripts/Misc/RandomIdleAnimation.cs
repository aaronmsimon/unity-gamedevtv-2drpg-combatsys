using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void Start() {
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

        anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }
}
