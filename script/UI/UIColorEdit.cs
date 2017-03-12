using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColorEdit : CPanel {
	[SerializeField]
	private Image imgSampleColor;

	[SerializeField]
	private Text textDecide;

	DataColorParam editParam;

	[SerializeField]
	private Slider slRed;
	[SerializeField]
	private Slider slGreen;
	[SerializeField]
	private Slider slBlue;


	public float fColorRed;
	public float fColorGreen;
	public float fColorBlue;
	private void SetColor()
	{
		imgSampleColor.color = new Color(fColorRed/255.0f, fColorGreen / 255.0f, fColorBlue / 255.0f);
		slRed.value = fColorRed;
		slGreen.value = fColorGreen;
		slBlue.value = fColorBlue;
	}

	public void OnChange()
	{
		int check_group_id = 1;

		if (DataManager.Instance.IsModeEdit)
		{
			foreach (DataColorParam param in DataManager.Instance.color.list)
			{
				if( param.group_id == check_group_id && param.index == editParam.index)
				{
					param.red = (int)fColorRed;
					param.green = (int)fColorGreen;
					param.blue = (int)fColorBlue;
				}
			}
		}
		else
		{
			int iAddIndex = 0;
			foreach( DataColorParam param in DataManager.Instance.color.list)
			{
				if( param.group_id == check_group_id)
				{
					iAddIndex += 1;
				}
			}
			DataColorParam add = new DataColorParam();
			add.group_id = check_group_id;
			add.index = iAddIndex;
			add.red = (int)fColorRed;
			add.green = (int)fColorGreen;
			add.blue = (int)fColorBlue;
			DataManager.Instance.color.list.Add(add);
		}
		DataManager.Instance.SaveColor();
		UIAssistant.main.ShowPreviousPage();
	}

	protected override void panelStart()
	{
		base.panelStart();

		if( DataManager.Instance.IsModeEdit)
		{
			editParam = DataManager.Instance.editColorParam;
			fColorRed = editParam.red;
			fColorGreen = editParam.green;
			fColorBlue = editParam.blue;

			TutorialDialog.Instance.Add("edit1");
			textDecide.text = "Change";
		}
		else
		{
			fColorRed = 255.0f;
			fColorGreen = 130.0f;
			fColorBlue = 130.0f;
			textDecide.text = "Add";
			TutorialDialog.Instance.Add("add1");
		}
		SetColor();
		TutorialDialog.Instance.Add("color1");
		TutorialDialog.Instance.TutorialStart();
	}

	public void OnChangeValueRed(float _fValue)
	{
		fColorRed = _fValue;
		SetColor();
	}
	public void OnChangeValueGreen(float _fValue)
	{
		fColorGreen = _fValue;
		SetColor();
	}
	public void OnChangeValueBlue(float _fValue)
	{
		fColorBlue = _fValue;
		SetColor();
	}

}
