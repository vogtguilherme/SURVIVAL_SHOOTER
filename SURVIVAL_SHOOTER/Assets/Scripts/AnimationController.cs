using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum AnimationStates
{
    IDLE, WALK, DEAD, ATTACK_1
}

public class AnimationController : MonoBehaviour
{
    [Header("Current State")]
    public AnimationStates currentStates;

    [Header("Animations")]
    public Animator anim;

    private void Awake()
    {
        anim.SetInteger("State", (int)currentStates);
    }

    public void ChangeState(AnimationStates p_state)
    {
        currentStates = p_state;

        anim.SetInteger("State", (int)currentStates);
    }
}
