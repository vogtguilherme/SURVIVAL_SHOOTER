using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : Singleton<StateController>
{
	public StateMachine StateMachine;

	public bool DebugMode = true;

	public static PlayingState playing;
	public static PausedState paused;
    public static ShopState shop;

	protected override void Awake()
	{
		base.Awake();

		StateMachine = new StateMachine();

		playing = new PlayingState();
		paused = new PausedState();
        shop = new ShopState();
	}

	private void Start()
	{
		if(DebugMode)
		{
			StateMachine.ChangeState(playing);
		}
		else
		{
			StateMachine.ChangeState(shop);
		}
	}

	protected void Update()
	{
		StateMachine.RunState();		
	}
}
