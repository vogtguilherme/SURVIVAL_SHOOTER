using UnityEngine;

[System.Serializable]
public struct Weapon
{
	
	public string Name;
	public float FireRate { get { return m_FireRate; } set { m_FireRate = value; } }
	public float FireDistance { get { return m_FireDistance; } set { m_FireDistance = value; } }
	public int BulletDamage { get { return m_BulletDamage; } set { m_BulletDamage = value; } }
	public int MaximumAmmo { get => m_MaximumAmmo; set => m_MaximumAmmo = value; }
	public int CurrentAmmo { get => m_CurrentAmmo; set => m_CurrentAmmo = value; }
	public int CarryingAmmo { get => m_CarryingAmmo; set => m_CarryingAmmo = value; }
	

	private float m_FireRate;
	private float m_FireDistance;
	private int m_BulletDamage;
	private int m_MaximumAmmo;
	private int m_CarryingAmmo;

	[SerializeField]
	private int m_CurrentAmmo;

	public Weapon(string name, int bulletDamage, int maxAmmo, float fireRate, float fireDistance)
	{
		Name = name;
		m_BulletDamage = bulletDamage;
		m_MaximumAmmo = maxAmmo;
		m_FireRate = fireRate;
		m_FireDistance = fireDistance;

		m_CurrentAmmo = m_MaximumAmmo;
		m_CarryingAmmo = maxAmmo * 3;
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
