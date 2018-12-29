using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//! 시작 씬
public class CStartScene : CSceneManager
{
	//! 시작 버튼을 클릭한다
	public void OnTouchStartButton()
	{
        SceneManager.LoadScene("GameScene");
    }

}
