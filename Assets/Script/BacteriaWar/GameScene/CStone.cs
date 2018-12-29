using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 돌
public class CStone : MonoBehaviour
{
	#region
	public EStoneColor _stoneColor = EStoneColor.NONE;
	public Button _stoneButton = null;
	public bool _isActive = false;
	#endregion public변수

	private bool _isSelect = false;

	public void Awake()
    {
		ChangeStoneColor();
	}

	public void OnTouchStone()
	{

		ChangeStoneColor();

	}

	public void ChangeStoneColor()
	{
		var color = _stoneButton.GetComponent<Image>().color;

		switch (_stoneColor)
		{
			case EStoneColor.NONE:
				{
					color = Color.white;
				}
				break;
			case EStoneColor.RED:
				{
					color = Color.red;
				}
				break;
			case EStoneColor.BLUE:
				{
					color = Color.blue;
				}
				break;
		}

		if (_isActive)
		{
			color.a = 255;
		}
		else
		{
			color.a = 0;
		}

		_stoneButton.GetComponent<Image>().color = color;
	}
}
