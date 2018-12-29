using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//! 사운드 관리자
public class CSoundManager : CSingleton<CSoundManager> {

	private bool _effectMute = false;
	private bool _backGroundMute = false;

	private float _effectVolume = 1.0f;
	private float _backGroundVolume = 1.0f;

	private CSound _backGroundSound = null;
	private Dictionary<string, List<CSound>> _effectSoundList = null;

	//! 배경음 볼륨 프로퍼티
	public float BackGroundVolume
	{
		get
		{
			return _backGroundSound.Volume;
		}
		set
		{
			_backGroundVolume = value;
			_backGroundSound.Volume = value;
		}
	}

	//! 배경음 음소거 프로퍼티
	public bool BackGroundMute
	{
		get
		{
			return _backGroundMute;
		}
		set
		{
			_backGroundMute = value;
			_backGroundSound.Mute = value;
		}
	}

	//! 효과음 볼륨 프로퍼티
	public float EffectVolume
	{
		get
		{
			return _effectVolume;
		}
		set
		{
			_effectVolume = value;

			this.EnumerateEffectSoundList((sound) =>
			{
				sound.Volume = value;
			});
		}
	}

	//! 효과음 음소거 프로퍼티
	public bool EffectMute
	{
		get
		{
			return _effectMute;
		}
		set
		{
			_effectMute = value;

			this.EnumerateEffectSoundList((sound) =>
			{
				sound.Mute = value;
			});
		}
	}

	//! 초기화
	public override void Awake()
	{
		base.Awake();

		_effectSoundList = new Dictionary<string, List<CSound>>();
		_backGroundSound = Function.CreateGameObject<CSound>("BackGroundSound", this.gameObject);
	}

	//! 효과음을 재생한다
	public void PlayOneShotSound(string filePath, Vector3 pos)
	{
		var audioClip = CResourceManager.Instance.GetAudioClipForKey(filePath);
		AudioSource.PlayClipAtPoint(audioClip, pos);
	}

	//! 효과음을 재생한다
	public void PlayEffectSound(string filePath, bool isLoop = false, bool is3D = false)
	{
		var sound = this.FindPlayableEffectSound(filePath);

		if(sound != null)
		{
			this.EffectVolume = _effectVolume;
			sound.PlaySound(filePath, isLoop, is3D);
		}
	}

	//! 효과음을 정지한다
	public void PauseEffectSound()
	{
		this.EnumerateEffectSoundList((sound) =>
		{
			sound.PauseSound();
		});
	}

	//! 효과음을 중지한다
	public void StopEffectSound()
	{
		this.EnumerateEffectSoundList((sound) =>
		{
			sound.StopSound();
		});
	}

	//! 배경음을 재생한다
	public void PlayBackGroundSound(string filePath, bool isLoop = true)
	{
		this.BackGroundVolume = _backGroundVolume;
		_backGroundSound.PlaySound(filePath, isLoop, false);
	}

	//! 배경음을 정지한다
	public void PauseBackGroundSound()
	{
		_backGroundSound.PauseSound();
	}

	//! 배경음을 중지한다
	public void StopBackGroundSound()
	{
		_backGroundSound.StopSound();
	}

	//! 효과음 리스트를 순회한다
	private void EnumerateEffectSoundList(System.Action<CSound> callBack)
	{
		var keyList = _effectSoundList.Keys.ToList();

		for(int i = 0; i < keyList.Count; ++i)
		{
			string key = keyList[i];
			var soundList = _effectSoundList[key];

			for(int j = 0; j < soundList.Count; ++j)
			{
				var sound = soundList[j];
				callBack(sound);
			}
		}
	}

	//! 재생 가능한 효과음을 탐색한다
	private CSound FindPlayableEffectSound(string filePath)
	{
		if (!_effectSoundList.ContainsKey(filePath))
		{
			var tempSoundList = new List<CSound>();
			_effectSoundList.Add(filePath, tempSoundList);
		}

		var soundList = _effectSoundList[filePath];

		// 최대 중첩 횟수를 벗어나지 않았을 경우
		if(soundList.Count < KDefine.MAX_NUM_DUPLICATE_EFFECT_SOUND)
		{
			var sound = Function.CreateGameObject<CSound>("EffectSound", this.gameObject);
			soundList.Add(sound);

			return sound;
		}
		else
		{
			for(int i = 0; i < soundList.Count; ++i)
			{
				var sound = soundList[i];

				if (!sound.IsPlaying)
				{
					return sound;
				}
			}
		}

		return null;
	}

	////! 효과음을 재생한다
	//public void PlayEffectSound(string filePath, bool isLoop = false)
	//{
	//	if(!_effectSoundList.ContainsKey(filePath))
	//	{
	//		var firstEffectSound = Function.CreateGameObject<CSound>(filePath, this.gameObject);
	//		var firstEffectSoundList = new List<CSound>();
	//		firstEffectSoundList.Add(firstEffectSound);
	//		_effectSoundList.Add(filePath, firstEffectSoundList);
	//		firstEffectSound.Volume = _effectVolume;
	//		firstEffectSound.PlaySound(filePath, isLoop);
	//		Function.ShowLog("{0} : 배열 생성 후 재생", filePath);
	//	}
	//	else
	//	{
	//		for (int i = 0; i < _effectSoundList[filePath].Count; ++i)
	//		{
	//			if (!_effectSoundList[filePath][i].IsPlaying)
	//			{
	//				_effectSoundList[filePath][i].Volume = _effectVolume;
	//				_effectSoundList[filePath][i].PlaySound(filePath, isLoop);
	//				Function.ShowLog("{0} : 기존 배열 재생", filePath);
	//				return;
	//			}
	//		}

	//		var effectSound = Function.CreateGameObject<CSound>(filePath, this.gameObject);
	//		_effectSoundList[filePath].Add(effectSound);
	//		effectSound.Volume = _effectVolume;
	//		effectSound.PlaySound(filePath, isLoop);
	//		Function.ShowLog("{0} : 배열 추가 후 재생", filePath);
	//	}
	//}

}
