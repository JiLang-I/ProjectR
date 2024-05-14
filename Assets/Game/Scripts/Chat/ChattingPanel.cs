using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChattingPanel : BasePanel
{
    private const float HEIGHT_MIN = 80;
    private const float HEIGHT_MAX = 580;
    private const float HEIGHT = 500;
    private const float SPEED = 8;

    [Header("Top")]
    [SerializeField] private Button m_ShowButton;

    [Header("ChattingPanel")]
    [SerializeField] private Image m_Outline;
    [SerializeField] private Image m_Panel;
    [SerializeField] private ScrollRect m_Scroll;
    [SerializeField] private RectTransform m_Content;

    [Header("Bottom")]
    [SerializeField] private RectTransform m_Bottom;

    private Coroutine m_Co;

    private bool m_IsShow = false;

    public void OnSwitch()
    {
        if (m_IsShow)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public override void Open()
    {
        m_IsShow = true;
        m_CanvasGroup.interactable = true;
        m_CanvasGroup.blocksRaycasts = true;
        m_Scroll.StopMovement();
        m_Content.anchoredPosition = Vector2.zero;

        if (m_Co != null)
        {
            StopCoroutine(m_Co);
        }
        m_Co = StartCoroutine(Co_Open());
    }

    public override void Close()
    {
        m_IsShow = false;
        m_Scroll.StopMovement();
        m_Content.anchoredPosition = Vector2.zero;

        if (m_Co != null)
        {
            StopCoroutine(m_Co);
        }
        m_Co = StartCoroutine(Co_Close());
    }

    private IEnumerator Co_Open()
    {
        float posX = m_RectTransform.anchoredPosition.x;
        float width = m_RectTransform.sizeDelta.x;
        float value = (m_RectTransform.sizeDelta.y - HEIGHT_MIN) / HEIGHT;

        var rectTransform = m_ShowButton.image.rectTransform;

        while (value < 1)
        {
            value += Time.deltaTime * SPEED;
            m_RectTransform.anchoredPosition = new Vector3(posX, 715 + 50 * value, 0);
            m_RectTransform.sizeDelta = new Vector2(width, HEIGHT_MIN + value * HEIGHT);
            m_Outline.color = new Color(1, 1, 1, value);
            m_Panel.color = new Color(0.25f - 0.15f * value, 0.25f - 0.15f * value, 0.25f - 0.15f * value, 0.5f + 0.35f * value);
            m_Panel.pixelsPerUnitMultiplier = 7.5f + 12.5f * value;
            m_Bottom.rotation = Quaternion.Euler(90 * (1 - value), 0, 0);
            rectTransform.rotation = Quaternion.Euler(0, 0, 180 - 180 * value);
            yield return null;
        }
        m_RectTransform.anchoredPosition = new Vector3(posX, 765, 0);
        m_RectTransform.sizeDelta = new Vector2(width, 580);
        m_Outline.color = Color.white;
        m_Panel.color = new Color(0.1f, 0.1f, 0.1f, 0.85f);
        m_Panel.pixelsPerUnitMultiplier = 20f;
        m_Bottom.rotation = Quaternion.Euler(0, 0, 0);
        rectTransform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private IEnumerator Co_Close()
    {
        float posX = m_RectTransform.localPosition.x;
        float width = m_RectTransform.sizeDelta.x;
        float value = (m_RectTransform.sizeDelta.y - HEIGHT_MIN) / HEIGHT;

        var rectTransform = m_ShowButton.image.rectTransform;

        while (value > 0)
        {
            value -= Time.deltaTime * SPEED;
            m_RectTransform.anchoredPosition = new Vector3(posX, 715 + 50 * value, 0);
            m_RectTransform.sizeDelta = new Vector2(width, HEIGHT_MIN + value * HEIGHT);
            m_Outline.color = new Color(1, 1, 1, value);
            m_Panel.color = new Color(0.25f - 0.15f * value, 0.25f - 0.15f * value, 0.25f - 0.15f * value, 0.5f + 0.35f * value);
            m_Panel.pixelsPerUnitMultiplier = 7.5f + 12.5f * value;
            m_Bottom.rotation = Quaternion.Euler(90 * (1 - value), 0, 0);
            rectTransform.rotation = Quaternion.Euler(0, 0, 180 - 180 * value);
            yield return null;
        }
        m_RectTransform.anchoredPosition = new Vector3(posX, 715, 0);
        m_RectTransform.sizeDelta = new Vector2(width, 80);
        m_Outline.color = new Color(1, 1, 1, 0);
        m_Panel.color = new Color(0.25f, 0.25f, 0.25f, 0.5f);
        m_Panel.pixelsPerUnitMultiplier = 7.5f;
        m_Bottom.rotation = Quaternion.Euler(90, 0, 0);
        rectTransform.rotation = Quaternion.Euler(0, 0, 180);
    }

}
