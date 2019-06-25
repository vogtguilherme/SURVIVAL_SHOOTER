using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;

public class MainMenuManager : MonoBehaviour
{
	private ScreenFader m_ScreenFader;
	private AsyncOperation asyncOperation;

	public GameObject buttons;
	public Text m_Text;
	public Button m_StartButton;
	public Button m_CreditsButton;
	public Button m_QuitButton;

	private void Awake()
	{
		m_ScreenFader = GetComponent<ScreenFader>();
		asyncOperation = new AsyncOperation();
	}

	private void Start()
	{
		m_Text.enabled = false;

		m_StartButton.onClick.AddListener(StartGame);
		m_QuitButton.onClick.AddListener(QuitGame);
	}

	public void StartGame()
	{
		StartCoroutine("LoadScene", 1);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	private IEnumerator LoadScene(int sceneIndex)
	{
		while(m_ScreenFader.IsFading)
			yield return null;

		buttons.SetActive(false);

		m_Text.enabled = true;

		asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);

		asyncOperation.allowSceneActivation = false;				

		while(!asyncOperation.isDone)
		{
			m_Text.text = "Carregando... " + (asyncOperation.progress * 100) + "%";			

			if(asyncOperation.progress >= 0.9f)
			{
				m_Text.text = "Pressione enter para iniciar";

				if (Input.GetKeyDown(KeyCode.Return))
				{
					asyncOperation.allowSceneActivation = true;
				}
			}

			yield return null;
		}
	}	
}
