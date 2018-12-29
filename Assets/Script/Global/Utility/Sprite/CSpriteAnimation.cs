using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 스프라이트 애니메이션
public class CSpriteAnimation : CComponent {

	private bool _isLoop = false;
	private bool _isPlaying = false;
	private float _skipTime = 0.0f;
	private float _delayPerUnit = 0.0f;
	private int _index = 0;
	private string[] _filePaths = null;
	private CSprite _sprite = null;

	//! 초기화
	public override void Awake()
	{
		base.Awake();
		_sprite = this.GetComponent<CSprite>();
	}

	//! 상태를 갱신한다
	public override void Update()
	{
		base.Update();

		if (_isPlaying)
		{
			_skipTime += Time.deltaTime;

			if(_skipTime > _delayPerUnit)
			{
				_skipTime = 0.0f;

				if (_index < _filePaths.Length - 1)
				{
					_sprite.SetSprite(_filePaths[++_index]);
				}
				else
				{
					if (!_isLoop)
					{
						this.StopAnimation();
					}
					else
					{
						this.PlayAnimation(_filePaths, _delayPerUnit, _isLoop);
					}
				}
			}
		}
	}

	//! 애니메이션을 재생한다
	public void PlayAnimation(string[] filePath, float delayPerUnit, bool isLoop)
	{
		_filePaths = filePath;
		this.StopAnimation();
		
		_isLoop = isLoop;
		_isPlaying = true;
		_delayPerUnit = delayPerUnit;
	}

	//! 애니메이션을 정지한다
	public void StopAnimation()
	{
		_index = 0;
		_isPlaying = false;
		_skipTime = 0.0f;

		_sprite.SetSprite(_filePaths[_index]);
	}
}
