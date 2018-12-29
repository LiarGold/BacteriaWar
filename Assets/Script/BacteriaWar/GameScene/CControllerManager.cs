using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CControllerManager : CSingleton<CControllerManager>
{
	public struct SelectStoneInfo
	{
		int num;
		Color color;
	}

	public EStoneColor CurrentTurn = EStoneColor.BLUE;
}
