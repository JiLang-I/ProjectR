using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PanelManager
{
	public static List<BasePanel> Panels = new List<BasePanel>();
	public static BasePanel CurrentPanel
	{
		get
		{
			if (s_Index > -1)
			{
				return Panels[s_Index];
			}
			else
			{
				return null;
			}
		}
	} 
	private static int s_Index = -1;

	public static void Add(BasePanel panel)
	{
		Remove(panel);

		Panels.Add(panel);
		s_Index++;
	}

	public static void Remove(BasePanel panel)
	{
		while (Panels.Contains(panel))
		{
			Panels.Remove(panel);
			s_Index--;
		}
	}

	public static void Clear()
	{
		for (; s_Index >= 0; s_Index--)
		{
			Panels[s_Index].Close();
		}
		s_Index = -1;
		Panels.Clear();
	}
}

public abstract class BasePanel : MonoBehaviour
{
	[SerializeField] protected RectTransform m_RectTransform;
	[SerializeField] protected CanvasGroup m_CanvasGroup;
	protected Coroutine m_Coroutine;

	private void Reset()
	{
		if (!TryGetComponent(out m_RectTransform))
		{
			m_RectTransform = gameObject.AddComponent<RectTransform>();
		}
		if (!TryGetComponent(out m_CanvasGroup))
		{
			m_CanvasGroup = gameObject.AddComponent<CanvasGroup>();
		}
	}

	public virtual void Open()
	{
		PanelManager.Add(this);

		m_RectTransform.SetAsLastSibling();

		m_CanvasGroup.blocksRaycasts = true;
	}

	public virtual void Close()
	{
		PanelManager.Remove(this);

		m_CanvasGroup.blocksRaycasts = false;
	}
}
