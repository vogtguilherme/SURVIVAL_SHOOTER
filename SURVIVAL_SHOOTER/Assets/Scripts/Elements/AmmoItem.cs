using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : PowerUp
{
	public int amount;

	private void Start()
	{
		Settype(ItemType.MUNICAO);
		Amount = amount;
	}

	public override void CollectItem()
	{
		gameObject.SetActive(false);

		Player.Instance.PerformPowerUp(this.Gettype(), this.Amount);
	}
}