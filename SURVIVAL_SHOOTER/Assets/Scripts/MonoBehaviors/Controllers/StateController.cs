using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : Singleton<StateController>
{
	public StateMachine StateMachine;

	public static PlayingState playing;
	public static PausedState paused;

	private void Awake()
	{
		Debug.Log("Awake");

		StateMachine = new StateMachine();

		playing = new PlayingState();
		paused = new PausedState();
	}

	private void Start()
	{
		Debug.Log("Start");

		StateMachine.ChangeState(playing);
	}

	protected void Update()
	{
		Debug.Log("Update");

		StateMachine.RunState();		
	}
}
