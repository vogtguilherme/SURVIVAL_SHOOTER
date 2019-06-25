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

		UIManager.Instance.hudController.playerStatsHolder.SetActive(true);
		
		Player.Instance.OnVariableChanged += UIManager.Instance.hudController.SetupHUD;

		UIManager.Instance.hudController.SetupHUD();
	}

	public override void Execute()
	{
		//COMPORTAMENTO DO ESTADO

		Player.Instance.m_PlayerMovement.UpdateMovement();

		Player.Instance.m_PlayerShooting.ShootingBehavior();
		
		
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

		UIManager.Instance.hudController.playerStatsHolder.SetActive(false);
	}

	private void ChangeToDeadState()
	{
		StateController.Instance.StateMachine.ChangeState(StateController.dead);
	}
}
