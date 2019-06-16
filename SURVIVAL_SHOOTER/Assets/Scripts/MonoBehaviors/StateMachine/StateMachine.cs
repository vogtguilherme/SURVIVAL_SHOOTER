using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class StateMachine
{
	public event Action<string> OnStateChange;

	[SerializeField]
	private State m_CurrentState;
	[SerializeField]
	private State m_PreviousState;

	public State CurrentState
	{
		get
		{
			Debug.Log("Current state: " + m_CurrentState.ToString());

			return m_CurrentState;
		}
	}

	public void ChangeState(State state)
	{
		if(m_CurrentState != null)
			m_CurrentState.Exit();

		m_PreviousState = m_CurrentState;

		m_CurrentState = state;

		m_CurrentState.Enter();

		string stateName = state.ToString().ToUpper();

		OnStateChange?.Invoke(stateName);
	}

	public void RunState()
	{
		if(m_CurrentState != null)
		{
			m_CurrentState.Execute();			

			Debug.Log("Current state: " + m_CurrentState.ToString());
		}
	}	
}
