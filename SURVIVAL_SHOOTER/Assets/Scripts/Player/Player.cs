using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity, ICollectable
{
	#region Private Variables

	public PlayerMovement m_PlayerMovement { get; private set; }
    public PlayerShooting m_PlayerShooting { get; private set; }

	#endregion

	#region Public Variables

	public static Player Instance;

	public event System.Action OnVariableChanged;
	public event System.Action OnPlayerDead;

	public float playerSpeed;	

	public Health PlayerHealth
	{
		get { return m_Health; }		
	}

	public Weapon m_Weapon;	

	#endregion

	void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_PlayerShooting = GetComponentInChildren<PlayerShooting>();		

		m_Health = new Health(5);
		m_Weapon = new Weapon(1, 12, 0.2f, 20f);	
	}

    void OnEnable()
    {
        m_PlayerMovement.MovementSpeed = playerSpeed;	
	}

	public override void TakeHit(int damage, RaycastHit hit)
	{
		base.TakeHit(damage, hit);

		TriggerEvent();
	}	

	public void DisableFunctions()
	{
		//m_PlayerMovement.MovementSpeed = 0f;
		m_PlayerMovement.DisableMovement();
		m_PlayerMovement.enabled = false;
		m_PlayerShooting.enabled = false;
	}

	protected override void Death()
	{
		OnPlayerDead();
		DisableFunctions();
	}

	public void PerformPowerUp(ItemType type, int amount)
	{
		if(type == ItemType.VIDA)
		{
			if(m_Health.CurrentHealth == m_Health.DefaultHealth)
			{
				Debug.Log("Health is full.");
				return;
			}

			int projectedHealth;

			projectedHealth = m_Health.CurrentHealth + amount;

			if (projectedHealth > m_Health.DefaultHealth)
			{
				m_Health.CurrentHealth = m_Health.DefaultHealth;
			}
			else
			{
				m_Health.CurrentHealth += amount;
			}

			Debug.Log("Recovered " + amount + " HP");
		}

		if(type == ItemType.MUNICAO)
		{
			bool full;	

			full = m_Weapon.IsWeaponFull();

			if (full)
			{
				Debug.Log("Weapon magazine is full.");
				return;
			}

			int projectedAmmo = m_Weapon.CurrentAmmo + amount;

			if(projectedAmmo > m_Weapon.MaximumAmmo)
			{
				m_Weapon.CurrentAmmo = m_Weapon.MaximumAmmo;
			}
			else
			{
				m_Weapon.CurrentAmmo += amount;
			}

			Debug.Log("Acquired " + amount + " bullets");
		}

		TriggerEvent();
		
	}

	public void TriggerEvent()
	{
		if (OnVariableChanged != null)
			OnVariableChanged();
	}
}
