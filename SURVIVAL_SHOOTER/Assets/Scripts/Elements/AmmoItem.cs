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

[System.Serializable]
public struct Weapon
{
	public float FireRate { get { return m_FireRate; } set { m_FireRate = value; } }
	public float FireDistance { get { return m_FireDistance; } set { m_FireDistance = value; } }
	public int BulletDamage { get { return m_BulletDamage; } set { m_BulletDamage = value; } }
	public int MaximumAmmo { get => m_MaximumAmmo; set => m_MaximumAmmo = value; }
	public int CurrentAmmo { get => m_CurrentAmmo; set => m_CurrentAmmo = value; }

	private float m_FireRate;
	private float m_FireDistance;
	private int m_BulletDamage;
	private int m_MaximumAmmo;
	[SerializeField]
	private int m_CurrentAmmo;

	public Weapon(int bulletDamage, int maxAmmo, float fireRate, float fireDistance)
	{
		m_BulletDamage = bulletDamage;
		m_MaximumAmmo = maxAmmo;
		m_FireRate = fireRate;
		m_FireDistance = fireDistance;

		m_CurrentAmmo = m_MaximumAmmo;
	}	

	public bool IsWeaponFull()
	{
		if (m_CurrentAmmo >= m_MaximumAmmo)
		{
			return true;
		}
		else
			return false;		
	}
}
