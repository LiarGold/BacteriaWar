using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C1PInputManager : CInputManagerBase
{
	public override CCell GetSelectCell()
	{
		var ray = CSceneManager.MainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit rayCastHit;

		if (Physics.Raycast(ray, out rayCastHit))
		{
			var cell = rayCastHit.collider.GetComponent<CCell>();

			if (cell != null && cell.CellStatus == EGermColor.BLUE)
			{
				return cell.OnClickCell();
			}
			else
			{
				return null;
			}
		}
		else
		{
			return null;
		}
	}

	public override CCell GetMoveCell()
	{
		var ray = CSceneManager.MainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit rayCastHit;

		if (Physics.Raycast(ray, out rayCastHit))
		{
			var cell = rayCastHit.collider.GetComponent<CCell>();

			if (cell != null && cell.CellStatus == EGermColor.NONE)
			{
				return cell.OnClickCell();
			}
			else
			{
				return null;
			}
		}
		else
		{
			return null;
		}
	}
}
