using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCell : MonoBehaviour
{
	public int[] CellIndex { get; set; }
	public GameObject _redGerm = null;
	public GameObject _blueGerm = null;
	public EGermColor CellStatus { get; set; }

	//! 초기화
	public void Awake()
	{
		CellIndex = new int[2];
		CellStatus = EGermColor.NONE;
	}

	//! 상태를 갱신한다
	public void Update()
	{

	}

	//! 칸을 선택한다
	public CCell OnClickCell()
	{
		return this;
	}

	//! 세균 색깔을 바꾼다
	public void ChangeGermColor(EGermColor color)
	{
		switch (color)
		{
			case EGermColor.RED:
				{
					_blueGerm.SetActive(false);
					_redGerm.SetActive(true);
				}
				break;
			case EGermColor.BLUE:
				{
					_blueGerm.SetActive(true);
					_redGerm.SetActive(false);
				}
				break;
			case EGermColor.NONE:
				{
					_blueGerm.SetActive(false);
					_redGerm.SetActive(false);
				}
				break;
			default:
				break;
		}
	}

}
