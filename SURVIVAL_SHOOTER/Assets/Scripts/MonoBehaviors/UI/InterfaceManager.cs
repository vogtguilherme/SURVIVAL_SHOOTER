using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
	public Text StateText;

	private void Start()
	{
		StateController.Instance.StateMachine.OnStateChange += UpdateText;
	}

	private void UpdateText(string text)
	{
		StateText.text = text;
	}
}
