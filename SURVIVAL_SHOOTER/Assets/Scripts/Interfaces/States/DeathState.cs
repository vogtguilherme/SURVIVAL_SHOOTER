using UnityEngine;

public class DeadState : State
{
	//public InterfaceManager interfaceManager { get; set; }

	public override void Enter()
	{
		UIManager.Instance.DisplayDeathText();
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
		UIManager.Instance.HideDeathText();
	}
}
