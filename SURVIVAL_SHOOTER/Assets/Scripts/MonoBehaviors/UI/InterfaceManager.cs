using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
	public Text StateText;

	[SerializeField]
	private ScreenFader screenFader;

	private void Start()
	{
		StateController.Instance.StateMachine.OnStateChange += UpdateText;

		StateController.paused.OnPause += ActivateScreenTint;
		StateController.paused.OnUnpause += DeactivateScreenTint;
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
		StateController.Instance.StateMachine.OnStateChange -= UpdateText;

		StateController.paused.OnPause -= ActivateScreenTint;
		StateController.paused.OnUnpause -= DeactivateScreenTint;
	}
}
