using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CGermUI : MonoBehaviour
{
	public Image[] _blueUIImg = null;
	public Image[] _redUIImg = null;

    //! 상태를 갱신한다
	public void UpdateState()
	{
		for(int i = 0; i < 3; ++i)
		{
			_blueUIImg[i].gameObject.SetActive(false);
			_redUIImg[i].gameObject.SetActive(false);
		}

		if (CGameDataStorage.Instance.BlueGerms < CGameDataStorage.Instance.RedGerms)
		{
			_blueUIImg[1].gameObject.SetActive(true);
			_redUIImg[2].gameObject.SetActive(true);
		}
		else if (CGameDataStorage.Instance.BlueGerms > CGameDataStorage.Instance.RedGerms)
		{
			_blueUIImg[2].gameObject.SetActive(true);
			_redUIImg[1].gameObject.SetActive(true);
		}
		else
		{
			_blueUIImg[0].gameObject.SetActive(true);
			_redUIImg[0].gameObject.SetActive(true);
		}
	}
}
