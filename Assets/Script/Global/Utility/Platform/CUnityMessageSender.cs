using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 유니티 메세지 전송자
public class CUnityMessageSender : CSingleton<CUnityMessageSender> {

#if !UNITY_EDITOR && UNITY_ANDROID
	private AndroidJavaObject _javaObject = null;

#endif

	//! 초기화
	public override void Awake()
	{
		base.Awake();
#if !UNITY_EDITOR && UNITY_ANDROID
		var javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		_javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity");

#endif
	}

	//! 메세지를 전송한다
	public void SendMessage(string command,
		string message,
		System.Action<string, string> callBack = null,
		bool isAutoRemove = true)
	{
		if(callBack != null)
		{
			var callBackInfo = new KeyValuePair<bool, System.Action<string, string>>(isAutoRemove, callBack);
			CDeviceMessageReceiver.Instance.AddCallBackInfo(command, callBackInfo);
		}

#if !UNITY_EDITOR && UNITY_ANDROID
		_javaObject.Call("handleUnityMessage", command, message);
#endif
	}
}
