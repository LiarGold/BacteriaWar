using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 마우스 버튼
public enum EMouseButton
{
	LEFT,
	RIGHT,
	MIDDLE,
	NONE
}

public enum EGamePhase
{
	IDLE,
	SELECT,
	MOVE,
	PRODUCTION,
	NONE
}

//! 전역 상수
public static partial class KDefine
{
    // 해상도
    public static readonly int SCREEN_WIDTH = 1280;
    public static readonly int SCREEN_HEIGHT = 720;

    public static readonly float UNIT_SCALE = 0.01f;
}