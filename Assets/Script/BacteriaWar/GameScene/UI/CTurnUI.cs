using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTurnUI : MonoBehaviour
{
	public Image[] _turnImage;

	//! 턴 전환
	public void UpdateState()
	{
		if(CGameDataStorage.Instance.CurrentTurn == EGermColor.BLUE)
		{
			_turnImage[0].gameObject.SetActive(false);
			_turnImage[1].gameObject.SetActive(false);
		}
		else
		{
			_turnImage[0].gameObject.SetActive(true);
			_turnImage[1].gameObject.SetActive(true);
		}
	}
}
