using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 돌
public class CStone : CComponent
{
	#region
	public EStoneColor _stoneColor = EStoneColor.NONE;
	public Button _stoneButton = null;
	public bool _isActive = false;
	#endregion public변수

	private bool _isSelect = false;

	public override void Awake()
	{
		base.Awake();

		ChangeStoneColor();
	}

	public void OnTouchStone()
	{
		var selectColor = CControllerManager.Instance.SelectStoneColor;

		switch (selectColor)
		{
			case EStoneColor.RED:
				{
					if (!_isActive)
					{
						_stoneColor = selectColor;
					}
					else
					{

					}
				}
				break;
			case EStoneColor.BLUE:
				{
					if (!_isActive)
					{
						_stoneColor = selectColor;
					}
				}
				break;
			case EStoneColor.NONE:
				{
					if (_isActive)
					{
						CControllerManager.Instance.SelectStoneColor = _stoneColor;
						var color = _stoneButton.image.color;
						color += Color.black;
						_stoneButton.image.color = color;
					}
				}
				break;
			default:
				break;
		}

		ChangeStoneColor();

		Function.ShowLog("GameStatus : {0}", CControllerManager.Instance.SelectStoneColor.ToString());
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
