using UnityEngine;

public class PausedState : IState
{
	public PausedState(){ }


	public void Enter()
	{
		
	}

	public void Execute()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.Log("PAUSE");

			StateController.Instance.StateMachine.ChangeState(StateController.playing);
		}
	}

	public void Exit()
	{
		
	}
}
