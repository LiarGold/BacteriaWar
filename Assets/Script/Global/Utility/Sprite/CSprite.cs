using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 스프라이트
//[RequireComponent(typeof(SpriteRenderer))]
public class CSprite : MonoBehaviour {

	private SpriteRenderer _spriteRenderer = null;

	//! 초기화
	public void Awake()
	{
		_spriteRenderer = Function.AddComponent<SpriteRenderer>(this.gameObject);
	}

	//! 스프라이트를 변경한다
	public void SetSprite(string filePath)
	{
		var sprite = CResourceManager.Instance.GetSpriteForKey(filePath);
		_spriteRenderer.sprite = sprite;
	}
}
