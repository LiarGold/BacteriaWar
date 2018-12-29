using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 이벤트 디스패처
public class CEventDispatcher : UnityEngine.MonoBehaviour
{

	public System.Action<GameObject, string> EventHandler { get; set; }

	//! 이벤트를 수신했을 경우
	public void OnReceiveEnvent(string eventName)
	{
		if(this.EventHandler != null)
		{
			this.EventHandler(this.gameObject, eventName);
		}
	}
}
