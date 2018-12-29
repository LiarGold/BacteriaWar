using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 충돌 이벤트 디스패처
public class CCollisionDispatcher : UnityEngine.MonoBehaviour {

	public System.Action<GameObject, Collision> CollisionEnter { get; set; }
	public System.Action<GameObject, Collision> CollisionStay { get; set; }
	public System.Action<GameObject, Collision> CollisionExit { get; set; }

	//! 충돌이 발생했을 경우
	public void OnCollisionEnter(Collision collision)
	{
		if(this.CollisionEnter != null)
		{
			this.CollisionEnter(this.gameObject, collision);
		}
	}

	//! 충돌 중일 경우
	public void OnCollisionStay(Collision collision)
	{
		if (this.CollisionStay != null)
		{
			this.CollisionStay(this.gameObject, collision);
		}
	}

	//! 충돌이 끝났을 경우
	public void OnCollisionExit(Collision collision)
	{
		if (this.CollisionExit != null)
		{
			this.CollisionExit(this.gameObject, collision);
		}
	}
}
