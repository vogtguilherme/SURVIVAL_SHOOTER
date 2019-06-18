using UnityEngine;

public class Enemy : Entity
{
    #region Private Variables    

    //private AudioSource m_AudioSource;
    private Collider m_Collider;
	private EnemyMovement m_EnemyMovement;
	private EnemySight m_EnemySight;

    private bool isDead;
    private bool onAttack;
	private bool playerOnSight;

	#endregion

	#region Public Variables

	public int minimumHealth = 3;
	public int maximumHealth = 7;
    public bool IsDead { get => isDead; set => isDead = value; }
    public bool OnAttack { get => onAttack; set => onAttack = value; }
    public bool PlayerOnSight { get => playerOnSight;}
    public Transform TargetTranform { get => m_EnemySight.PlayerTranform;}

    #endregion

    #region Unity Methods

    private void Awake()
    {
        m_Collider = GetComponentInChildren<Collider>();

		m_Health = new Health(Random.Range(minimumHealth, maximumHealth));

		m_EnemyMovement = GetComponent<EnemyMovement>();
		m_EnemySight = GetComponent<EnemySight>();
        animationController = GetComponent<AnimationController>();
    }

	public void Update()
	{
        if (StateController.Instance.StateMachine.CurrentState == StateController.playing)
        {
            if (!isDead)
                playerOnSight = m_EnemySight.PlayerOnSight();
        }

        AnimationUpdate();
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

    void AnimationUpdate()
    {
        if (isDead)
        {
            if (animationController.currentStates != AnimationStates.DEAD)
            {
                animationController.ChangeState(AnimationStates.DEAD);
            }

            return;
        }
        else if (onAttack)
        {
            if (animationController.currentStates != AnimationStates.ATTACK_1)
            {
                animationController.ChangeState(AnimationStates.ATTACK_1);
            }

            return;
        }
        else if (m_EnemyMovement.velocity.normalized.magnitude > 0.1f)
        {
            if (animationController.currentStates != AnimationStates.WALK)
            {
                animationController.ChangeState(AnimationStates.WALK);
            }

            return;
        }
        else
        {
            if (animationController.currentStates != AnimationStates.IDLE)
            {
                animationController.ChangeState(AnimationStates.IDLE);
            }
        }
    }

}