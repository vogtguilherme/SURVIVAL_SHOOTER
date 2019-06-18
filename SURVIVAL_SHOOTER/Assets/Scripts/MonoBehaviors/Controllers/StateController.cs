using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : Singleton<StateController>
{
	[SerializeField] private SceneController m_sceneController;

	public StateMachine StateMachine;

	public bool DebugMode = true;

	public static PlayingState playing;
	public static PausedState paused;
    public static ShopState shop;
	public static DeadState dead;

	protected override void Awake()
	{
		base.Awake();

		StateMachine = new StateMachine();

		playing = new PlayingState();
		paused = new PausedState();
        shop = new ShopState();

		dead = new DeadState();

		m_sceneController = GetComponent<SceneController>();
	}

	private void OnEnable()
	{
		m_sceneController.OnSceneLoaded += SetShopState;
	}

	private void Start()
	{
		
	}	

	protected void Update()
	{
		StateMachine.RunState();		
	}	

	public void SetShopState()
	{
		StateMachine.ChangeState(shop);
	}

	public void RestartLevel()
	{
		int currentLevel = m_sceneController.CurrentScene();

		m_sceneController.FadeAndLoadScene(currentLevel);
	}

	private void OnDisable()
	{
		m_sceneController.OnSceneLoaded -= SetShopState;
	}
}
