using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameController : MonoBehaviour
{
	#region public 변수
	public GameObject _originCell;
	#endregion

	private CInputManagerBase[] _inputManagers = null;
	private CCell _selectCell = null;
	private CCell _moveCell = null;

	public void Awake()
	{
		CGameDataStorage.Instance.GamePhase = EGamePhase.SELECT;
		CGameDataStorage.Instance.CellObjectList = new GameObject[7, 7];
		_inputManagers = new CInputManagerBase[2];

		_inputManagers[0] = Function.AddComponent<C1PInputManager>(gameObject);
		_inputManagers[1] = Function.AddComponent<C2PInputManager>(gameObject);

		CGameDataStorage.Instance.CurrentTurn = EGermColor.BLUE;

		for (int i = 0; i < 7; ++i)
		{
			for (int j = 0; j < 7; ++j)
			{
				var cell = Function.CreateCopiedGameObject("Cell:" + j.ToString() + "X" + i.ToString(),
					_originCell,
					CSceneManager.ObjectRoot);

				cell.transform.localPosition = new Vector3(-255.0f + (85.0f * j), 255.0f - (85.0f * i), 0.0f);

				var cellClass = cell.GetComponent<CCell>();
				cellClass.CellIndexX = j;
				cellClass.CellIndexY = i;

				if(i == 0 && j == 0 || i == 6 && j == 6)
				{
					cellClass.CellStatus = EGermColor.BLUE;
					cellClass.ChangeGermColor(EGermColor.BLUE);
				}
				else if(i == 0 && j == 6 || i == 6 && j == 0)
				{
					cellClass.CellStatus = EGermColor.RED;
					cellClass.ChangeGermColor(EGermColor.RED);
				}

				CGameDataStorage.Instance.CellObjectList[j, i] = cell;
			}
		}
	}

	public void Update()
	{
		switch (CGameDataStorage.Instance.GamePhase)
		{
			case EGamePhase.SELECT:
				{
					if (Input.GetMouseButtonDown((int)EMouseButton.LEFT))
					{
						_selectCell = _inputManagers[(int)CGameDataStorage.Instance.CurrentTurn].GetSelectCell();

						if(_selectCell != null)
						{
							_selectCell.UpdateState();
							CGameDataStorage.Instance.GamePhase = EGamePhase.MOVE;
						}
					}
				}
				break;
			case EGamePhase.MOVE:
				{
					if (Input.GetMouseButtonDown((int)EMouseButton.LEFT))
					{
						_moveCell = _inputManagers[(int)CGameDataStorage.Instance.CurrentTurn].GetMoveCell();

						if(_moveCell != null && _moveCell.CellStatus != _selectCell.CellStatus)
						{
							InteractionCells();
						}
						else
						{
							CGameDataStorage.Instance.GamePhase = EGamePhase.SELECT;
						}

						_selectCell._isClick = false;
						_selectCell.UpdateState();
					}
				}
				break;
			case EGamePhase.INFECT:
				{
					InfectGerm();
				}
				break;
			case EGamePhase.TURNCHANGE:
				{
					ChangeTurn();
				}
				break;
			case EGamePhase.GAMEOVER:
				{
					CSceneManager.CurrentSceneManager.SendMessage("GameOver", SendMessageOptions.DontRequireReceiver);
					CGameDataStorage.Instance.GamePhase = EGamePhase.NONE;
				}
				break;
			default:
				break;
		}
	}

	private void InteractionCells()
	{
		var selectIndexX = _selectCell.CellIndexX;
		var selectIndexY = _selectCell.CellIndexY;
		var moveIndexX = _moveCell.CellIndexX;
		var moveIndexY = _moveCell.CellIndexY;

		if (selectIndexX - 2 > moveIndexX || selectIndexX + 2 < moveIndexX ||
		    selectIndexY - 2 > moveIndexY || selectIndexY + 2 < moveIndexY)
		{
			CGameDataStorage.Instance.GamePhase = EGamePhase.SELECT;
		}
		else
		{
			if (selectIndexX - 1 > moveIndexX || selectIndexX + 1 < moveIndexX ||
				selectIndexY - 1 > moveIndexY || selectIndexY + 1 < moveIndexY)
			{
				_moveCell.ChangeGermColor(_selectCell.CellStatus);
				_selectCell.ChangeGermColor(EGermColor.NONE);
			}
			else
			{
				_moveCell.ChangeGermColor(_selectCell.CellStatus);
			}

			CGameDataStorage.Instance.GamePhase = EGamePhase.INFECT;
		}
	}

	private void InfectGerm()
	{
		for(int i = -1; i < 2; ++i)
		{
			for(int j = -1; j < 2; ++j)
			{
				var moveIndexX = _moveCell.CellIndexX;
				var moveIndexY = _moveCell.CellIndexY;

				if(moveIndexY + i < 0 || moveIndexY + i > 6 ||
					moveIndexX + j < 0 || moveIndexX + j > 6)
				{
					continue;
				}
				else
				{
					var cell = CGameDataStorage.Instance.CellObjectList[moveIndexX + j, moveIndexY + i].GetComponent<CCell>();

					if (cell.CellStatus != EGermColor.NONE &&
						cell.CellStatus != _moveCell.CellStatus)
					{
						cell.ChangeGermColor(_moveCell.CellStatus);
					}
				}
			}
		}

		_selectCell = null;
		_moveCell = null;
		CGameDataStorage.Instance.GamePhase = EGamePhase.TURNCHANGE;
	}

	private void ChangeTurn()
	{
		if(CGameDataStorage.Instance.CurrentTurn == EGermColor.BLUE)
		{
			CGameDataStorage.Instance.CurrentTurn = EGermColor.RED;
		}
		else
		{
			CGameDataStorage.Instance.CurrentTurn = EGermColor.BLUE;
		}
	
		CGameDataStorage.Instance.GamePhase = EGamePhase.SELECT;

		GameOverCheck();
		CSceneManager.UIRoot.BroadcastMessage("UpdateState");
	}

	private void GameOverCheck()
	{
		CGameDataStorage.Instance.BlueGerms = 0;
		CGameDataStorage.Instance.RedGerms = 0;

		int deadBlueGerms = 0;
		int deadRedGerms = 0;

		for (int i = 0; i < 7; ++i)
		{
			for (int j = 0; j < 7; ++j)
			{
				var cell = CGameDataStorage.Instance.CellObjectList[i, j].GetComponent<CCell>();
				cell.DeadCheck();

				if (cell.CellStatus == EGermColor.BLUE)
				{
					CGameDataStorage.Instance.BlueGerms += 1;

					if (cell._isDead)
					{
						deadBlueGerms += 1;
					}
				}
				else if (cell.CellStatus == EGermColor.RED)
				{
					CGameDataStorage.Instance.RedGerms += 1;

					if (cell._isDead)
					{
						deadRedGerms += 1;
					}
				}
			}
		}

		Function.ShowLog("Dead B : {0} R : {1}", deadBlueGerms, deadRedGerms);
		Function.ShowLog("B : {0}, R : {1}", CGameDataStorage.Instance.BlueGerms, CGameDataStorage.Instance.RedGerms);

		if (deadBlueGerms >= CGameDataStorage.Instance.BlueGerms ||
			deadRedGerms >= CGameDataStorage.Instance.RedGerms)
		{
			if (CGameDataStorage.Instance.RedGerms > CGameDataStorage.Instance.BlueGerms)
			{
				CGameDataStorage.Instance.WinnerString = "빨강승리";
			}
			else
			{
				CGameDataStorage.Instance.WinnerString = "파랑승리";
			}

			CSceneManager.UIRoot.BroadcastMessage("UpdateState");
			CGameDataStorage.Instance.GamePhase = EGamePhase.GAMEOVER;
			return;
		}

		if (CGameDataStorage.Instance.RedGerms + CGameDataStorage.Instance.BlueGerms >= 49)
		{
			if (CGameDataStorage.Instance.RedGerms > CGameDataStorage.Instance.BlueGerms)
			{
				CGameDataStorage.Instance.WinnerString = "빨강승리";
			}
			else
			{
				CGameDataStorage.Instance.WinnerString = "파랑승리";
			}

			CSceneManager.UIRoot.BroadcastMessage("UpdateState");
			CGameDataStorage.Instance.GamePhase = EGamePhase.GAMEOVER;
			return;
		}
		else if (CGameDataStorage.Instance.RedGerms == 0)
		{
			CGameDataStorage.Instance.WinnerString = "파랑승리";
			CSceneManager.UIRoot.BroadcastMessage("UpdateState");
			CGameDataStorage.Instance.GamePhase = EGamePhase.GAMEOVER;
			return;
		}
		else if (CGameDataStorage.Instance.BlueGerms == 0)
		{
			CGameDataStorage.Instance.WinnerString = "빨강승리";
			CSceneManager.UIRoot.BroadcastMessage("UpdateState");
			CGameDataStorage.Instance.GamePhase = EGamePhase.GAMEOVER;
			return;
		}
	}

}

