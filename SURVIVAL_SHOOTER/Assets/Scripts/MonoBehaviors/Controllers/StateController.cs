using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : Singleton<StateController>
{
	private StateMachine stateMachine;

	private PlayingState playing;

	private void Awake()
	{
		Debug.Log("Awake");

		stateMachine = new StateMachine();

		playing = new PlayingState();		
	}

	private void Start()
	{
		Debug.Log("Start");

		stateMachine.ChangeState(playing);
	}

	protected void Update()
	{
		Debug.Log("Update");

		stateMachine.RunState();		
	}
}
