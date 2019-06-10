using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public event Action OnFadeCompleted;

    [SerializeField] private Image m_FadeImage;
    [SerializeField] private Color m_FadeColor;
    [SerializeField] private float m_FadeDuration;    

    private Color m_FadedOutColor;
    private bool isFading = false;

    public bool IsFading
    {
        get { return isFading; }
    }

    private void Awake()
    {
        m_FadedOutColor = new Color(m_FadeColor.r, m_FadeColor.g, m_FadeColor.b, 0);
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

		if (!m_FadeImage.IsActive())
			m_FadeImage.enabled = true;

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
