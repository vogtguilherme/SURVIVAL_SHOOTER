using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    public AnimationController animationController;

	[SerializeField]
	protected Health m_Health;

	public abstract void TakeHit(int damage, RaycastHit hit);

	public virtual void TakeHit(int damage/*, RaycastHit hit*/)
	{
		//Se a vida já for igual ou menor que zero
		if (m_Health.CurrentHealth <= 0)
			//Retornar
			return;
		
		//Desconta o valor de damage no valor atual
		m_Health.CurrentHealth -= damage;
		Debug.Log("Damage");

		//Se a vida restante for menor que um
		if(m_Health.CurrentHealth < 1)
			//Morrer
			Death();
	}

	protected abstract void Death();
}
