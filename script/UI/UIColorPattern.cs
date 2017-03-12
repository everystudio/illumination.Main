using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColorPattern : CPanel {

	[SerializeField]
	private RectTransform rtSave;
	[SerializeField]
	private RectTransform rtPattern;

	[SerializeField]
	private GameObject m_goRootColorBar;

	[SerializeField]
	private Text m_txtPattern;
	[SerializeField]
	private Text m_txtSave;

	[SerializeField]
	private Button btnSave;
	
	private List<ColorBar> m_listColorBar = new List<ColorBar>();


	public void ReviewResult(bool _bResult)
	{
		ReviewDialog.Instance.Result.RemoveListener(ReviewResult);
		if ( _bResult == true)
		{
			DataManager.Instance.userdata.Write("reviewed", "reviewed");
			DataManager.Instance.userdata.Save();
			move_addcolor();

#if UNITY_ANDROID
			Application.OpenURL("market://details?id=tools.everystudio.illumination.simple");
#elif UNITY_IPHONE
			Application.OpenURL("http://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?id=1214683737&pageNumber=0&sortOrdering=2&type=Purple+Software&mt=8");
#endif


		}
	}

	private void move_addcolor()
	{
		DataManager.Instance.IsModeEdit = false;
		DataManager.Instance.editColorParam = null;
		UIAssistant.main.ShowPage("ColorEdit");
	}

	public void OnAddColor()
	{
		int iCount = 0;

		foreach( DataColorParam param in DataManager.Instance.color.list)
		{
			if( param.group_id == 1)
			{
				iCount += 1;
			}
		}

		if (5 <= iCount && !DataManager.Instance.userdata.HasKey("reviewed"))
		{
			ReviewDialog.Instance.Show();
			ReviewDialog.Instance.Result.AddListener(ReviewResult);
		}
		else {
			move_addcolor();
		}
	}

	protected override void awake()
	{
		base.awake();
		DeviceOrientationDetector.Instance.OnChangeOrientation.AddListener(OnChangeOrientation);
	}

	protected override void panelStart()
	{
		base.panelStart();
		RefreshPattern(1);
		RefreshColor(1);
		m_txtSave.text = "";
		OnChangeOrientation(DeviceOrientationDetector.Instance.orientation);
		btnSave.interactable = false;
	}

	public void RefreshColor(int _group_id)
	{
		foreach (ColorBar bar in m_listColorBar)
		{
			Destroy(bar.gameObject);
		}
		m_listColorBar.Clear();

		ColorBar[] barArr = m_goRootColorBar.GetComponentsInChildren<ColorBar>();
		foreach (ColorBar bar in barArr)
		{
			Destroy(bar.gameObject);
		}

		List<DataColorParam> bufList = new List<DataColorParam>();
		foreach (DataColorParam param in DataManager.Instance.color.list)
		{
			if (param.group_id == 1)
			{
				bufList.Add(param);
			}
		}

		bufList.Sort((a, b) => a.index - b.index);
		foreach (DataColorParam param in bufList)
		{
			ColorBar bar = PrefabManager.Instance.MakeScript<ColorBar>("prefab/ColorBar", m_goRootColorBar);
			bar.gameObject.transform.localScale = Vector3.one;
			bar.Initialize(param);
			bar.OnDownEvent.AddListener(OnDown);
			bar.OnUpEvent.AddListener(OnUp);
			bar.OnEditEvent.AddListener(OnEdit);
			bar.OnDeleteEvent.AddListener(OnDelete);
			m_listColorBar.Add(bar);
		}

	}
	public void RefreshPattern(int _group_id)
	{
		string strPattern = "";
		foreach (DataPatternParam pattern in DataManager.Instance.pattern.list)
		{
			if (pattern.group_id == 1)
			{
				strPattern = pattern.pattern;
				break;
			}
		}
		m_txtPattern.text = strPattern;

	}

	public void swap(DataColorParam _param , int _iMove )
	{
		int move_index = _param.index + _iMove;
		if( move_index < 0)
		{
			return;
		}
		else if( m_listColorBar.Count <= move_index)
		{
			return;
		}

		DataColorParam temp_a = null;
		DataColorParam temp_b = null;
		foreach (DataColorParam param in DataManager.Instance.color.list)
		{
			if (_param.group_id == param.group_id)
			{
				if (_param.index == param.index)
				{
					temp_a = param;
				}
				else if (_param.index +_iMove == param.index)
				{
					temp_b = param;
				}
			}
		}
		temp_a.index += _iMove;
		temp_b.index -= _iMove;

		RefreshColor(_param.group_id);
	}

	private void OnDown(DataColorParam _param)
	{
		swap(_param, 1);
		m_txtSave.text = "";
		btnSave.interactable = true;
		TutorialDialog.Instance.Add("move1");
		TutorialDialog.Instance.Add("save1");
		TutorialDialog.Instance.Add("save2");
		TutorialDialog.Instance.TutorialStart();
	}
	private void OnUp(DataColorParam _param)
	{
		//Debug.LogError(_param.index);
		swap(_param, -1);
		m_txtSave.text = "";
		btnSave.interactable = true;
		TutorialDialog.Instance.Add("move1");
		TutorialDialog.Instance.Add("save1");
		TutorialDialog.Instance.Add("save2");
		TutorialDialog.Instance.TutorialStart();
	}
	private void OnEdit(DataColorParam _param)
	{
		//Debug.LogError(_param.index);
		m_txtSave.text = "";
		btnSave.interactable = true;
		DataManager.Instance.IsModeEdit = true;
		DataManager.Instance.editColorParam = _param;
		UIAssistant.main.ShowPage("ColorEdit");


	}

	private void OnDelete(DataColorParam _param)
	{
		m_txtSave.text = "";
		btnSave.interactable = true;

		DataManager.Instance.color.list.Remove(_param);

		int check_group_id = 1;
		int index = 0;
		foreach( DataColorParam param in DataManager.Instance.color.list)
		{
			if( param.group_id == check_group_id)
			{
				param.index = index;
				index += 1;
			}
		}


		RefreshColor(1);
	}

	private void rotation_pattern(DataPatternParam _pattern)
	{
		string strRet = "";
		switch( _pattern.pattern)
		{
			case "gradation":
			case "Gradation":
				strRet = "Fade";
				break;
			case "fade":
			case "Fade":
				strRet = "Slide";
				break;
			case "slide":
			case "Slide":
			default:
				strRet = "Gradation";
				break;
		}
		_pattern.pattern = strRet;
		m_txtSave.text = "";
		btnSave.interactable = true;
	}
	public void OnChangePattern()
	{
		foreach( DataPatternParam pattern in DataManager.Instance.pattern.list)
		{
			if( pattern.group_id == 1)
			{
				rotation_pattern(pattern);
				break;
			}
		}
		RefreshPattern(1);
		m_txtSave.text = "";
		btnSave.interactable = true;


		TutorialDialog.Instance.Add("pattern1");
		TutorialDialog.Instance.TutorialStart();
	}

	public void OnSave()
	{
		DataManager.Instance.Save();
		m_txtSave.text = "Success";
		btnSave.interactable = false;
	}

	protected override void panelEndStart()
	{
		base.panelEndStart();

		DataManager.Instance.Reload();
	}



	private void OnChangeOrientation(DeviceOrientationDetector.ORIENTATION _orientaiton)
	{
		if (_orientaiton == DeviceOrientationDetector.ORIENTATION.YOKO)
		{
			rtSave.pivot = new Vector2(0.5f, 0.5f);
			rtSave.anchorMin = new Vector2(1, 0);
			rtSave.anchorMax = new Vector2(1, 0);
			rtSave.offsetMin = new Vector2(-204.7f, 59.95f);
			rtSave.offsetMax = new Vector2(-23.29999f, 130.05f);
			rtSave.sizeDelta = new Vector2(181.4f, 70.1f);

			rtPattern.pivot = new Vector2(0.5f, 0.5f);
			rtPattern.anchorMin = new Vector2(0, 0);
			rtPattern.anchorMax = new Vector2(0, 0);
			rtPattern.offsetMin = new Vector2(15.3f, 57.8f);
			rtPattern.offsetMax = new Vector2(196.7f, 106.2f);
			rtPattern.sizeDelta = new Vector2(181.4f, 48.4f);
		}
		else
		{
			//CheckRectTransform(rtSave, "rtSave");
			rtSave.pivot = new Vector2(0.5f, 0.5f);
			rtSave.anchorMin = new Vector2(1, 0);
			rtSave.anchorMax = new Vector2(1, 0);
			rtSave.offsetMin = new Vector2(-204.7f, 59.95f);
			rtSave.offsetMax = new Vector2(-23.29999f, 130.05f);
			rtSave.sizeDelta = new Vector2(181.4f, 70.1f);

			rtPattern.pivot = new Vector2(0.5f, 0.5f);
			rtPattern.anchorMin = new Vector2(0, 0);
			rtPattern.anchorMax = new Vector2(0, 0);
			rtPattern.offsetMin = new Vector2(15.3f, 57.8f);
			rtPattern.offsetMax = new Vector2(196.7f, 106.2f);
			rtPattern.sizeDelta = new Vector2(181.4f, 48.4f);
		}
	}


}
