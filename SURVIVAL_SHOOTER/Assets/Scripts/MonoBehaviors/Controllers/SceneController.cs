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

    public string sceneToLoad;

    private ScreenFader m_ScreenFader;    

    private void Awake()
    {
        m_ScreenFader = GetComponent<ScreenFader>();
    }    

    private IEnumerator Start()
    {
        yield return StartCoroutine(LoadSceneAndSetActive(sceneToLoad));

        if (FadeOnSceneLoad)
            m_ScreenFader.FadeIn();
    }

    public void FadeAndLoadScene(string sceneName)
    {
        if(!m_ScreenFader.IsFading)
        {
            StartCoroutine(FadeAndSwitchScenes(sceneName));
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            m_ScreenFader.FadeIn();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            m_ScreenFader.FadeOut();
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            FadeAndLoadScene("Stage01");
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            FadeAndLoadScene("Stage02");
        }
    }

    private IEnumerator FadeAndSwitchScenes(string sceneName)
    {
        m_ScreenFader.FadeOut();

        while(m_ScreenFader.IsFading)
        {
            Debug.Log("Fading...");

            yield return null;
        }

        OnSceneUnload?.Invoke();

        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

        OnSceneLoaded?.Invoke();

        StateController.Instance.StateMachine.ChangeState(StateController.shop);

        m_ScreenFader.FadeIn();

        while (m_ScreenFader.IsFading)
        {
            Debug.Log("Fading...");

            yield return null;
        }
    }

    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        Scene newLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

        SceneManager.SetActiveScene(newLoadedScene);
    }
}