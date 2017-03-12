using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPatternParam : CsvDataParam
{
	public int group_id { get; set; }
	public string pattern { get; set; }

}

public class DataPattern : CsvData<DataPatternParam> {

}
