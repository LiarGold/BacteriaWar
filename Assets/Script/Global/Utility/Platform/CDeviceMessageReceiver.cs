using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 타입 재정의 (typeDef)
using TCallBackInfo = System.Collections.Generic.KeyValuePair<bool, System.Action<string, string>>;

//! 디바이스 메세지 수신자
public class CDeviceMessageReceiver : CSingleton<CDeviceMessageReceiver> {

	private Dictionary<string, TCallBackInfo> _callBackInfoList = null;

	//! 초기화
	public override void Awake()
	{
		base.Awake();

		_callBackInfoList = new Dictionary<string, TCallBackInfo>();
	}

	//! 콜백 정보를 추가한다
	public void AddCallBackInfo(string key, TCallBackInfo callBackInfo)
	{
		if (!_callBackInfoList.ContainsKey(key))
		{
			_callBackInfoList.Add(key, callBackInfo);
		}
	}

	//! 콜백 정보를 제거한다
	public void RemoveCallBackInfo(string key)
	{
		if (_callBackInfoList.ContainsKey(key))
		{
			_callBackInfoList.Remove(key);
		}
	}

	//! 디바이스 메세지를 처리한다
	public void HandleDeviceMessage(string message)
	{
		var jsonObject = SimpleJSON.JSON.Parse(message);

		string command = jsonObject["Command"];
		string tempMessage = jsonObject["Message"];

		if (_callBackInfoList.ContainsKey(command))
		{
			var callBackInfo = _callBackInfoList[command];
			callBackInfo.Value(command, message);

			if (callBackInfo.Key)
			{
				this.RemoveCallBackInfo(command);
			}
		}
	}
}
