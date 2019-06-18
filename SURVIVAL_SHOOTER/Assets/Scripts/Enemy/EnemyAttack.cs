using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Enemy m_Enemy;
    private EnemySight m_EnemySight;

    [SerializeField]
    private float attackRange = 1f;

    [SerializeField]
    private int attackDamage = 1;
    public int AttackDamage { get => attackDamage; }

    private float attackTime;

    [SerializeField]
    private string attackAnimationName = "Z_attack_A";

    [SerializeField]
    private GameObject collisionAttack, handAttack;

    private void Awake()
    {
        m_Enemy = GetComponent<Enemy>();
        m_EnemySight = GetComponent<EnemySight>();

        for (int i = 0; i < m_Enemy.animationController.anim.runtimeAnimatorController.animationClips.Length; i++)                 //For all animations
        {
            if (m_Enemy.animationController.anim.runtimeAnimatorController.animationClips[i].name == attackAnimationName)        //If it has the same name as your clip
            {
                attackTime = m_Enemy.animationController.anim.runtimeAnimatorController.animationClips[i].length;
            }
        }

        collisionAttack.SetActive(false);
    }

    private void Update()
    {
        if (StateController.Instance.StateMachine.CurrentState == StateController.playing
            && m_Enemy.animationController.currentStates != AnimationStates.ATTACK_1
            && m_Enemy.animationController.currentStates != AnimationStates.DEAD)
        {
            if (Vector3.Distance(transform.position, m_EnemySight.PlayerTranform.position) < attackRange)
            {
                m_Enemy.OnAttack = true;

                collisionAttack.SetActive(true);

                collisionAttack.transform.position = handAttack.transform.position;

                StartCoroutine(WaitAnimationAttack(attackTime));
            }
        }
    }

    float __time = 0;

    IEnumerator WaitAnimationAttack(float p_waiTime)
    {
        __time = 0;

        while (__time < p_waiTime)
        {
            collisionAttack.transform.position = handAttack.transform.position;

            __time += Time.deltaTime;

            yield return null;
        }

        m_Enemy.OnAttack = false;

        collisionAttack.SetActive(false);
    }
}
