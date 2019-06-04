using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingState : IState
{
	public string Name { get; set;}

	public PlayingState()
	{
		Name = "Playing";
	}

	public void Enter()
	{
		throw new System.NotImplementedException();
	}

	public void Execute()
	{
		//throw new System.NotImplementedException();
	}

	public void Exit()
	{
		throw new System.NotImplementedException();
	}
}
