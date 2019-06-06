using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateMachine //: Singleton<StateMachine>
{
	public event Action<string> OnStateChange;

	[SerializeField]
	private IState m_CurrentState;
	[SerializeField]
	private IState m_PreviousState;

	public IState CurrentState
	{
		get
		{
			Debug.Log("Current state: " + m_CurrentState.ToString());

			return m_CurrentState;
		}
	}

	public void ChangeState(IState state)
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
