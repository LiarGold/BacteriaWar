using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLocalizeManager : CSingleton<CLocalizeManager> {

	private Dictionary<string, string> _stringList = null;

	//! 초기화
	public override void Awake()
	{
		base.Awake();
		_stringList = new Dictionary<string, string>();
	}

	//! 문자열을 반환한다
	public string GetStringForKey(string key)
	{
		if (_stringList.ContainsKey(key))
		{
			return _stringList[key];
		}

		return key;
	}

	//! 문자열 리스트를 제거한다
	public void ResetStringList()
	{
		_stringList.Clear();
	}

	//! 문자열 리스트를 불러온다
	public void LoadStringListFromFile(string filePath)
	{
		var stringPairList = CSVParser.ParseFromResource(filePath);
		this.ResetStringList();

		for(int i = 0; i < stringPairList.Count; ++i)
		{
			var stringPair = stringPairList[i];

			string key = stringPair["Key"];
			string tempString = stringPair["Value"];

			_stringList.Add(key, tempString);
		}
	}
}
