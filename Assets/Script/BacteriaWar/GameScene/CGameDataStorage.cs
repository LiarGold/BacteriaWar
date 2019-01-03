using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameDataStorage : CSingleton<CGameDataStorage>
{
	[HideInInspector] public EGamePhase GamePhase = EGamePhase.NONE;
	[HideInInspector] public EGermColor CurrentTurn { get; set; }
	[HideInInspector] public int RedGerms { get; set; }
	[HideInInspector] public int BlueGerms { get; set; }
	[HideInInspector] public string WinnerString { get; set; }
	[HideInInspector] public GameObject[,] CellObjectList { get; set;}
}
