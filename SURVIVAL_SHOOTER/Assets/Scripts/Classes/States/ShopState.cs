using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopState : State
{
    public string Name { get; set; }

    public ShopState()
    {
        Name = "Shop";
    }

    public override void Enter()
	{
		if (ShopInstance.Instance)
        {
            ShopInstance.Instance.shopGUI.Open();
        }
    }

    public override void Execute()
	{
		if(ShopInstance.Instance)
        {
            ShopInstance.Instance.onStartGameClicked += ChangeToPlayingState;
        }
	}

	public override void Exit()
	{
        if (ShopInstance.Instance)
        {
            ShopInstance.Instance.onStartGameClicked -= ChangeToPlayingState;
        }
	}

    void ChangeToPlayingState()
    {
        Debug.Log("PLAYING");

        StateController.Instance.StateMachine.ChangeState(StateController.playing);
    }
}
 

