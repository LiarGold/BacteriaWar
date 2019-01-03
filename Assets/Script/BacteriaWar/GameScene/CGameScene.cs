using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//! 게임 씬
public class CGameScene : CSceneManager
{
	public void GameOver()
	{
		SceneManager.LoadScene("ResultScene", LoadSceneMode.Additive);
	}
}
