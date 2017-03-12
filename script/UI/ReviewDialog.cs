using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewDialog : Singleton<ReviewDialog> {

	[SerializeField]
	private GameObject m_goRoot;

	public void Show()
	{
		m_goRoot.SetActive(true);
	}

	public void OnYes()
	{
		m_goRoot.SetActive(false);
		Result.Invoke(true);
	}

	public void OnNo()
	{
		m_goRoot.SetActive(false);
		Result.Invoke(false);
	}
	public UnityEventBool Result = new UnityEventBool();

}
