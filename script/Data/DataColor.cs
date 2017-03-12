using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataColorParam : CsvDataParam
{
	public int group_id { get; set; }
	public int index { get; set; }
	public int red { get; set; }
	public int green { get; set; }
	public int blue { get; set; }
}

public class DataColor : CsvData<DataColorParam> {
	static void Swap(DataColorParam a ,DataColorParam b)
	{
	}
}
