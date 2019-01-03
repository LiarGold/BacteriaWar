using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CResultScene : MonoBehaviour
{
	public Text _winnerText = null;

	public void Awake()
	{
		_winnerText.text = CGameDataStorage.Instance.WinnerString;
	}

	public void OnTouchRestartButton()
	{
		SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
	}
}
