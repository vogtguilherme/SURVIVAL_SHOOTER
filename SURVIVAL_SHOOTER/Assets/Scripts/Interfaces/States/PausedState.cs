using System;
using UnityEngine;

public class PausedState : State
{
	public event Action OnPause;
	public event Action OnUnpause;


	public override void Enter()
	{
		OnPause?.Invoke();
	}

	public override void Execute()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.Log("PAUSE");

			StateController.Instance.StateMachine.ChangeState(StateController.playing);
		}
	}

	public override void Exit()
	{
		OnUnpause?.Invoke();
	}
}
