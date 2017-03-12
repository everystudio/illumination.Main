using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDialog : Singleton<TutorialDialog> {

	public UnityEventString OnEnd = new UnityEventString();

	[SerializeField]
	private GameObject m_goRoot;

	[SerializeField]
	public List<string> m_listTutorial = new List<string>();

	[SerializeField]
	private Text txtMessage;

	private string strKey;

	public void Add(string _strKey)
	{
		if (false == DataManager.Instance.userdata.HasKey(_strKey))
		{
			m_listTutorial.Add(_strKey);
		}
	}

	public int TutorialStart()
	{
		int iRet = m_listTutorial.Count;
		if ( 0 < m_listTutorial.Count)
		{
			string strNext = m_listTutorial[0];
			m_listTutorial.RemoveAt(0);
			SimpleTutorial(strNext);
		}
		return iRet;
	}

	public bool SimpleTutorial(string _strKey)
	{
		if (false == DataManager.Instance.userdata.HasKey(_strKey))
		{
			Show(_strKey, DataManager.Instance.tutorial_message.Read(_strKey));
			DataManager.Instance.userdata.Write(_strKey, "end");
			DataManager.Instance.userdata.Save();
			return true;
		}
		return false;
	}

	public void Show(string _strKey , string _strMessage)
	{
		strKey = _strKey;
		string[] delemeter =  {"\\n" };
		Debug.Log(_strMessage);
		string[] string_arr = _strMessage.Split(delemeter, System.StringSplitOptions.None);

		//Debug.LogError(string_arr.Length);
		string strSetMessage = "";
		foreach(string buf in string_arr)
		{
			if( 0 < strSetMessage.Length)
			{
				strSetMessage += '\n';
			}
			strSetMessage += buf;
		}

		txtMessage.text = strSetMessage;

		m_goRoot.SetActive(true);

	}

	public void OnClick()
	{
		if (0 < m_listTutorial.Count)
		{
			TutorialStart();
		}
		else {
			m_goRoot.SetActive(false);
			OnEnd.Invoke(strKey);
		}
	}


}
