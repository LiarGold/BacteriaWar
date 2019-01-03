using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CScoreUI : MonoBehaviour
{
	public Text _blueScoreText = null;
	public Text _redScoreText = null;

	//! 상태를 갱신한다
	public void UpdateState()
	{
		_blueScoreText.text = string.Format("{0:D2}", CGameDataStorage.Instance.BlueGerms);
		_redScoreText.text = string.Format("{0:D2}", CGameDataStorage.Instance.RedGerms);
	}
}
