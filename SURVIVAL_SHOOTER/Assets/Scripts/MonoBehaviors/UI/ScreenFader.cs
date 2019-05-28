using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public event Action OnFadeCompleted;

    [SerializeField] private Image m_FadeImage;
    [SerializeField] private Color m_FadeColor;
    [SerializeField] private float m_FadeDuration;
    public bool FadeOnSceneLoad;

    private Color m_FadedOutColor;
    private bool isFading = false;

    private void Awake()
    {
        SceneManager.sceneLoaded += HandleSceneLoaded;

        m_FadedOutColor = new Color(m_FadeColor.r, m_FadeColor.g, m_FadeColor.b, 0);
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (FadeOnSceneLoad)
            FadeIn();
    }

    private void Start()
    {
        if (FadeOnSceneLoad)
            FadeIn();
    }

    public void FadeIn()
    {
        if (isFading)
            return;

        StartCoroutine("StartFadeIn");
    }

    public void FadeOut()
    {
        if (isFading)
            return;

        StartCoroutine("StartFadeOut");
    }

    private IEnumerator StartFadeIn()
    {
        yield return StartFade(m_FadeColor, m_FadedOutColor, m_FadeDuration);
    }

    private IEnumerator StartFadeOut()
    {
        yield return StartFade(m_FadedOutColor, m_FadeColor, m_FadeDuration);
    }

    private IEnumerator StartFade(Color from, Color to, float duration)
    {       
        isFading = true;

        float startTime = 0f;

        while (startTime <= duration)
        {
            m_FadeImage.color = Color.Lerp(from, to, startTime / duration);

            startTime += Time.deltaTime;

            yield return null;
        }

        isFading = false;

        Debug.Log("Fade Completed");

        if (OnFadeCompleted != null)
        {
            OnFadeCompleted();
            OnFadeCompleted = null;
        }
    }
}
