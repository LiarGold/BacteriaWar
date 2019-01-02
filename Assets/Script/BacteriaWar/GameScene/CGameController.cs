using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameController : CSingleton<CGameController> {
	#region public 변수
	public EGamePhase _gamePhase = EGamePhase.NONE;
	public GameObject _originCell = null;
	#endregion

	private CInputManagerBase _inputManager = null;
	private GameObject[,] _cellObjectList = null;

	public void Awake()
	{
		_gamePhase = EGamePhase.SELECT;
		_cellObjectList = new GameObject[7, 7];
		_inputManager = new C1PInputManager();

		for (int i = 0; i < 7; ++i)
		{
			for (int j = 0; j < 7; ++j)
			{
				var cell = Function.CreateCopiedGameObject("Cell:" + i.ToString() + "X" + j.ToString(),
					_originCell,
					CSceneManager.ObjectRoot);

				cell.transform.localPosition = new Vector3(-410.0f + (137.0f * j), 180.0f - (76.0f * i), 0.0f);

				var cellClass = cell.GetComponent<CCell>();
				cellClass.CellIndex[0] = i;
				cellClass.CellIndex[1] = j;

				if(i == 0 && j == 0 || i == 6 && j == 6)
				{
					cellClass.ChangeGermColor(EGermColor.RED);
				}
				else if(i == 0 && j == 6 || i == 6 && j == 0)
				{
					cellClass.ChangeGermColor(EGermColor.BLUE);
				}

				_cellObjectList[i, j] = cell;
			}
		}
	}

	public void Update()
	{
		switch (_gamePhase)
		{
			case EGamePhase.SELECT:
				{
					if (Input.GetMouseButtonDown((int)EMouseButton.LEFT))
					{
						if(_inputManager.GetSelectCell() != null)
						{
							_gamePhase = EGamePhase.MOVE;
						}
						else
						{
							Function.ShowLog("Clicked EmptyCell");
						}
					}
				}
				break;
			case EGamePhase.MOVE:
				{
					if (Input.GetMouseButtonDown((int)EMouseButton.LEFT))
					{
						var selectCell = _inputManager.GetSelectCell();
						var moveCell = _inputManager.GetMoveCell();
						InteractionCells(selectCell, moveCell);
					}
				}
				break;
			case EGamePhase.INFECT:
				{

				}
				break;
			case EGamePhase.NONE:
				{

				}
				break;
			default:
				break;
		}
	}

	private void InteractionCells(CCell selectCell, CCell moveCell)
	{
		var selectIndex = selectCell.CellIndex;
		var moveIndex = moveCell.CellIndex;

		if (selectIndex[0] - 2 > moveIndex[0] && selectIndex[0] + 2 < moveIndex[0] &&
		   selectIndex[1] - 2 > moveIndex[1] && selectIndex[1] + 2 < moveIndex[1])
		{
			return;
		}
		else
		{
			if (selectIndex[0] - 1 > moveIndex[0] && selectIndex[0] + 1 < moveIndex[0] &&
				selectIndex[1] - 1 > moveIndex[1] && selectIndex[1] + 1 < moveIndex[1])
			{
				moveCell.ChangeGermColor(selectCell.CellStatus);
			}

			moveCell.ChangeGermColor(selectCell.CellStatus);
			selectCell.ChangeGermColor(EGermColor.NONE);
		}
	}

}

