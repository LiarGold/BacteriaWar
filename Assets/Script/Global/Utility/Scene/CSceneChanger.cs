using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSceneChanger : CSingleton<CSceneChanger> {
	//! 씬을 제거한다
	public void RemoveScene(ESceneIndex sceneIndex, System.Action<AsyncOperation, bool> callBack)
	{
		var scenePath = SceneUtility.GetScenePathByBuildIndex((int)sceneIndex);
		this.RemoveScene(scenePath, callBack);
	}

	//! 씬을 제거한다
	public void RemoveScene(string sceneName, System.Action<AsyncOperation, bool> callBack)
	{
		var sceneManager = CSceneManager.CurrentSceneManager;
		sceneManager.StartCoroutine(this.DoRemoveScene(sceneName, callBack));
	}

	//! 씬을 전환한다
	public void ChangeScene(ESceneIndex sceneIndex,
		System.Action<AsyncOperation, bool> callBack,
		float delay = 0.0f,
		LoadSceneMode loadSceneMode = LoadSceneMode.Single)
	{
		var scenePath = SceneUtility.GetScenePathByBuildIndex((int)sceneIndex);
		this.ChangeScene(scenePath, callBack, delay, loadSceneMode);
	}

	//! 씬을 전환한다
	public void ChangeScene(string sceneName,
		System.Action<AsyncOperation, bool> callBack,
		float delay = 0.0f,
		LoadSceneMode loadSceneMode = LoadSceneMode.Single)
	{
		var sceneManager = CSceneManager.CurrentSceneManager;
		sceneManager.StartCoroutine(this.DoChangeScene(
			sceneName,
			callBack,
			delay,
			loadSceneMode));
	}

	//! 씬을 제거한다
	private IEnumerator DoRemoveScene(string sceneName, System.Action<AsyncOperation, bool> callBack)
	{
		var asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
		yield return Function.WaitAsyncOperation(asyncOperation, callBack);
	}

	//! 씬을 전환한다
	private IEnumerator DoChangeScene(string sceneName,
		System.Action<AsyncOperation, bool> callBack,
		float delay,
		LoadSceneMode loadSceneMode)
	{
		yield return new WaitForSeconds(delay);
		var asyncOperation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
		yield return Function.WaitAsyncOperation(asyncOperation, callBack);
	}
}
