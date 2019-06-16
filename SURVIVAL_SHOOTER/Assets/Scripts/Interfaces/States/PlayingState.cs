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
	}

	public override void Exit()
	{
		//throw new System.NotImplementedException();
	}
}
