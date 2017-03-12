using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettingTop : CPanel {
	[SerializeField]
	public RectTransform rtAreaButton;

	protected override void panelStart()
	{
		base.panelStart();

		OnChangeOrientation(DeviceOrientationDetector.Instance.orientation);
	}

	protected override void awake()
	{
		base.awake();
		DeviceOrientationDetector.Instance.OnChangeOrientation.AddListener(OnChangeOrientation);
	}

	private void OnChangeOrientation(DeviceOrientationDetector.ORIENTATION _orientaiton)
	{
		if (_orientaiton == DeviceOrientationDetector.ORIENTATION.YOKO)
		{
			/*
			CheckRectTransform(rtAreaButton, "rtAreaButton");
			rtAreaButton.pivot = new Vector2(1, 0.5f);
			rtAreaButton.anchorMin = new Vector2(1, 0);
			rtAreaButton.anchorMax = new Vector2(1, 1);
			rtAreaButton.offsetMin = new Vector2(-515.3499f, 0);
			rtAreaButton.offsetMax = new Vector2(49.35007f, 0);
			rtAreaButton.sizeDelta = new Vector2(564.7f, 0);
			*/
		}
		else
		{
			/*
			CheckRectTransform(rtAreaButton, "rtAreaButton");
			rtAreaButton.pivot = new Vector2(0.5f, 0);
			rtAreaButton.anchorMin = new Vector2(0, 0);
			rtAreaButton.anchorMax = new Vector2(1, 0);
			rtAreaButton.offsetMin = new Vector2(10, 50);
			rtAreaButton.offsetMax = new Vector2(-10, 550);
			rtAreaButton.sizeDelta = new Vector2(-20, 500);
			*/
		}



	}
}
