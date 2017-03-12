using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITopMenu : CPanel
{
	[SerializeField]
	public RectTransform rtAreaButton;
	[SerializeField]
	public RectTransform rtAreaIcon;

	[SerializeField]
	public LightRotation lightRotation;

	protected override void panelStart()
	{
		base.panelStart();

		OnChangeOrientation(DeviceOrientationDetector.Instance.orientation);

		TutorialDialog.Instance.Add("tutorial_top1");
		TutorialDialog.Instance.Add("tutorial_top2");
		TutorialDialog.Instance.TutorialStart();
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
			//CheckRectTransform(rtAreaButton, "rtAreaButton");
			rtAreaButton.pivot = new Vector2(1, 0.5f);
			rtAreaButton.anchorMin = new Vector2(1, 0);
			rtAreaButton.anchorMax = new Vector2(1, 1);
			rtAreaButton.offsetMin = new Vector2(-515.3499f, 0);
			rtAreaButton.offsetMax = new Vector2(49.35007f, 0);
			rtAreaButton.sizeDelta = new Vector2(564.7f, 0);
			//CheckRectTransform(rtAreaIcon, "rtAreaIcon");
			rtAreaIcon.pivot = new Vector2(0, 0.5f);
			rtAreaIcon.anchorMin = new Vector2(0, 0);
			rtAreaIcon.anchorMax = new Vector2(0, 1);
			rtAreaIcon.offsetMin = new Vector2(34, 7.999937f);
			rtAreaIcon.offsetMax = new Vector2(400, -10.00006f);
			rtAreaIcon.sizeDelta = new Vector2(366, -18);
		}
		else
		{
			//CheckRectTransform(rtAreaButton, "rtAreaButton");
			rtAreaButton.pivot = new Vector2(0.5f, 0);
			rtAreaButton.anchorMin = new Vector2(0, 0);
			rtAreaButton.anchorMax = new Vector2(1, 0);
			rtAreaButton.offsetMin = new Vector2(10, 50);
			rtAreaButton.offsetMax = new Vector2(-10, 550);
			rtAreaButton.sizeDelta = new Vector2(-20, 500);

			//CheckRectTransform(rtAreaIcon, "rtAreaIcon");
			rtAreaIcon.pivot = new Vector2(0.5f, 1);
			rtAreaIcon.anchorMin = new Vector2(0, 1);
			rtAreaIcon.anchorMax = new Vector2(1, 1);
			rtAreaIcon.offsetMin = new Vector2(9.999985f, -512);
			rtAreaIcon.offsetMax = new Vector2(-10.00002f, -12);
			rtAreaIcon.sizeDelta = new Vector2(-20, 500);
		}
	}

	public void OnLightStart()
	{
		lightRotation.LightStart(DataManager.Instance.pattern.list[0]);
		UIAssistant.main.ShowPage("Light");
	}
}
