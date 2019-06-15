using System;
using System.Collections.Generic;

using UnityEngine;

public class HealthItem : PowerUp
{
	public int amount = 3;

	private void Start()
	{
		Settype(ItemType.VIDA);
		Amount = amount;		
	}

	public override void CollectItem()
	{
		gameObject.SetActive(false);

		Player.Instance.PerformPowerUp(this.Gettype(), this.Amount);
	}		
}

[Serializable]
public struct Health
{
	[SerializeField] private int currentHealth;
	public int DefaultHealth { get; private set; }

	public int CurrentHealth
	{
		get { return currentHealth; }
		set { currentHealth = value; }
	}

	public Health(int amount)
	{
		DefaultHealth = amount;
		currentHealth = amount;
	}	
}
