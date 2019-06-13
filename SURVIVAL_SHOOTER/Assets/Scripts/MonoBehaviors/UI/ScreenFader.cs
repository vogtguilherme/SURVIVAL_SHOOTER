using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public event Action OnFadeCompleted;

    [SerializeField] private CanvasGroup m_FadeImage;
    [SerializeField] private float m_Actived, m_Desactived;
    [SerializeField] private float m_FadeDuration;    

    private bool isFading = false;

    public bool IsFading
    {
        get { return isFading; }
    }

    private void Awake()
    {

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
        yield return StartFade(m_Desactived, m_FadeDuration);
    }

    private IEnumerator StartFadeOut()
    {
        yield return StartFade(m_Actived, m_FadeDuration);
    }

    private IEnumerator StartFade(float to, float duration)
    {       
        isFading = true;

        m_FadeImage.blocksRaycasts = true;
        m_FadeImage.interactable = true;

        float initialAlpha = m_FadeImage.alpha;

        float startTime = 0f;

        while (startTime <= duration)
        {
            m_FadeImage.alpha = Mathf.Lerp(initialAlpha, to, startTime / duration);

            startTime += Time.deltaTime;

            yield return null;
        }

        m_FadeImage.blocksRaycasts = to == m_Actived;
        m_FadeImage.interactable = to == m_Actived;

        isFading = false;

        Debug.Log("Fade Completed");

        if (OnFadeCompleted != null)
        {
            OnFadeCompleted();
            OnFadeCompleted = null;
        }
    }
}
