using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//! 네트워크 컴포넌트
public class CNetworkComponent : NetworkBehaviour {

	[HideInInspector] public Transform _transform = null;
	[HideInInspector] public Rigidbody _rigidBody = null;

	//! 초기화
	public virtual void Awake()
	{
		_transform = this.GetComponent<Transform>();
		_rigidBody = this.GetComponent<Rigidbody>();
	}

	//! 초기화
	public virtual void Start()
	{
		// Do Nothing
	}

	//! 상태를 갱신한다
	public virtual void Update()
	{
		// Do Nothing
	}

	//! 상태를 지연 갱신한다
	public virtual void LateUpdate()
	{
		// Do Nothing
	}
}
