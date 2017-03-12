using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : DataManagerBase<DataManager> {

	public DataKvs userdata = new DataKvs();
	public DataKvs tutorial_message = new DataKvs();
	public DataColor color = new DataColor();
	public DataPattern pattern = new DataPattern();

	public bool IsModeEdit;
	public DataColorParam editColorParam;

	public readonly string FILENAME_PATTERN = "data/pattern";
	public readonly string FILENAME_COLOR = "data/color";
	public readonly string FILENAME_USERDATA = "data/userdata";

	public int sysmtem_timeout;

	public override void Initialize()
	{
		sysmtem_timeout = SleepTimeout.SystemSetting;
		base.Initialize();
		IsModeEdit = false;
		editColorParam = null;

		pattern.SetSaveFilename(FILENAME_PATTERN);
		if (false == pattern.LoadMulti(FILENAME_PATTERN))
		{
			pattern.LoadMulti("Data/default_pattern");
			pattern.Save();
		}
		color.SetSaveFilename(FILENAME_COLOR);
		if (false == color.LoadMulti(FILENAME_COLOR))
		{
			Debug.LogError("use resources color");
			color.LoadMulti("Data/default_color");
			color.Save();
		}

		userdata.SetSaveFilename(FILENAME_USERDATA);
		if( false == userdata.Load(FILENAME_USERDATA))
		{
			userdata.Save();
			userdata.Load(FILENAME_USERDATA);
		}
		tutorial_message.LoadResources("Data/tutorial_message");

	}

	public void Save()
	{
		pattern.Save();
		SaveColor();
	}
	public void SaveColor()
	{
		color.Save();

	}

	public void Reload()
	{
		pattern.LoadMulti(FILENAME_PATTERN);
		color.LoadMulti(FILENAME_COLOR);
	}

	void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			Debug.LogError("datamanager OnApplicationPause false");
			Screen.sleepTimeout = SleepTimeout.SystemSetting;
		}
		else
		{
		}

	}


}
