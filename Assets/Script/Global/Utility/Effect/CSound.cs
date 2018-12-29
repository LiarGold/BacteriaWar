using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 사운드
public class CSound : CComponent {

	private AudioSource _audioSource = null;

	//! 볼륨
	public float Volume
	{
		get
		{
			return _audioSource.volume;
		}
		set
		{
			_audioSource.volume = value;
		}
	}

	//! 음소거 프로퍼티
	public bool Mute
	{
		get
		{
			return _audioSource.mute;
		}
		set
		{
			_audioSource.mute = value;
		}
	}

	//! 재생 여부 프로퍼티
	public bool IsPlaying
	{
		get
		{
			return _audioSource.isPlaying;
		}
	}

	//! 초기화
	public override void Awake()
	{
		base.Awake();
		_audioSource = Function.AddComponent<AudioSource>(this.gameObject);
		_audioSource.playOnAwake = false;
	}

	//! 사운드를 재생한다
	public void PlaySound(string filePath, bool isLoop, bool is3D)
	{
		_audioSource.clip = CResourceManager.Instance.GetAudioClipForKey(filePath);
		_audioSource.loop = isLoop;
		_audioSource.spatialBlend = is3D ? 1.0f : 0.0f;
		_audioSource.Play();
	}

	//! 사운드를 정지한다
	public void PauseSound()
	{
		_audioSource.Pause();
	}

	//! 사운드를 중지한다
	public void StopSound()
	{
		_audioSource.Stop();
	}
}
