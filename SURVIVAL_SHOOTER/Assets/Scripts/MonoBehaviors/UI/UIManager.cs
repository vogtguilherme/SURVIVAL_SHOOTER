using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
	public Text StateText;

	[SerializeField]
	private ScreenFader screenFader;

	[SerializeField]
	private CanvasGroup deathPanel;

	private void Start()
	{
		StateController.Instance.StateMachine.OnStateChange += UpdateText;

		StateController.paused.OnPause += ActivateScreenTint;
		StateController.paused.OnUnpause += DeactivateScreenTint;

		SetPanel(deathPanel, false, 0f);
	}

	private void SetPanel(CanvasGroup panel, bool interactable, float alpha)
	{
		panel.interactable = interactable;
		panel.blocksRaycasts = interactable;
		panel.alpha = alpha;
	}

	public void DisplayDeathText()
	{
		SetPanel(deathPanel, true, 1f);
	}

	public void HideDeathText()
	{
		SetPanel(deathPanel, false, 0f);
	}

	private void UpdateText(string text)
	{
		StateText.text = text;
	}

	public void ActivateScreenTint()
	{
		screenFader.FadeOut();
	}

	public void DeactivateScreenTint()
	{
		screenFader.FadeIn();
	}

	private void OnDisable()
	{
		if(StateController.Instance != null)
		{
			StateController.Instance.StateMachine.OnStateChange -= UpdateText;
		}

		StateController.paused.OnPause -= ActivateScreenTint;
		StateController.paused.OnUnpause -= DeactivateScreenTint;
	}
}
