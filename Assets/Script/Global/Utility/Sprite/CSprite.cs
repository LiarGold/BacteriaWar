using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 스프라이트
//[RequireComponent(typeof(SpriteRenderer))]
public class CSprite : CComponent {

	private SpriteRenderer _spriteRenderer = null;
	private CSpriteAnimation _spriteAnimation = null;

	//! 초기화
	public override void Awake()
	{
		base.Awake();

		_spriteRenderer = Function.AddComponent<SpriteRenderer>(this.gameObject);
		_spriteAnimation = Function.AddComponent<CSpriteAnimation>(this.gameObject);
	}

	//! 상태를 갱신한다
	public override void Update()
	{
		base.Update();
	}

	//! 스프라이트를 변경한다
	public void SetSprite(string filePath)
	{
		var sprite = CResourceManager.Instance.GetSpriteForKey(filePath);
		_spriteRenderer.sprite = sprite;
	}

	//! 애니메이션을 재생한다
	public void PlayAnimation(string[] filePath, float delayPerUnit, bool isLoop = false)
	{
		_spriteAnimation.PlayAnimation(filePath, delayPerUnit, isLoop);
	}

	//! 애니메이션을 정지한다
	public void StopAnimation()
	{
		_spriteAnimation.StopAnimation();
	}
}
