using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public event Action OnSceneUnload;
    public event Action OnSceneLoaded;

    private ScreenFader m_ScreenFader;

    private void Awake()
    {
        m_ScreenFader = GetComponent<ScreenFader>();
    }

    public void FadeAndLoadScene()
    {

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
    }

    private IEnumerator FadeAndSwitchScenes(int sceneBuildIndex)
    {
        yield return null;
    }

    private IEnumerator LoadSceneAndSetActive(int sceneBuildIndex)
    {
        yield return null;
    }
}