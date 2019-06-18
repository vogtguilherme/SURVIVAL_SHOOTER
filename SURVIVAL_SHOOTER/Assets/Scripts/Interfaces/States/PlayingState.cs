using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingState : State
{
	public string Name { get; set;}

	public PlayingState()
	{
		Name = "Playing";
	}

	public override void Enter()
	{
		//throw new System.NotImplementedException();
		Player.Instance.OnPlayerDead += ChangeToDeadState;
	}

	public override void Execute()
	{
		//COMPORTAMENTO DO ESTADO

		Player.Instance.m_PlayerMovement.UpdateMovement();
		
		
		//CONDIÇÕES PARA TROCA DE ESTADOS

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.Log("PAUSE");

			StateController.Instance.StateMachine.ChangeState(StateController.paused);
		}

		if(Input.GetKeyDown(KeyCode.Alpha7))
		{
			Player.Instance.TakeHit(1);
			Debug.Log("Take Hit");
		}
	}

	public override void Exit()
	{
		//throw new System.NotImplementedException();
		Player.Instance.OnPlayerDead -= ChangeToDeadState;
	}

	private void ChangeToDeadState()
	{
		StateController.Instance.StateMachine.ChangeState(StateController.dead);
	}
}
