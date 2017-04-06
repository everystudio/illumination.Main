using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightRotation : MonoBehaviourEx {

	[SerializeField]
	private RectTransform rtScreen;

	[SerializeField]
	private Image img;

	[SerializeField]
	private Image imgSub;

	[SerializeField]
	private Button btn;

	private List<DataColorParam> color_list = new List<DataColorParam>();
	private int index;

	public Color fadeTo;
	public Color fadeTarget;

	void Awake()
	{
		/*
		CheckRectTransform(rtScreen, "rtScreen");
		Debug.LogError(DeviceOrientationDetector.Instance.orientation);
		Debug.LogError(rtScreen.sizeDelta.x);
		Debug.LogError(rtScreen.sizeDelta.y);
		Debug.LogError(rtScreen.localScale);
		Debug.LogError(rtScreen.gameObject.transform.localScale);
		*/
		imgSub.transform.localScale = Vector3.zero;
		DeviceOrientationDetector.Instance.OnChangeOrientation.AddListener(OnChangeOrientation);
	}
	private void OnChangeOrientation(DeviceOrientationDetector.ORIENTATION _orientaiton)
	{
	}

	public void LightStop()
	{
		iTween.Stop(gameObject);
		img.raycastTarget = true;
		imgSub.raycastTarget = true;
	}
	public void OnStop()
	{
		LightStop();
		img.raycastTarget = false;
		imgSub.raycastTarget = false;
		UIAssistant.main.ShowPage("TopMenu");
	}

	public void LightStart( DataPatternParam _pattern)
	{
		index = 0;

		img.raycastTarget = true;
		imgSub.raycastTarget = true;

		//Debug.LogError(Screen.width);
		//CheckRectTransform(img.rectTransform, "img");
		//CheckRectTransform(imgSub.rectTransform, "imgSub");
		//Debug.LogError(rtScreen.sizeDelta.x);
		imgSub.transform.localScale = Vector3.zero;


		color_list.Clear();
		foreach (DataColorParam param in DataManager.Instance.color.list)
		{
			if( _pattern.group_id == param.group_id)
			{
				color_list.Add(param);
			}
		}
		color_list.Sort((a, b) => a.index - b.index);

		if (_pattern.pattern.Equals("Gradation"))
		{
			OnGradationCompleteChangeColor(0);
		}
		else if(_pattern.pattern.Equals("Fade"))
		{
			OnFadeUpCompleteChangeColor(0);
		}
		else if (_pattern.pattern.Equals("Slide"))
		{
			OnSlideWaitCompleteChangeColor(0);
		}
		else
		{
			OnGradationCompleteChangeColor(0);
		}
	}

	#region グラデーション
	public void ChangeColorGradation(float newValue)
	{
		Color target = GetColor(color_list[(index + 1) % color_list.Count]);
		Color from = GetColor(color_list[(index + 0) % color_list.Count]);
		var diff = target - from;
		img.color = new Color(
			from.r + diff.r * newValue,
			from.g + diff.g * newValue,
			from.b + diff.b * newValue
		);
	}

	public void OnGradationCompleteChangeColor(int _index)
	{
		index = _index;
		img.color = GetColor(color_list[index]);
		iTween.ValueTo(gameObject,
			iTween.Hash(
				"time", 6.0f,
				"from", 0.0f,
				"to", 1.0f,
				"onupdate", "ChangeColorGradation",
				"oncomplete", "OnGradationCompleteChangeColor",
				"oncompleteparams", (_index + 1) % color_list.Count
				)
				);
	}
	#endregion

	#region フェード
	public void ChangeColorFade(float newValue)
	{
		var diff = fadeTarget - fadeTo;
		img.color = new Color(
			fadeTo.r + diff.r * newValue,
			fadeTo.g + diff.g * newValue,
			fadeTo.b + diff.b * newValue
		);
	}
	public void OnFadeUpCompleteChangeColor(int _index)
	{
		fadeTo = Color.black;
		fadeTarget = GetColor(color_list[_index]);
		img.color = fadeTo;

		iTween.ValueTo(gameObject,
			iTween.Hash(
				"time", 6.0f,
				"from", 0.0f,
				"to", 1.0f,
				"onupdate", "ChangeColorFade",
				"oncomplete", "OnFadeWaitCompleteChangeColor",
				"oncompleteparams", (_index) % color_list.Count
				)
				);
	}
	public void OnFadeWaitCompleteChangeColor(int _index)
	{
		iTween.ValueTo(gameObject,
			iTween.Hash(
				"time", 3.0f,
				"from", 0.0f,
				"to", 1.0f,
				"onupdate", "dummy",
				"oncomplete", "OnFadeDownCompleteChangeColor",
				"oncompleteparams", (_index) % color_list.Count
				)
				);
	}
	public void OnFadeDownCompleteChangeColor(int _index)
	{
		fadeTo = GetColor(color_list[_index]);
		fadeTarget = Color.black;
		img.color = fadeTo;
		index += 1;

		iTween.ValueTo(gameObject,
			iTween.Hash(
				"time", 6.0f,
				"from", 0.0f,
				"to", 1.0f,
				"onupdate", "ChangeColorFade",
				"oncomplete", "OnFadeUpCompleteChangeColor",
				"oncompleteparams", (_index + 1) % color_list.Count
				)
				);
	}
	#endregion

	#region スライド
	public void OnSlideWaitCompleteChangeColor(int _index)
	{
		img.color = GetColor(color_list[_index]);
		imgSub.transform.localScale = Vector3.zero;

		iTween.ValueTo(gameObject,
			iTween.Hash(
				"time", 6.0f,
				"from", 0.0f,
				"to", 1.0f,
				"onupdate", "dummy",
				"oncomplete", "OnSlideCompleteChangeColor",
				"oncompleteparams", (_index ) % color_list.Count
				)
				);
	}
	public void OnSlideCompleteChangeColor(int _index)
	{
		img.color = GetColor(color_list[_index]);
		index = (_index+1) % color_list.Count;
		imgSub.color = GetColor(color_list[index]);

		iTween.ValueTo(gameObject,
			iTween.Hash(
				"time", 6.0f,
				"from", 0.0f,
				"to", 1.0f,
				"onupdate", "ChangeColorSlide",
				"oncomplete", "OnSlideWaitCompleteChangeColor",
				"oncompleteparams", index
		)
		);
	}
	public void ChangeColorSlide( float _fvalue)
	{
		imgSub.transform.localScale = new Vector3(_fvalue, 1.0f, 1.0f);
	}
	public void dummy(float _fValue)
	{

	}

	#endregion

	private Color GetColor(DataColorParam _color)
	{
		return new Color(_color.red / 255.0f, _color.green / 255.0f, _color.blue / 255.0f);
	}




}
