using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 시작 씬
public class CStartScene : CSceneManager
{
	//! 초기화
	public override void Awake()
	{
		base.Awake();
	}

	//! 상태를 갱신한다
	public override void Update()
	{
		base.Update();
	}

	//! 시작 버튼을 클릭한다
	public void OnTouchStartButton()
	{
		CSceneChanger.Instance.ChangeScene(ESceneIndex.GAME_SCENE, null);
	}

}
