using UnityEngine;

public class Enemy : Entity
{
    #region Private Variables    

    //private AudioSource m_AudioSource;
    private Collider m_Collider;
	private EnemyMovement m_EnemyMovement;
	private EnemySight m_EnemySight;

    private bool isDead;
	private bool playerOnSight;

	#endregion

	#region Public Variables

	public int minimumHealth = 3;
	public int maximumHealth = 7;
    public bool IsDead { get => isDead; set => isDead = value; }    

    #endregion

    #region Unity Methods

    private void Awake()
    {
        m_Collider = GetComponentInChildren<Collider>();

		m_Health = new Health(Random.Range(minimumHealth, maximumHealth));

		m_EnemyMovement = GetComponent<EnemyMovement>();
		m_EnemySight = GetComponent<EnemySight>();
    }

	public void EnemyUpdate()
	{
		if(!isDead)
			playerOnSight = m_EnemySight.PlayerOnSight();
	}

	#endregion

	protected override void Death()
    {
		Debug.Log(gameObject.name + " dead.");

		isDead = true;

        m_Collider.isTrigger = true;

		//Desabilitar movimento
		m_EnemyMovement.DisableMovement();
		m_EnemyMovement.StopAllCoroutines();
        
    }

	public override void TakeHit(int damage, RaycastHit hit)
	{
		TakeHit(damage);
	}
}