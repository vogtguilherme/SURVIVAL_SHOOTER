using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAttackAutoDisable : MonoBehaviour
{
    public EnemyAttack m_EnemyAttack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ataca carai");

            Player.Instance.TakeHit(m_EnemyAttack.AttackDamage);

            gameObject.SetActive(false);
        }
    }
}
