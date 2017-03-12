using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventColorBar : UnityEvent<DataColorParam>
{
}
public class ColorBar : MonoBehaviour {

	[SerializeField]
	private Image m_imgBar;
	[SerializeField]
	private Button m_btn;

	[SerializeField]
	private Text m_txtNumber;

	public int m_iIndex;
	public float m_fColorRed;
	public float m_fColorGreen;
	public float m_fColorBlue;

	public DataColorParam m_param;

	private void _initialize( int _iIndex , float _fRed , float _fGreen , float _fBlue )
	{
		m_fColorRed = _fRed;
		m_fColorGreen = _fGreen;
		m_fColorBlue = _fBlue;
		//Debug.LogError(string.Format("r={0} g={1} b={2}", _fRed, _fGreen, _fBlue));
		m_imgBar.color = new Color(_fRed / 255.0f, _fGreen / 255.0f, _fBlue / 255.0f);

		m_txtNumber.text = string.Format("{0}", _iIndex + 1);
	}

	public void Initialize( DataColorParam _param)
	{
		m_param = _param;
		_initialize(_param.index, _param.red, _param.green, _param.blue);
	}

	public EventColorBar OnEditEvent = new EventColorBar();
	public void OnEdit()
	{
		OnEditEvent.Invoke(m_param);
	}

	public EventColorBar OnUpEvent = new EventColorBar();
	public void OnUp()
	{
		OnUpEvent.Invoke(m_param);
	}
	public EventColorBar OnDownEvent = new EventColorBar();
	public void OnDown()
	{
		OnDownEvent.Invoke(m_param);
	}
	public EventColorBar OnDeleteEvent = new EventColorBar();
	public void OnDelete()
	{
		OnDeleteEvent.Invoke(m_param);
	}

}
