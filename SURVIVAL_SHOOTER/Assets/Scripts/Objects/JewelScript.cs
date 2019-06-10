using System;
using UnityEngine;

public class JewelScript : PowerUp
{
	public Action OnJewelCollected;

	public Jewel jewel;

	void Start()
    {
		Settype(ItemType.CHAVE);

		jewel = new Jewel(false);
    }     

	public override void CollectItem()
	{ 
		jewel.IsCollected = true;

		OnJewelCollected.Invoke();

		gameObject.SetActive(false);
	}
}

public struct Jewel
{
	private bool isCollected;
	public bool IsCollected
	{
		get { return isCollected; }
		set { isCollected = value; }
	}

	public Jewel(bool collected)
	{
		isCollected = collected;
	}
}