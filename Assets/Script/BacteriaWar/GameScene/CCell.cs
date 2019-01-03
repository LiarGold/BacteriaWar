using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCell : MonoBehaviour
{
	public int CellIndexX { get; set; }
	public int CellIndexY { get; set; }
	public GameObject _redGerm = null;
	public GameObject _blueGerm = null;
	public bool _isClick = false;
	public bool _isDead = false;
	public EGermColor CellStatus = EGermColor.NONE;

	//! 초기화
	public void Awake()
	{
		CellStatus = EGermColor.NONE;
	}

	//! 칸을 선택한다
	public CCell OnClickCell()
	{
		_isClick = true;
		return this;
	}

	//! 칸 상태 업데이트
	public void UpdateState()
	{
		if (_isClick)
		{
			_redGerm.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
			_blueGerm.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
		}
		else
		{
			_redGerm.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
			_blueGerm.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
		}
	}

	//! 세균 색깔을 바꾼다
	public void ChangeGermColor(EGermColor color)
	{
		CellStatus = color;

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

	public void DeadCheck()
	{
		int emptyCell = 0;

		for (int i = -2; i < 3; ++i)
		{
			for(int j = -2; j < 3; ++j)
			{
				if (CellIndexX + j < 0 || CellIndexX + j > 6 ||
					CellIndexY + i < 0 || CellIndexY + i > 6)
				{
					continue;
				}
				else
				{
					var tempCell = CGameDataStorage.Instance.CellObjectList[CellIndexX + j, CellIndexY + i].GetComponent<CCell>();

					if (tempCell.CellStatus == EGermColor.NONE)
					{						
						emptyCell += 1;
					}
				}
			}
		}

		if(emptyCell <= 0)
		{
			_isDead = true;
		}
		else
		{
			_isDead = false;
		}

	}
}
