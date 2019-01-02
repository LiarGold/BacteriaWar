using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSceneManager : MonoBehaviour
{
	//! UI 카메라 프로퍼티
	public static Camera UICamera
	{
		get
		{
			return Function.FineComponent<Camera>(KDefine.NAME_UI_CAMERA);
		}
	}

	//! 메인 카메라 프로퍼티
	public static Camera MainCamera
	{
		get
		{
			return Function.FineComponent<Camera>(KDefine.NAME_MAIN_CAMERA);
		}
	}

	//! 씬 메니저 프로퍼티
	public static CSceneManager CurrentSceneManager
	{
		get
		{
			return Function.FineComponent<CSceneManager>(KDefine.NAME_SCENE_MANAGER);
		}
	}

	//! UI 루트 프로퍼티
	public static GameObject UIRoot
	{
		get
		{
			return GameObject.Find(KDefine.NAME_UI_ROOT);
		}
	}

	//! 게임 객체 프로퍼티
	public static GameObject ObjectRoot
	{
		get
		{
			return GameObject.Find(KDefine.NAME_OBJECT_ROOT);
		}
	}


	//! 초기화
	public void Awake()
    {
		// 카메라를 설정한다
        this.SetupUICamera();
        this.SetupMainCamera();

		// 싱글톤 객체를 생성한다
		CResourceManager.Create();

        // 해상도를 변경한다
        Screen.SetResolution(KDefine.SCREEN_WIDTH, KDefine.SCREEN_HEIGHT, false);
    }

	//! UI 카메라를 설정한다
	protected virtual void SetupUICamera()
    {
		if (CSceneManager.UICamera != null)
		{
			var uiCamera = CSceneManager.UICamera;
			uiCamera.orthographic = true;
			uiCamera.orthographicSize = (KDefine.SCREEN_HEIGHT / 2.0f) * KDefine.UNIT_SCALE;
		}
    }

	//! 메인 카메라를 설정한다
	protected virtual void SetupMainCamera()
    {
		if (CSceneManager.MainCamera != null)
		{
			var mainCamera = CSceneManager.MainCamera;
			mainCamera.orthographic = false;

			float distance = Mathf.Abs(mainCamera.transform.position.z);
			float height = (KDefine.SCREEN_HEIGHT / 2.0f) * KDefine.UNIT_SCALE;
			mainCamera.fieldOfView = Mathf.Atan(height / distance) * 2.0f * Mathf.Rad2Deg;
		}
	}
}
