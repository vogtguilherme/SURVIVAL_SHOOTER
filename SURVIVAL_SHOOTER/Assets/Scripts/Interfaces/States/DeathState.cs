using UnityEngine;

public class DeadState : State
{
	public override void Enter()
	{
		
	}

	public override void Execute()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			//Reiniciar cena
			Debug.Log("Restarting scene");

			StateController.Instance.RestartLevel();
		}
	}

	public override void Exit()
	{
		
	}
}
