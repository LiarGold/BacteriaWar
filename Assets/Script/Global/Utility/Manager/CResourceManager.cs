using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 리소스 관리자
public class CResourceManager : CSingleton<CResourceManager>
{
	private Dictionary<string, Sprite> _spriteList = null;
	private Dictionary<string, Texture> _textureList = null;
	private Dictionary<string, Material> _materialList = null;
	private Dictionary<string, GameObject> _gameObjectList = null;

	//! 초기화
	public void Awake()
	{
		_spriteList = new Dictionary<string, Sprite>();
		_textureList = new Dictionary<string, Texture>();
		_materialList = new Dictionary<string, Material>();
		_gameObjectList = new Dictionary<string, GameObject>();
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

}
