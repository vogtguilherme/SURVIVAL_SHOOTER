using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public event Action OnSceneUnload;
    public event Action OnSceneLoaded;

    [SerializeField]private bool FadeOnSceneLoad;

	public GameObject UICamera, MainCamera;

	public int sceneToLoadIndex;

    private ScreenFader m_ScreenFader;    

    private void Awake()
    {
        m_ScreenFader = GetComponent<ScreenFader>();

		if (UICamera == null)
			GameObject.FindGameObjectWithTag("InterfaceCamera");
    }    

    private IEnumerator Start()
    {
        yield return StartCoroutine(LoadSceneAndSetActive(sceneToLoadIndex));

        if (FadeOnSceneLoad)
            m_ScreenFader.FadeIn();
    }

    public void FadeAndLoadScene(int sceneIndex)
    {
        if(!m_ScreenFader.IsFading)
        {
            StartCoroutine(FadeAndSwitchScenes(sceneIndex));
        }
    }    

    private IEnumerator FadeAndSwitchScenes(int sceneIndex)
    {
        m_ScreenFader.FadeOut();

        while(m_ScreenFader.IsFading)
        {
            Debug.Log("Fading...");

            yield return null;
        }

		if(MainCamera != null)
			MainCamera.SetActive(false);

		UICamera.SetActive(true);

        OnSceneUnload?.Invoke();

        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        yield return StartCoroutine(LoadSceneAndSetActive(sceneIndex));

        OnSceneLoaded?.Invoke();

        //StateController.Instance.StateMachine.ChangeState(StateController.shop);

        m_ScreenFader.FadeIn();

        while (m_ScreenFader.IsFading)
        {
            Debug.Log("Fading...");

            yield return null;
        }

		UICamera.SetActive(false);
		MainCamera.SetActive(true);
	}

    private IEnumerator LoadSceneAndSetActive(int sceneIndex)
    {
        yield return SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

        Scene newLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

        SceneManager.SetActiveScene(newLoadedScene);

		MainCamera = Camera.main.gameObject;

		UICamera.SetActive(false);
		//MainCamera = GameObject.Find("MainCamera");
	}
}