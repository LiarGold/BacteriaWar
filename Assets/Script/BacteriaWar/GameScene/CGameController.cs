using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameController : CSingleton<CGameController> {

	public EGamePhase GamePhase = EGamePhase.NONE;
	public GameObject[] CellList = null;

	public void Awake()
	{
		
	}

	public void Update()
	{
		switch (GamePhase)
		{
			case EGamePhase.IDLE:
				{

				}
				break;
			case EGamePhase.SELECT:
				{

				}
				break;
			case EGamePhase.MOVE:
				{

				}
				break;
			case EGamePhase.PRODUCTION:
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

}

