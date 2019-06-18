using System.Collections;

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
	#region Variables

	public Transform pathHolder;

	[SerializeField]private float movementSpeed = 3.5f;
	[SerializeField]private float waitTime = 1.5f;


	private Transform target;
    private NavMeshAgent m_NavMeshAgent;
    private Enemy m_Enemy;
	private float refreshRate = 0.25f;

	[SerializeField]
	private Vector3[] waypoints; //Pontos de ronda
	[SerializeField]
	int targetWaypointIndex;
	
	Vector3 targetWaypoint;

	#endregion

	#region Unity Methods

	private void Awake()
    {        
        m_NavMeshAgent = GetComponent<NavMeshAgent>();

		m_Enemy = GetComponent<Enemy>();

		waypoints = new Vector3[pathHolder.childCount];

		for (int i = 0; i < waypoints.Length; i++)
		{
			waypoints[i] = pathHolder.GetChild(i).position;
			waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
		}
    }
    
    void OnEnable()
    {
        m_NavMeshAgent.speed = movementSpeed;
    }

	private void Start()
	{
		//transform.position = waypoints[0];

		if (Player.Instance != null)
			target = Player.Instance.transform;

		//StartCoroutine(UpdateTargetPath());
		Patrol();		
	}

	private void OnDrawGizmos()
	{
		if (pathHolder == null)
			return;

		Vector3 startPosition = pathHolder.GetChild(0).position;
		Vector3 previousPosition = startPosition;

		foreach(Transform waypoint in pathHolder)
		{
			Gizmos.DrawSphere(waypoint.position, 0.3f);
			Gizmos.DrawLine(previousPosition, waypoint.position);

			previousPosition = waypoint.position;
		}

		Gizmos.DrawLine(previousPosition, startPosition);
	}

	#endregion

	#region Class Methods

	public void Chase()
	{
		Debug.Log("Chasing!");

		StartCoroutine(UpdateTargetPath());
	}

	public void Patrol()
	{
		Debug.Log("Patroling...");

		StartCoroutine(PatrolRoute(waypoints));
	}

	IEnumerator UpdateTargetPath()
	{
		m_NavMeshAgent.stoppingDistance = 0.5f;

		while (!m_Enemy.IsDead && target != null)
		{
			Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);

			m_NavMeshAgent.SetDestination(targetPosition);

			yield return new WaitForSeconds(refreshRate);
		}
	}

	IEnumerator PatrolRoute(Vector3[] waypoints)
	{
		m_NavMeshAgent.stoppingDistance = 0.15f;

		targetWaypointIndex = 0;

		targetWaypoint = waypoints[targetWaypointIndex];

		gameObject.transform.LookAt(targetWaypoint);

		//var state = StateController.playing;

		//Substituir por uma condição dentro da state machine do inimigo
		while (StateController.Instance.StateMachine.CurrentState == StateController.playing/*true*/)
		{
			m_NavMeshAgent.SetDestination(targetWaypoint);

			if (m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance)
			{
				m_NavMeshAgent.isStopped = true;
				
				yield return new WaitForSeconds(waitTime);				

				targetWaypointIndex++; //= (targetWaypointIndex + 1) % waypoints.Length;

				if (targetWaypointIndex > waypoints.Length-1)
					targetWaypointIndex = 0;

				targetWaypoint = waypoints[targetWaypointIndex];

				gameObject.transform.LookAt(targetWaypoint);

				m_NavMeshAgent.destination = targetWaypoint;				

				m_NavMeshAgent.isStopped = false;
			}			

			yield return null;			
		}		
	}

	public void DisableMovement()
	{
		m_NavMeshAgent.enabled = false;
	}

    #endregion
}
