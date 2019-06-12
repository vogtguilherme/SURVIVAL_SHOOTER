using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : Singleton<StateController>
{
	public StateMachine StateMachine;

	public static PlayingState playing;
	public static PausedState paused;
    public static ShopState shop;

	private void Awake()
	{
		Debug.Log("Awake");

		StateMachine = new StateMachine();

		playing = new PlayingState();
		paused = new PausedState();
        shop = new ShopState();
	}

	private void Start()
	{
		Debug.Log("Start");

		StateMachine.ChangeState(shop);
	}

	protected void Update()
	{
		Debug.Log("Update");

		StateMachine.RunState();		
	}
}
