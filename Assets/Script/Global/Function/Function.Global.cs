using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 전역 함수
public static partial class Function
{
    //! 로그를 출력한다
    public static void ShowLog(string format, params object[] tempParams)
    {
        string log = string.Format(format, tempParams);
        Debug.LogFormat(log);
    }

	//! 함수를 지연 호출한다
	public static void LateCall(System.Action<object[]> callBack, float delay, params object[] tempParams)
	{
		var sceneManager = CSceneManager.CurrentSceneManager;
		sceneManager.StartCoroutine(DoLateCall(callBack, delay, tempParams));
	}

	//! 비동기 연산을 대기한다
	public static IEnumerator WaitAsyncOperation(AsyncOperation asyncOperation,
		System.Action<AsyncOperation, bool> callBack)
	{
		asyncOperation.completed += (sender) =>
		{
			if (callBack != null)
			{
				callBack(sender, true);
			}
		};

		while (!asyncOperation.isDone)
		{
			yield return new WaitForEndOfFrame();

			if(callBack != null)
			{
				callBack(asyncOperation, false);
			}
		}
	}

	//! 컴포넌트를 추가한다
	public static T AddComponent<T>(GameObject gameObject) where T : Component
	{
		var component = gameObject.GetComponent<T>();

		if(component == null)
		{
			component = gameObject.AddComponent<T>();
		}

		return component;
	}

	//! 컴포넌트를 탐색한다
	public static T FineComponent<T>(string name) where T : Component
	{
		var gameObject = GameObject.Find(name);

		if(gameObject == null)
		{
			return null;
		}

		return gameObject.GetComponentInChildren<T>();
	}

	//! 게임 객체를 생성한다
	public static GameObject CreateGameObject(string name,
		GameObject parent,
		bool isWorldStay = false,
		bool isResetTransform = false)
	{
		var gameObject = new GameObject(name);

		if(parent != null)
		{
			gameObject.transform.SetParent(parent.transform, isWorldStay);
		}

		if (isResetTransform)
		{
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localEulerAngles = Vector3.zero;
			gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		}

		return gameObject;
	}

	//! 사본 게임 객체를 생성한다
	public static GameObject CreateCopiedGameObject(string name,
		GameObject origin,
		GameObject parent,
		bool isWorldStay = false,
		bool isResetTransform = false)
	{
		var gameObject = Object.Instantiate(origin,
											Vector3.zero,
											Quaternion.identity);

		if(parent != null)
		{
			gameObject.transform.SetParent(parent.transform, isWorldStay);

			if (!isResetTransform)
			{
				gameObject.transform.localPosition = origin.transform.localPosition;
				gameObject.transform.localEulerAngles = origin.transform.localEulerAngles;
			}
		}

		if (isResetTransform)
		{
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localEulerAngles = Vector3.zero;
			gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		}

		gameObject.name = name;

		return gameObject;
	}

	//! 사본 게임 객체를 생성한다
	public static GameObject CreateCopiedGameObject(string name,
		string filePath,
		GameObject parent,
		bool isWorldStay = false,
		bool isResetTransform = false)
	{
		var originObject = CResourceManager.Instance.GetObjectForKey(filePath);
		return CreateCopiedGameObject(name, originObject, parent);
	}

	//! 게임 객체를 생성한다
	public static T CreateGameObject<T>(string name,
		GameObject parent,
		bool isWorldStay = false,
		bool isResetTransform = false) where T : Component
	{
		var gameObject = CreateGameObject(name, parent, isWorldStay, isResetTransform);

		return AddComponent<T>(gameObject);
	}

	//! 사본 게임 객체를 생성한다
	public static T CreateCopiedGameObject<T>(string name,
		GameObject origin,
		GameObject parent,
		bool isWorldStay = false,
		bool isResetTransform = false) where T : Component
	{
		var gameObject = CreateCopiedGameObject(name,
			origin,
			parent,
			isWorldStay,
			isResetTransform);

		return gameObject.GetComponentInChildren<T>();
	}

	//! 사본 게임 객체를 생성한다
	public static T CreateCopiedGameObject<T>(string name,
		string filePath,
		GameObject parent,
		bool isWorldStay = false,
		bool isResetTransform = false) where T : Component
	{
		var gameObject = CreateCopiedGameObject(name,
			filePath,
			parent,
			isWorldStay,
			isResetTransform);

		return gameObject.GetComponentInChildren<T>();
	}

	//! 함수를 지연 호출한다
	private static IEnumerator DoLateCall(System.Action<object[]> callBack, float delay, params object[] tempParams)
	{
		yield return new WaitForSeconds(delay);
		callBack(tempParams);
	}
}