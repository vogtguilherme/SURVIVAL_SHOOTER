using UnityEngine;
using System;
using System.Collections;

public class EnemySight : MonoBehaviour
{
	public event Action OnPlayerSpotted;

	[SerializeField] private LayerMask viewMask;
	[SerializeField] private float viewDistance = 7.5f;
	[SerializeField] private float fieldOfView = 110f;

	public bool IsPlayerSighted{ get; set; }

	[SerializeField]private Transform m_Player;
	private SphereCollider m_Collider;
	private Vector3 playerCurrentPosition;
	private Vector3 lastSeenPlayerPos;
	//private int viewMask;	

	[Space(0.5f)]
	[Header("Debug")]
	[SerializeField] private bool drawDebugRay = true;

    public Transform PlayerTranform { get => m_Player; }

	private void Start()
	{
		m_Player = Player.Instance.transform;

		IsPlayerSighted = false;
	}

	void Update()
	{
		if (drawDebugRay)
			Debug.DrawRay(transform.position, transform.forward * viewDistance, Color.green);
	}

	public bool PlayerOnSight()
	{
		//Verifica se o jogador está dentro da distância em que o inimigo enxerga		/ Alt. Implementar função com detecção de trigger
		if (Vector3.Distance(transform.position, m_Player.position) < viewDistance)
		{
            return true;

			//Vetor da posicao do player para o inimigo
			Vector3 directionToPlayer = (m_Player.position - transform.position).normalized;
			//Angulo entre o vetor forward e o vetor para o player
			float anglePlayer = Vector3.Angle(transform.forward, directionToPlayer);
			//Verifica se o jogador está dentro do campo de visão
			if(anglePlayer < fieldOfView / 2)
			{
				//Executa em line cast para o player para descobrir se tem algo no caminho, ex. uma parede
				if (!Physics.Linecast(transform.position, m_Player.position, viewMask))
				{
					//Atualiza a posição do player			/ Criar posição global do jogador para inimigos próximos tambem perseguí-lo
					playerCurrentPosition = m_Player.position;
					
					IsPlayerSighted = true;

					Debug.Log("Player Spotted!");

					if(OnPlayerSpotted != null)
					{
						OnPlayerSpotted.Invoke();
						Debug.Log("On Player Spotted");						
					}

					return true;
				}
			}
		}
		else
		{
			IsPlayerSighted = false;

			lastSeenPlayerPos = playerCurrentPosition;
		}

		return false;
	}
}
