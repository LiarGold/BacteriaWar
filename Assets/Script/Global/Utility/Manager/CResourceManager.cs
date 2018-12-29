using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 리소스 관리자
public class CResourceManager : CSingleton<CResourceManager>
{
	private Dictionary<string, Shader> _shaderList = null;
	private Dictionary<string, Sprite> _spriteList = null;
	private Dictionary<string, Texture> _textureList = null;
	private Dictionary<string, Material> _materialList = null;
	private Dictionary<string, AudioClip> _audioClipList = null;
	private Dictionary<string, GameObject> _gameObjectList = null;
	private Dictionary<string, RuntimeAnimatorController> _runTimeAnimatorControllerList = null;

	//! 초기화
	public void Awake()
	{
		_shaderList = new Dictionary<string, Shader>();
		_spriteList = new Dictionary<string, Sprite>();
		_textureList = new Dictionary<string, Texture>();
		_materialList = new Dictionary<string, Material>();
		_audioClipList = new Dictionary<string, AudioClip>();
		_gameObjectList = new Dictionary<string, GameObject>();
		_runTimeAnimatorControllerList = new Dictionary<string, RuntimeAnimatorController>();
	}

	//! 쉐이더를 반환한다
	public Shader GetShaderForKey(string filePath, bool isAutoCreate = true)
	{
		if(isAutoCreate && !_shaderList.ContainsKey(filePath))
		{
			var shader = Shader.Find(filePath);
			_shaderList.Add(filePath, shader);
		}

		return _shaderList[filePath];
	}

	//! 스프라이트를 반환한다
	public Sprite GetSpriteForKey(string filePath, bool isAutoCreate = true)
	{
		if (isAutoCreate && !_spriteList.ContainsKey(filePath))
		{
			var sprite = Resources.Load<Sprite>(filePath);
			_spriteList.Add(filePath, sprite);
		}

		return _spriteList[filePath];
	}

	//! 텍스처를 반환한다
	public Texture GetTextureForKey(string filePath, bool isAutoCreate = true)
	{
		if (isAutoCreate && !_textureList.ContainsKey(filePath))
		{
			var texture = Resources.Load<Texture>(filePath);
			_textureList.Add(filePath, texture);
		}

		return _textureList[filePath];
	}

	//! 재질을 반환한다
	public Material GetMaterialForKey(string filePath, bool isAutoCreate = true)
	{
		if (isAutoCreate && !_materialList.ContainsKey(filePath))
		{
			var material = Resources.Load<Material>(filePath);
			_materialList.Add(filePath, material);
		}

		return _materialList[filePath];
	}

	//! 사본 재질을 반환한다
	public Material GetCopiedMaterialForKey(string filePath, bool isAutoCreate = true)
	{
		var material = this.GetMaterialForKey(filePath, isAutoCreate);
		return new Material(material);
	}

	//! 오디오 클립을 반환한다
	public AudioClip GetAudioClipForKey(string filePath, bool isAutoCreate = true)
	{
		if(isAutoCreate &&
			!_audioClipList.ContainsKey(filePath))
		{
			var audioClip = Resources.Load<AudioClip>(filePath);
			_audioClipList.Add(filePath, audioClip);
		}

		return _audioClipList[filePath];
	}

	//! 게임 객체를 반환한다
	public GameObject GetObjectForKey(string filePath, bool isAutoCreate = true)
	{
		if (isAutoCreate && !_gameObjectList.ContainsKey(filePath))
		{
			var gameObject = Resources.Load<GameObject>(filePath);
			_gameObjectList.Add(filePath, gameObject);
		}

		return _gameObjectList[filePath];
	}

	//! 애니메이터 컨트롤러를 반환한다
	public RuntimeAnimatorController GetRunTimeAnimatorControllerForKey(string filePath, bool isAutoCreate = true)
	{
		if (isAutoCreate && !_runTimeAnimatorControllerList.ContainsKey(filePath))
		{
			var runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(filePath);
			_runTimeAnimatorControllerList.Add(filePath, runtimeAnimatorController);
		}

		return _runTimeAnimatorControllerList[filePath];
	}

}
