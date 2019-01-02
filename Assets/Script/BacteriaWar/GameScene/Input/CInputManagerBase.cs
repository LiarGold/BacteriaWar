using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CInputManagerBase : MonoBehaviour
{
	public abstract CCell GetSelectCell();
	public abstract CCell GetMoveCell();
}
